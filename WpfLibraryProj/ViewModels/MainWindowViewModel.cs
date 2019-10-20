using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using WpfLibraryProj.ViewModels.Manage.Items;
using WpfLibraryProj.Views;
using WpfLibraryProj.Views.LibraryItems.AllCollection;
using WpfLibraryProj.Views.Users.Customer;
using WpfLibraryProj.Views.Users.Manager;

namespace WpfLibraryProj.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private static Frame _mianFrame;

        public static Frame MainFrame
        {
            get
            {
                if (_mianFrame == null)
                    _mianFrame = new Frame();

                return _mianFrame;
            }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            MainFrame.Navigate(new MainView());
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion(Regions.MainRegion, typeof(MainView));
            _regionManager.RegisterViewWithRegion(Regions.ManagerViewRegion, typeof(ManageAllItemsView));
            //_regionManager.RegisterViewWithRegion(Regions.CustomerRegion, typeof(ViewAllItemsView));
        }

        //private ContentControl _currentView;

        //public ContentControl CurrentView
        //{
        //    get => _currentView; set
        //    {
        //        SetProperty(ref _currentView, value);
        //    }
        //}

        //public MainWindowViewModel()
        //{
        //    CurrentView = new MainView();
        //}
    }
}
