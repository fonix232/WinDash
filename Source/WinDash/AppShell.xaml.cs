using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WinDash
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        private DispatcherTimer timer;

        public AppShell()
        {
            this.InitializeComponent();

            this.Loaded += (sender, e) =>
            {
                timer = new DispatcherTimer();
                timer.Tick += Timer_Tick;
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Start();
            };

            this.Unloaded += (sender, e) =>
            {
                timer.Stop();
                timer = null;
            };

            Current = this;
        }


        public static AppShell Current { get; private set; }

        public Frame AppFrame { get { return this.appFrame; } }

        private void Timer_Tick(object sender, object e)
        {
            var t = DateTime.Now;
            this.CurrentTime.Text = t.ToString("T", CultureInfo.CurrentCulture);
        }

        private async void powerMenuList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as FrameworkElement;
            if (item == null)
                return;

            // Not supported in latest releases
            // TODO: Uncomment when fixed
            //var dialog = new MessageDialog("Are you sure you want to " + (e.ClickedItem as TextBlock).Text.ToLower() + "?", (e.ClickedItem as TextBlock).Text.ToUpper());

            //var resp = await dialog.ShowAsync();

            //if (resp.Label == "No")
            //    return;

            switch (item.Name)
            {
                case "ShutdownOption":
                    ShutdownHelper(ShutdownKind.Shutdown);
                    break;
                case "RestartOption":
                    ShutdownHelper(ShutdownKind.Restart);
                    break;
            }
        }

        private void ShutdownHelper(ShutdownKind kind)
        {
            ShutdownManager.BeginShutdown(kind, TimeSpan.FromSeconds(0.5));
        }

        private void powerButton_Click(object sender, RoutedEventArgs e)
        {
            powerMenu.IsOpen = !powerMenu.IsOpen;
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            while (AppFrame.CanGoBack)
                AppFrame.GoBack();

            AppFrame.Navigate(typeof(Views.Settings));

            setButtonsColor(settingsButton.Name);
        }

        private void deviceInfoButton_Click(object sender, RoutedEventArgs e)
        {
            while (AppFrame.CanGoBack)
                AppFrame.GoBack();

            AppFrame.Navigate(typeof(Views.DeviceInfo));
            setButtonsColor(deviceInfoButton.Name);
        }





        private void setButtonsColor(string target)
        {
            var buttons = new List<Button> { settingsButton, deviceInfoButton };


            foreach(Button b in buttons)
            {
                if (b.Name == target)
                    b.Background = new SolidColorBrush((Color)App.Current.Resources["SystemAccentColor"]);
                else
                    b.Background = new SolidColorBrush(Colors.Transparent);
            }
        }
    }
}
