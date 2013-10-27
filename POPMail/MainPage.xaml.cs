using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace POPMail
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : POPMail.Common.LayoutAwarePage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
            // Restore values stored in app data.
            //Windows.Storage.ApplicationDataContainer loadSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataContainer loadSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (loadSettings.Values.ContainsKey("server"))
            {
                serverInput.Text = loadSettings.Values["server"].ToString();
            }
            if (loadSettings.Values.ContainsKey("username"))
            {
                nameInput.Text = loadSettings.Values["username"].ToString();
            }
            if (loadSettings.Values.ContainsKey("password"))
            {
                passwordInput.Password = loadSettings.Values["password"].ToString();
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        private void MailTestPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(serverInput.Text) || string.IsNullOrEmpty(nameInput.Text) || string.IsNullOrEmpty(passwordInput.Password))
            {
                inputError.Text = "All fields are required in order to continue. Please enter the mail account details and try again.";
            }
            else
            {
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(MailTest));
                }
            }
        }

        private void nameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Windows.Storage.ApplicationDataContainer saveSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataContainer saveSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            saveSettings.Values["username"] = nameInput.Text;
        }

        private void serverInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Windows.Storage.ApplicationDataContainer saveSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataContainer saveSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            saveSettings.Values["server"] = serverInput.Text;
        }

        private void passwordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Windows.Storage.ApplicationDataContainer saveSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.ApplicationDataContainer saveSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            saveSettings.Values["password"] = passwordInput.Password;
        }
    }
}
