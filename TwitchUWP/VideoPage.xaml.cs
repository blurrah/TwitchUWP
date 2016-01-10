using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TwitchUWP.Models;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Media.Streaming.Adaptive;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TwitchUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoPage : Page
    {

        public ObservableCollection<Message> chatMessages { get; set; }
        public LiveStream liveStream { get; set; }
        private SimpleOrientationSensor _sensor;

        public VideoPage()
        {
            this.InitializeComponent();

            _sensor = SimpleOrientationSensor.GetDefault();
            if (_sensor != null)
            {
                StreamPlayerControls.IsFullWindowButtonVisible = false;
                StreamPlayerControls.IsFullWindowEnabled = false;
                _sensor.OrientationChanged += new TypedEventHandler<SimpleOrientationSensor, SimpleOrientationSensorOrientationChangedEventArgs>(OrientationChanged);
            } 
            var manager = SystemNavigationManager.GetForCurrentView();
            manager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            chatMessages = new ObservableCollection<Message>();
            liveStream = new LiveStream();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Streamers.Channel streamer = (Streamers.Channel)e.Parameter;

            Uri uri = new Uri("http://www.twitch.tv/" + streamer.name + "/chat?popout=");

            chatWebView.LoadCompleted += chatWebView_LoadCompleted;
            chatWebView.Navigate(uri);

            loadVideo(streamer.name);
        }

        private async void loadVideo(string name)
        {
            Task t = TwitchHLSHelper.LoadTwitchStream(name, liveStream);
            await t;

            Uri streamUrl = new Uri(liveStream.sourceStream);
            var result = await AdaptiveMediaSource.CreateFromUriAsync(streamUrl);

            if (result.Status == AdaptiveMediaSourceCreationStatus.Success)
            {
                var astream = result.MediaSource;
                StreamPlayer.SetMediaStreamSource(astream);
            }

        }

        private async void chatWebView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            try
            {
                await chatWebView.InvokeScriptAsync("eval", new string[] { "document.getElementsByClassName('button glyph-only left tooltip')[0].style.display='none';" });
                await chatWebView.InvokeScriptAsync("eval", new string[] { "document.getElementsByClassName('textarea-contain')[0].style.display='none';" });
                await chatWebView.InvokeScriptAsync("eval", new string[] { "document.getElementsByClassName('button primary float-right send-chat-button')[0].style.display='none';" });
                await chatWebView.InvokeScriptAsync("eval", new string[] { "document.getElementsByClassName('chat-room')[0].setAttribute(\"style\", \"bottom:-70px\")" });
            }
            catch (Exception) { }

            if (_sensor != null)
            {
                try {
                    await chatWebView.InvokeScriptAsync("eval", new string[] { "document.body.style.zoom = \"200%\"" });
                }
                catch (Exception) { }
            }
        }

        private void StreamPlayer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (_sensor == null)
            {
                if (!StreamPlayer.IsFullWindow)
                {
                    StreamPlayer.IsFullWindow = true;
                }
                else
                {
                    StreamPlayer.IsFullWindow = false;
                }
            }
        }


        async private void OrientationChanged(object sender, SimpleOrientationSensorOrientationChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                DisplayOrientation(e.Orientation);
            });
        }

        private void DisplayOrientation(SimpleOrientation orientation)
        {
            switch (orientation)
            {
                case SimpleOrientation.NotRotated:
                    StreamPlayer.IsFullWindow = false;
                    break;
                case SimpleOrientation.Rotated90DegreesCounterclockwise:
                    StreamPlayer.IsFullWindow = true;
                    break;
                case SimpleOrientation.Rotated180DegreesCounterclockwise:
                    StreamPlayer.IsFullWindow = false;
                    break;
                case SimpleOrientation.Rotated270DegreesCounterclockwise:
                    StreamPlayer.IsFullWindow = true;
                    break;
                case SimpleOrientation.Faceup:
                    break;
                case SimpleOrientation.Facedown:
                    break;
                default:
                    break;
            }
        }
    }
}
