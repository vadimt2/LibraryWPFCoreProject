using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.UserService;

namespace WpfLibraryProj.ViewModels.Employee
{
    public class EmployeeAllUsersViewModel : BindableBase
    {

        private ObservableCollection<User> _allUsersCollection;

        public ObservableCollection<User> AllUsersCollection
        {
            get => _allUsersCollection; set
            {
                SetProperty(ref _allUsersCollection, value);
            }
        }

        private readonly IUserService _userItemService;

        public EmployeeAllUsersViewModel(IUserService userItemService)
        {

            _userItemService = userItemService;

            var allUserItemCollection = _userItemService.GetUsers();

            AllUsersCollection = new ObservableCollection<User>(allUserItemCollection);

        }

        //private void UpdateList()
        //{
        //    var allUserItemCollection = _userItemService.GetUsers();

        //    AllUsersCollection = new ObservableCollection<User>(allUserItemCollection);
        //}
    }
}