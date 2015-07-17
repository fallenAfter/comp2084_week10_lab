using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//refrence database model to connect to sql server
using comp2084_lesson9.Models;

namespace comp2084_lesson9
{
    public partial class departmentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //call get method if it is first time to page and so it does not run when form is submited
            if (!IsPostBack)
            {
                //look up the selected department and fill the form
                if (!String.IsNullOrEmpty(Request.QueryString["DepartmentID"]))
                {
                    getDepartment();
                }
            }

        }

        protected void getDepartment()
        {
            //look up the selected department and fill the form
            using (DefaultConnection db = new DefaultConnection())
            {
                Int32 DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                //look up the department
                Department dep = (from d in db.Departments
                                  where d.DepartmentID == DepartmentID
                                  select d).FirstOrDefault();

                //prepopulate the for fields
                txtName.Text = dep.Name;
                txtbudget.Text = dep.Budget.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //connect
            using (DefaultConnection db = new DefaultConnection())
            {
                //create a new department in memory
                Department dep = new Department();

                Int32 DepartmentID = 0;

                //handle updating a recod
                if (!String.IsNullOrEmpty(Request.QueryString["DepartmentID"]))
                {
                    DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    dep = (from d in db.Departments
                           where d.DepartmentID == DepartmentID
                           select d).FirstOrDefault();
                }

                //fill the properties of the department
                dep.Name = txtName.Text;
                dep.Budget = Convert.ToDecimal(txtbudget.Text);

                if (DepartmentID == 0)
                {
                    //save the new department
                    db.Departments.Add(dep);
                }


                db.SaveChanges();

                //redirect to departments list page
                Response.Redirect("departments.aspx");
            }
        }
    }
}