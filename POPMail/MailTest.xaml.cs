﻿using System;
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
using OpenPop.Common.Logging;
using OpenPop.Mime;
using OpenPop.Mime.Decode;
using OpenPop.Mime.Header;
using OpenPop.Pop3;



// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace POPMail
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MailTest : POPMail.Common.LayoutAwarePage
    {
        public MailTest()
        {
            this.InitializeComponent();
        }

        private Pop3Client PopClient = new Pop3Client();
        private int messageCount = 0;
        public string crlf = Environment.NewLine;
        private Windows.UI.Xaml.Visibility hidden = Windows.UI.Xaml.Visibility.Collapsed;
        private Windows.UI.Xaml.Visibility visible = Windows.UI.Xaml.Visibility.Visible;

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
            fetchMessageHeaders();
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void openConnection()
        {
            var hostname = "";
            var port = 110;
            var useSsl = false;
            var username = "";
            var password = "";

            Windows.Storage.ApplicationDataContainer loadSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            
            if (loadSettings.Values.ContainsKey("username"))
            {
                username = loadSettings.Values["username"].ToString();
            }

            if (loadSettings.Values.ContainsKey("server"))
            {
                hostname = loadSettings.Values["server"].ToString();
            }

            if (loadSettings.Values.ContainsKey("password"))
            {
                password = loadSettings.Values["password"].ToString();
            }

            PopClient.Connect(hostname, port, useSsl);
            PopClient.Authenticate(username, password);
        }

        private void closeConnection()
        {
            PopClient.Disconnect();
        }

        private void fetchMessageHeaders()
        {
            MailRefresh.Visibility = visible;
            MailCountLabel.Visibility = hidden;
            MailCount.Visibility = hidden;
            MailError.Visibility = hidden;
            try
            {
                openConnection();
                MessageListView.ItemsSource = FetchAllHeaders();
                closeConnection();
                MailCount.Text = messageCount.ToString();
                
                MailCountLabel.Visibility = visible;
                MailCount.Visibility = visible;
                MailRefresh.Visibility = hidden;
                MailError.Visibility = hidden;
            }
            catch
            {
                MailCountLabel.Visibility = hidden;
                MailCount.Visibility = hidden;
                MailError.Visibility = visible;
                MailRefresh.Visibility = hidden;
            }
        }

        private List<MessageHeader> FetchAllHeaders()
        {
            messageCount = PopClient.GetMessageCount();
            List<MessageHeader> allMessageHeaders = new List<MessageHeader>(messageCount);
            for (int i = messageCount; i > 0; i--)
            {
                allMessageHeaders.Add(PopClient.GetMessageHeaders(i));
            }
            return allMessageHeaders;
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RefreshButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            fetchMessageHeaders();
        }

        private void DeleteButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var textToDisplay = "";
            for (var i = 1; i <= MessageListView.SelectedItems.Count; i++ )
            {
                //MessageHeader messageHeaderText = (MessageHeader)messageId;
                //textToDisplay += messageHeaderText.MessageId + " : " + messageHeaderText.DateSent;
                openConnection();
                textToDisplay += PopClient.GetMessage(MessageListView.SelectedIndex).FindFirstPlainTextVersion().GetBodyAsText() + " : ";
                closeConnection();
            }
            DebugText.Text = textToDisplay.ToString();
            DebugText.Visibility = visible;
        }
    }
}
