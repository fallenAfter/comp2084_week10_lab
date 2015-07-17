using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//add database to the scope
using comp2084_lesson9.Models;

namespace comp2084_lesson9
{
    public partial class courseDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading for first time get the departments and fill drop down
            if (!IsPostBack)
            {
                getDepartments();

                //check url for a course id
                if (!string.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    getCourse();
                    //show enrolments
                    pnlEnrollments.Visible = true;
                }
                else
                {
                    //hide enrolments is new course
                    pnlEnrollments.Visible = false;
                }
                
            }
        }
        
        protected void getDepartments(){
            using (DefaultConnection db = new DefaultConnection())
            {
                var dpmt = from d in db.Departments
                           orderby d.Name
                           select d;
                ddlDepartments.DataSource = dpmt.ToList();
                ddlDepartments.DataBind();

                //add default option to drop down list
                ListItem DefaultItem = new ListItem("-Select-", "0");
                ddlDepartments.Items.Insert(0, DefaultItem);
            }
        }
        protected void getCourse()
        {
            //fill in form based upon selected course
            Int32 CourseID = Convert.ToInt32(Request.QueryString["CourseID"]);

            using (DefaultConnection db = new DefaultConnection())
            {
                //query database
                Course objc = (from c in db.Courses
                               where c.CourseID == CourseID
                               select c).FirstOrDefault();

                //populate the form
                txtTitle.Text = objc.Title;
                txtCredits.Text = Convert.ToString(objc.Credits);

                //assaign value for drop down menu
                ddlDepartments.SelectedValue = Convert.ToString(objc.DepartmentID);

                //populate enrollments grid
                var Enrollments = from en in db.Enrollments
                                  where en.CourseID == CourseID
                                  orderby en.Student.LastName, en.Student.FirstMidName
                                  select en;

                //bind to grid
                grdEnrollments.DataSource = Enrollments.ToList();
                grdEnrollments.DataBind();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //create new course and fill properties
                Course objc = new Course();
                objc.Title = txtTitle.Text;
                objc.Credits = Convert.ToInt32(txtCredits.Text);
                objc.DepartmentID = Convert.ToInt32(ddlDepartments.SelectedValue);

                //save
                db.Courses.Add(objc);
                db.SaveChanges();

                //redirect back to courses page
                Response.Redirect("courses.aspx");

                //handle updating a record
                if (!string.IsNullOrEmpty(Request.QueryString["CourseID"]))
                {
                    Int32 CourseID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                }

            }
        }
    }
}