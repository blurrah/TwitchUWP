using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TwitchUWP.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TwitchUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VideoPage : Page
    {

        public ObservableCollection<Message> chatMessages { get; set; }

        public VideoPage()
        {
            this.InitializeComponent();

            var manager = SystemNavigationManager.GetForCurrentView();
            manager.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            chatMessages = new ObservableCollection<Message>();

            RenderMessage("Jasper", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus et quam id ex tempus varius non et mi.");
        }

        private void RenderMessage(string name, string content)
        {
            TextBlock tb = new TextBlock();
            tb.Margin = new Thickness(0, 0, 0, 5);
            tb.TextWrapping = TextWrapping.WrapWholeWords;

            Run renderName = new Run();
            renderName.Text = name + ": ";
            renderName.FontWeight = FontWeights.Bold;
            renderName.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x64, 0x41, 0xA5));

            Run renderContent = new Run();
            renderContent.Text = content;

            tb.Inlines.Add(renderName);
            tb.Inlines.Add(renderContent);

            ChatMessageView.Children.Add(tb);
            ChatScrollViewer.ChangeView(0.0f, ChatScrollViewer.ExtentHeight, 1.0f);
        }
    }
}
