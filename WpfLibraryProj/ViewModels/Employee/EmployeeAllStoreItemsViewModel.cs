using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WpfLibraryProj.Common;
using WpfLibraryProj.Logic.AbstractItemService;

namespace WpfLibraryProj.ViewModels.Employee
{
    public class EmployeeAllStoreItemsViewModel : BindableBase
    {
        private ObservableCollection<AbstractItem> _abstractItemCollection;

        public ObservableCollection<AbstractItem> AbstractItemCollection
        {
            get => _abstractItemCollection; set
            {
                SetProperty(ref _abstractItemCollection, value);
            }
        }

        private readonly IAbsractItemService _absractItemService;

        public EmployeeAllStoreItemsViewModel(IAbsractItemService absractItemService)
        {
            _absractItemService = absractItemService;

            var abstractItemCollection = _absractItemService.GetAllItems();

            AbstractItemCollection = new ObservableCollection<AbstractItem>(abstractItemCollection);
        }

        
        private void UpdateList()
        {
            var abstractItemCollection = _absractItemService.GetAllItems();

            AbstractItemCollection = new ObservableCollection<AbstractItem>(abstractItemCollection);
        }
    }
}
