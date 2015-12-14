using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TwitchUWP.Models;
using System.Diagnostics;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TwitchUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailViewPage : Page
    {
        public ObservableCollection<Streamers.Stream> streams { get; set; }

        public DetailViewPage()
        {
            this.InitializeComponent();

            var manager = SystemNavigationManager.GetForCurrentView();
            manager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            streams = new ObservableCollection<Streamers.Stream>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string gameTitle = (string)e.Parameter;

            loadStreamers(gameTitle);
        }

        public async void loadStreamers(string gameTitle)
        {
            Task t = TwitchAPIHelper.PopulateTwitchStreamersAsync(gameTitle, streams);
            await t;
        }

        private void StreamsListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
