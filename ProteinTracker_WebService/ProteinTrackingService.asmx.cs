using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using ProteinTracker_WebService.DAL;

namespace ProteinTracker_WebService
{
    /// <summary>
    /// interface for Wcf service
    /// </summary>
    [WebService(Namespace = "http://proteinTracker.ie/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //  [System.ComponentModel.ToolboxItem(false)]
    [ScriptService] // allows us to use .this through defing a service  script service 
    [ServiceContract(Namespace = "http://simpleprogram.ie")] // needed for WCF
    interface IProteinTrackerService
    {
        [WebMethod]
        [OperationContract]
        int AddProtein(int amount, int userId);


        [WebMethod]
        [OperationContract]
        int AddUser(string name, int goal);

        [WebMethod]
        [OperationContract]
        List<User> ListUsers();
    }

    /// <summary>
    /// Summary description for ProteinTrackingService
    /// </summary>
    //  [WebService(Namespace = "http://proteinTracker.ie/")]
    //[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    //  [System.ComponentModel.ToolboxItem(false)]
    //[ScriptService] // allows us to use .this through defing a service  script service 
    public class ProteinTrackingService : System.Web.Services.WebService, IProteinTrackerService
    {
        public class AuthenticationHeader : SoapHeader
        {
            public string UserName;
            public string Password;
        }

        public AuthenticationHeader Authentication;

        private UserRepository repo = new UserRepository();

        //[WebMethod]
        //[SoapHeader("Authentication")]
        public int AddProtein(int amount, int userId)
        {
            //  if (Authentication == null || Authentication.UserName != "Mark" || Authentication.Password != "pass")
            //    throw new Exception("Bad credentials!!");


            var user = repo.GetUserById(userId);

            if (user == null)
                return -1;

            user.Total += amount;
            repo.Save(user);

            return user.Total;


            return 0;
        }

        // [WebMethod]
        public int AddUser(string name, int goal)
        {
            var user = new User { UserName = name, Goal = goal, Total = 0 };
            repo.AddUser(user);

            return user.UserId;
        }

       // [WebMethod]
        public List<User> ListUsers()
        {
            return new List<User>(repo.GetAllUsers());
        }

    }
}
