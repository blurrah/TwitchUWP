using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TwitchUWP.Models;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TwitchUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Game> topGames { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            // Load GameOverviewPage on load
            NavFrame.Navigate(typeof(GameOverviewPage));
            PageTitle.Text = "Twitch";

            topGames = new ObservableCollection<Game>();
            RefreshGames();
        }

        public async void RefreshGames()
        {
            Task t = TwitchAPIHelper.PopulateTwitchTopGamesAsync(topGames);
            await t;
        }

        private void BackButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (NavFrame.CanGoBack)
            {
                NavFrame.GoBack();
            }
        }

        private void GamesListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedGame = (Game)e.ClickedItem;
            string gameTitle = selectedGame.name;

            NavFrame.Navigate(typeof(DetailViewPage), gameTitle);
        }
    }

   
}
