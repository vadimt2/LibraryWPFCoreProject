using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.JournalService;
using WpfLibraryProj.ViewModels.Employee;
using WpfLibraryProj.ViewModels.Manager;
using WpfLibraryProj.Views.LibraryItems.AllCollection;

namespace WpfLibraryProj.ViewModels.Manage.Items
{
    public class AddJournalViewModel : BindableBase, INavigationAware
    {
        private readonly IJournalService _journalService;

        private readonly IRegionManager _regionManager;

        private Journal _journal;

        public Journal Journal
        {
            get => _journal; set
            {
                SetProperty(ref _journal, value);
            }
        }

        private ObservableCollection<JournalCategory> _journalCategories;

        public ObservableCollection<JournalCategory> JournalCategories
        {
            get => _journalCategories; set
            {
                SetProperty(ref _journalCategories, value);
            }
        }

        public DelegateCommand AddItemCommand { get; set; }

        public DelegateCommand ClearFeildsCommand { get; set; }

        private bool _updateItem;

        private Journal _tempJournal;


        public AddJournalViewModel(IJournalService journalService, IRegionManager regionManager)
        {
            _tempJournal = Application.Current.Properties[NavParameters.Journal] as Journal;

            _journalService = journalService;

            _regionManager = regionManager;

            JournalCategories = new ObservableCollection<JournalCategory>();

            foreach (JournalCategory category in (JournalCategory[])Enum.GetValues(typeof(JournalCategory)))
                JournalCategories.Add(category);

            AddItemCommand = new DelegateCommand(AddItem);

            ClearFeildsCommand = new DelegateCommand(ClearFeilds);

            if (_tempJournal != null)
            {
                Journal = _tempJournal;

                _updateItem = true;

                Application.Current.Properties.Remove(NavParameters.Journal);
            }
            else
                Journal = new Journal();
        }

        private void AddItem()
        {
            if (string.IsNullOrWhiteSpace(Journal.Title) || string.IsNullOrWhiteSpace(Journal.Publisher) || string.IsNullOrWhiteSpace(Journal.Description) ||
            Journal.Price <= 0 || Journal.Quantity <= 0 || Journal.PublishDate == null) return;

            Journal.Discount((int)Journal.Disscount);

            if (_updateItem)
            {
                Journal.Id = _tempJournal.Id;

                _journalService.UpdateJournal(Journal);

                var getUser = Application.Current.Properties[NavParameters.User] as User;

                if (getUser == null) return;

                if (getUser.UserRank == UserRank.Manager)
                    ManagerMainViewModel.ManagerFrame.Navigate(new ItemHasAddedView());

                if (getUser.UserRank == UserRank.Manager)
                    EmployeeMainViewModel.EmployeeFrame.Navigate(new ItemHasAddedView());
            }
            else
                _journalService.AddJournal(Journal);

            ClearFeilds();


            AddItemViewModel.AddItemFrame.Navigate(new ItemHasAddedView());
        }

        public void ClearFeilds()
        {
            Journal.PublishDate = new DateTime();
            Journal.Title = "";
            Journal.Publisher = "";
            Journal.Description = "";
            Journal.Disscount = 0;
            Journal.Quantity = 0;
            Journal.Price = 0;
            Journal.JournalCategory = JournalCategory.Art;
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            var getJournal = navigationContext.Parameters[NavParameters.Journal];

            //if (getJournal != null)
            //{
            //    var journal = getJournal as Journal;

            //    PublishDate = journal.PublishDate;

            //    Title = journal.Title;

            //    Publisher = journal.Publisher;

            //    Description = journal.Description;

            //    Disscount = journal.Disscount;

            //    Quantity = journal.Quantity;

            //    Price = journal.Price;

            //    JournalCategory = journal.JournalCategory;

            //    _tempJournal = journal;

            //    _updateItem = true;

            //}
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

    }
}
