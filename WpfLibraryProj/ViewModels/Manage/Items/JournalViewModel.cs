using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WpfLibraryProj.Common;

namespace WpfLibraryProj.ViewModels.Manage.Items
{
    public class JournalViewModel : BindableBase, INavigationAware
    {
        private Journal _journal;

        public Journal Journal
        {
            get => _journal; set
            {
                SetProperty(ref _journal, value);
            }
        }

        public JournalViewModel()
        {
            _journal = Application.Current.Properties[NavParameters.Journal] as Journal;

            Application.Current.Properties.Remove(NavParameters.Journal);
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            var getJournal = navigationContext.Parameters[NavParameters.Journal];

            if (getJournal == null) return;

            Journal = getJournal as Journal;
        }
    }
}

