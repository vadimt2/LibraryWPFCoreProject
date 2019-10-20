using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WpfLibraryProj.Common;
using WpfLibraryProj.Views;
using WpfLibraryProj.Views.LibraryItems.AllCollection;
using WpfLibraryProj.Views.Users.Employee;

namespace WpfLibraryProj.ViewModels.Employee
{
    public class EmployeeMainViewModel : BindableBase
    {
        private static Frame _employeeFrame;

        public static Frame EmployeeFrame
        {
            get
            {
                if (_employeeFrame == null)
                    _employeeFrame = new Frame();

                return _employeeFrame;
            }
        }

        private User _user;

        public DelegateCommand SignoutCommand { get; private set; }

        public DelegateCommand ViewAllItemsCommand { get; private set; }

        public DelegateCommand AddItemCommand { get; private set; }

        public DelegateCommand ViewAllUsersCommand { get; private set; }

        public EmployeeMainViewModel()
        {
            AddItemCommand = new DelegateCommand(AddItem);

            ViewAllItemsCommand = new DelegateCommand(ViewAllItems);

            ViewAllUsersCommand = new DelegateCommand(ViewAllUsers);

            SignoutCommand = new DelegateCommand(Signout);

            _user = Application.Current.Properties[NavParameters.User] as User;

            EmployeeFrame.Navigate(new EmployeeAllStoreItemsView());
        }

        private void Signout()
        {
            Application.Current.Properties.Remove(NavParameters.User);

            MainWindowViewModel.MainFrame.Navigate(new MainView());

        }

        private void ViewAllItems()
        {
            EmployeeFrame.Navigate(new EmployeeAllStoreItemsView());

        }

        private void ViewAllUsers()
        {
            EmployeeFrame.Navigate(new EmployeeAllUsersView());
        }

        void AddItem()
        {
            EmployeeFrame.Navigate(new AddItemView());
        }

    }
}
