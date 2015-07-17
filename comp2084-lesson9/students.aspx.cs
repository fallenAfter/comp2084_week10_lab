using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//add database conection
using comp2084_lesson9.Models;

namespace comp2084_lesson9
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //call the GetDepartments function to populate the grid
            if (!IsPostBack)
            { //only populates on load if first time running this page. if delete is pressed will not run
                getStudents();
            }

        }

        protected void getStudents()
        {
            //conect and use database student table
            using (DefaultConnection db = new DefaultConnection())
            {
                var stdt = from s in db.Students
                           select s;

                //bind query return to grid
                grdStudents.DataSource = stdt.ToList();
                grdStudents.DataBind();
            }
        }

        protected void grdStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //identify student id to manipulate
            Int32 StudentID = Convert.ToInt32(grdStudents.DataKeys[e.RowIndex].Values["StudentID"]);


            //Select current id based upon selected row
            using (DefaultConnection db = new DefaultConnection())
            {
                Student stdt = (from s in db.Students
                                where s.StudentID == StudentID
                                select s).FirstOrDefault();
                db.Students.Remove(stdt);
                db.SaveChanges();

                //refresh grid
                getStudents();
            }
        }
    }
}