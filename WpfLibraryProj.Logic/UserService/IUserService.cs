using System.Collections.Generic;
using WpfLibraryProj.Common;

namespace WpfLibraryProj.Logic.UserService
{
    public interface IUserService
    {
        void DeleteUser(int id);
        IEnumerable<User> GetUsers();
        bool ManagerAddUser(User user);
        void ManagerUpdateUser(User user);
        User Signin(User user);
        bool Signup(User user);
        void UpdateUser(User user);
    }
}