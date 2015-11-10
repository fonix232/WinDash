using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinDash.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeviceInfo : Page
    {
        public DeviceInfo()
        {
            this.InitializeComponent();

            DataContextChanged += (sender,e) =>
            {
                this.ViewModel = (ViewModels.DeviceInfoViewModel)DataContext;
                
            };
        }

        private void DeviceInfo_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            throw new NotImplementedException();
        }


        public ViewModels.DeviceInfoViewModel ViewModel { get; set; }
    }
}
