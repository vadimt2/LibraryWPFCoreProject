using System.Collections.Generic;
using System.Linq;
using WpfLibraryProj.Common;
using WpfLibraryProj.DAL;

namespace WpfLibraryProj.Logic.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepos;

        public UserService(IRepository<User> userReposy)
        {
            _userRepos = userReposy;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepos.GetAll();
        }

        public bool Signup(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password)) return false;

            user.UserRank = 0;

            var getUser = IsUserExsist(user);

            if (getUser != null) return false;

            _userRepos.Insert(user);

            var result = _userRepos.Save();

            return result;
        }

        public User Signin(User user)
        {
            var getUser = IsUserExsist(user);

            if (getUser == null) return null;

            return getUser.Password == user.Password ? getUser : null;
        }

        private User IsUserExsist(User user)
        {
            var getUsers = _userRepos.GetAll().ToList();

            var getUserInList = getUsers.FirstOrDefault(userInList => userInList.UserName.ToLower() == user.UserName.ToLower());

            return getUserInList ?? getUserInList;
        }

        public void DeleteUser(int id)
        {
            var getUser = _userRepos.GetById(id);

            if (getUser == null) return;

            _userRepos.Delete(id);

            _userRepos.Save();
        }

        public void UpdateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password)) return;

            var getUser = _userRepos.GetById(user.Id);

            if (getUser == null) return;

            getUser.UserName = user.UserName;

            getUser.Password = user.Password;

            getUser.Email = user.Email;

            _userRepos.Update(getUser);

            _userRepos.Save();
        }

        public void ManagerUpdateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password)) return;

            var getUser = _userRepos.GetById(user.Id);

            if (getUser == null) return;

            getUser.UserName = user.UserName;

            getUser.Password = user.Password;

            getUser.Email = user.Email;

            getUser.UserRank = user.UserRank;

            _userRepos.Update(getUser);

            _userRepos.Save();
        }

        public bool ManagerAddUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password)) return false;

            _userRepos.Insert(user);

            var result = _userRepos.Save();

            return result;
        }
    }
}
