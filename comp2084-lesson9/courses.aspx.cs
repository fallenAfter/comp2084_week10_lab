using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//add defaultconnection to page
using comp2084_lesson9.Models;


namespace comp2084_lesson9
{
    public partial class courses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getCourses();
            }
        }
        protected void getCourses()
        {
            //connect to default connection
            using (DefaultConnection db = new DefaultConnection()){
                var crs = from c in db.Courses
                      select c;
                //bind query to grid
                grdCourses.DataSource = crs.ToList();
                grdCourses.DataBind();
            }
        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}