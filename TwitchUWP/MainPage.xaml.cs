using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TwitchUWP.Models;
using Windows.UI.Core;
using Windows.UI.Xaml;
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

        public MainPage()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            NavFrame.Navigated += OnNavigated;

            // Load GameOverviewPage on load
            NavFrame.Navigate(typeof(GameOverviewPage));

        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (NavFrame != null && NavFrame.CanGoBack)
            {
                e.Handled = true;
                NavFrame.GoBack();
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Parameter != null && NavFrame.CanGoBack)
            {
                PageTitle.Text = e.Parameter.ToString();
            }
            else
            {
                PageTitle.Text = "Twitch";
            }
        }
    }

   
}
