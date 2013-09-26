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
        public string crlf = Environment.NewLine;

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
            try
            {
                openConnection(PopClient);

                var numberOfMessages = getMessageCount(PopClient);
                var messageHeaders = FetchAllHeaders(PopClient);

                closeConnection(PopClient);
                ItemListView.ItemsSource = messageHeaders;
                MailCount.Text = numberOfMessages.ToString();
            }
            catch
            {
                MailCountLabel.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MailCount.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MailError.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
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

        private void openConnection(Pop3Client thisPopClient)
        {
            var hostname = "pop.spamcop.net";
            var port = 110;
            var useSsl = false;
            var username = "simons@spamcop.net";
            var password = "in8dz1re";

            thisPopClient.Connect(hostname, port, useSsl);
            thisPopClient.Authenticate(username, password);
        }

        private void closeConnection(Pop3Client thisPopClient)
        {
            thisPopClient.Disconnect();
        }
        
        private int getMessageCount(Pop3Client thisPopClient) {
            return thisPopClient.GetMessageCount();
        }

        public static List<MessageHeader> FetchAllHeaders(Pop3Client thisPopClient)
        {
            int messageCount = thisPopClient.GetMessageCount();
            List<MessageHeader> allMessageHeaders = new List<MessageHeader>(messageCount);
            for (int i = messageCount; i > 0; i--)
            {
                allMessageHeaders.Add(thisPopClient.GetMessageHeaders(i));
            }
            return allMessageHeaders;
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
