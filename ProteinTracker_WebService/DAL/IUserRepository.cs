using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteinTracker_WebService.DAL
{
    interface IUserRepository : IDisposable
    {
        void AddUser(User user);
        IReadOnlyCollection<User> GetAllUsers();
        User GetUserById(int userId);
        void Save(User updatedUser);
    }
}
