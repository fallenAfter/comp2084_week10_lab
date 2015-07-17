using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//add a reference so we can use EF for our database
using comp2084_lesson9.Models;

namespace comp2084_lesson9
{
    public partial class departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //call the GetDepartments function to populate the grid
            if (!IsPostBack)
            { //only populates on load if first time running this page. if delete is pressed will not run
                GetDepartments();
            }

        }

        protected void GetDepartments()
        {
            //use Entity Framework to connect and get the list of Departments
            using (DefaultConnection db = new DefaultConnection())
            {
                var deps = from d in db.Departments
                           select d;

                //bind the deps query result to our grid
                grdDepartments.DataSource = deps.ToList();
                grdDepartments.DataBind();
            }
        }

        protected void grdDepartments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //set the new page index and repopulate the grid
            grdDepartments.PageIndex = e.NewPageIndex;
            GetDepartments();
        }

        protected void grdDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //identify department id to be deleted from the row the user selected
            Int32 DepartmentID = Convert.ToInt32(grdDepartments.DataKeys[e.RowIndex].Values["DepartmentID"]);

            //connect to database
            using (DefaultConnection db = new DefaultConnection())
            {
                //SElect current department based upon id
                Department dep = (from d in db.Departments
                                  where d.DepartmentID == DepartmentID
                                  select d).FirstOrDefault();
                db.Departments.Remove(dep);
                db.SaveChanges();

                //refresh the grid
                GetDepartments();

            }

        }
    }
}