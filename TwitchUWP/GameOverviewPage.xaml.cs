using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TwitchUWP.Models;
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

namespace TwitchUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameOverviewPage : Page
    {
        public ObservableCollection<Game> topGames { get; set; }

        public GameOverviewPage()
        {
            this.InitializeComponent();

            topGames = new ObservableCollection<Game>();
            RefreshGames();
        }

        public async void RefreshGames()
        {
            Task t = TwitchAPIHelper.PopulateTwitchTopGamesAsync(topGames);
            await t;
        }

        private void GamesListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedGame = (Game)e.ClickedItem;
            string gameTitle = selectedGame.name;

            this.Frame.Navigate(typeof(DetailViewPage), gameTitle);
        }
    }
}
