using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProteinTracker_WebService.DAL;

namespace ProteinTracker_WebService
{
    public partial class ProteinTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*===== Page Methods ======*/
        [WebMethod]
        //[SoapHeader("Authentication")]
        public static int AddProtein(int amount, int userId)
        {
            UserRepository repo = new UserRepository();

            var user = repo.GetUserById(userId);

            if (user == null)
                return -1;

            user.Total += amount;
            repo.Save(user);

            return user.Total;


        }

        [WebMethod]
        public static int AddUser(string name, int goal)
        {
            UserRepository repo = new UserRepository();

            var user = new User { UserName = name, Goal = goal, Total = 0 };
            repo.AddUser(user);

            return user.UserId;
        }

        [WebMethod]
        public static List<User> ListUsers()
        {
            UserRepository repo = new UserRepository();

            return new List<User>(repo.GetAllUsers());
        }

    }
}