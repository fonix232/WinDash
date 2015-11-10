using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinDash.ViewModels
{
    public class ViewModelLocator
    {
        //private static NavigationService _navService { get; set; }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //_navService = new NavigationService();
            //SimpleIoc.Default.Register<INavigationService>(() => _navService);
            //SimpleIoc.Default.Register<IDialogService, DialogService>();

            //ConfigureNavigation();


            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DeviceInfoViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        //private static void ConfigureNavigation()
        //{
        //    _navService.Configure("DeviceInfo", typeof(Views.DeviceInfo));
        //    _navService.Configure("Settings", typeof(Views.Settings));
        //}

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public DeviceInfoViewModel DeviceInfo => ServiceLocator.Current.GetInstance<DeviceInfoViewModel>();
        public SettingsViewModel Settings => ServiceLocator.Current.GetInstance<SettingsViewModel>();
    }
}
