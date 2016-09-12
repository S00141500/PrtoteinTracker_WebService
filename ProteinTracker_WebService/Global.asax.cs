using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ProteinTracker_WebService.DAL;

namespace ProteinTracker_WebService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
             ProteinTracker_WebService.DAL.UserRepository repo = new UserRepository();
            repo.AddUser(new User { Goal = 100, UserName = "Mark", Total = 10 });
            repo.AddUser(new User { Goal = 123, UserName = "Mick", Total = 50 });
            repo.AddUser(new User { Goal = 110, UserName = "Tom", Total = 20 });
            repo.AddUser(new User { Goal = 200, UserName = "Frank", Total = 0 });
            repo.AddUser(new User { Goal = 70, UserName = "Tim", Total = 50 });
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}