using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace WpfLibraryProj.Navigation
{
    public interface INavigationFrame
    {
        public Frame BindFrame { get; set; }
    }
}
