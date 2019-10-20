using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace WpfLibraryProj.ViewModels.Manage.Items
{
    public class ItemHasAddedViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;


        public ItemHasAddedViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            
        }
    }
}
