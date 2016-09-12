using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteinTracker_WebService.DAL
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> UsersList = new List<User>();
        private static int _nextId = 0;

        public void Dispose()
        {
            this.Dispose();
        }

        public void AddUser(User user)
        {
            user.UserId = _nextId;
            UsersList.Add(user);
            _nextId++;
        }

        public IReadOnlyCollection<User> GetAllUsers()
        {
            return UsersList.AsReadOnly();
        }

        public User GetUserById(int userId)
        {
            var user = UsersList.SingleOrDefault(u => u.UserId == userId);
            return user;
        }

        public void Save(User updatedUser)
        {
            var user = UsersList.FirstOrDefault(u => u.UserId == updatedUser.UserId);

            if (user == null)
                return;

            user.UserName = updatedUser.UserName;
            user.Total = updatedUser.Total;
            user.Goal = updatedUser.Goal;
        }
    }
}
