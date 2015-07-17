using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//add database to resource list
using comp2084_lesson9.Models;

namespace comp2084_lesson9
{
    public partial class studetDetails : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //call post back so the form does not update with existing information
            if (!IsPostBack)
            {
                //look up selected department from get to fill in form
                if (!String.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    getStudent();
                }
            }
        }

        protected void getStudent()
        {
            //fill in form based upon selected student
            using (DefaultConnection db = new DefaultConnection())
            {
                Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                //search departmet
                Student stdt = (from s in db.Students
                                where s.StudentID == StudentID
                                select s).FirstOrDefault();

                //populate fields
                txtLName.Text = stdt.LastName;
                txtFName.Text = stdt.FirstMidName;

                //populate courses grid
                var courses = from en in db.Enrollments
                              where en.StudentID == StudentID
                              orderby en.Course.Title
                              select en;

                //bind to grid
                grdCourses.DataSource = courses.ToList();
                grdCourses.DataBind();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect to database
            using (DefaultConnection db = new DefaultConnection())
            {
                //create new student in memory
                Student stdt = new Student();
                Int32 StudentID = 0;

                DateTime EnDate = DateTime.Now;

                //handle updating a record
                if (!String.IsNullOrEmpty(Request.QueryString["StudentID"]))
                {
                    StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    stdt = (from s in db.Students
                            where s.StudentID == StudentID
                            select s).FirstOrDefault();

                    //set enroll date to equal existing result
                    EnDate = stdt.EnrollmentDate;


                }

                //fill in student details
                stdt.FirstMidName = txtLName.Text;
                stdt.LastName = txtFName.Text;
                //add current date time as time of registering
                stdt.EnrollmentDate = EnDate;

                if (StudentID == 0)
                {
                    //create new row in database
                    db.Students.Add(stdt);
                }

                db.SaveChanges();

                //redirect back to students page
                Response.Redirect("students.aspx");


            }

        }
    }
}