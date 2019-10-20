using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using WpfLibraryProj.Common;
using WpfLibraryProj.ViewModels.Manager;
using WpfLibraryProj.Views.LibraryItems.Book;
using WpfLibraryProj.Views.LibraryItems.Journal;

namespace WpfLibraryProj.ViewModels.Manage.Items
{
    public class AddItemViewModel : BindableBase, INavigationAware
    {
        private static Frame _addItemFrame;

        public static Frame AddItemFrame
        {
            get
            {
                if (_addItemFrame == null)
                    _addItemFrame = new Frame();

                return _addItemFrame;
            }
        }

        private AbstractItemCategory _abstractItemCategory;

        public AbstractItemCategory AbstractItemCategory
        {
            get => _abstractItemCategory; set
            {
                SetProperty(ref _abstractItemCategory, value);
                SelectedChange();
            }
        }

        private readonly IRegionManager _regionManager;

        private ObservableCollection<AbstractItemCategory> _abstractItemCategories;

        public ObservableCollection<AbstractItemCategory> AbstractItemCategories
        {
            get => _abstractItemCategories; set
            {
                SetProperty(ref _abstractItemCategories, value);
            }
        }

        public AddItemViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            AbstractItemCategories = new ObservableCollection<AbstractItemCategory>();
            foreach (AbstractItemCategory category in (AbstractItemCategory[])Enum.GetValues(typeof(AbstractItemCategory)))
                AbstractItemCategories.Add(category);

        }

        private void SelectedChange()
        {
            if (AbstractItemCategory == AbstractItemCategory.Book)
                AddItemFrame.Navigate(new AddBookView());
                //_regionManager.RequestNavigate(Regions.AddItem, Pages.AddBookView);


            if (AbstractItemCategory == AbstractItemCategory.Journal)
                AddItemFrame.Navigate(new AddJournalView());

            //_regionManager.RequestNavigate(Regions.AddItem, Pages.AddJournalView);

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
