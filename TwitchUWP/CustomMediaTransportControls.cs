using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TwitchUWP
{
    public sealed class CustomMediaTransportControls : MediaTransportControls
    {
        public CustomMediaTransportControls()
        {
            this.DefaultStyleKey = typeof(CustomMediaTransportControls);
        }

        protected override void OnApplyTemplate()
        {
            var chunkedItem = GetTemplateChild("chunked") as MenuFlyoutItem;
            chunkedItem.Click += (s, a) => VideoPage.Quality = "chunked";

            var highItem = GetTemplateChild("high") as MenuFlyoutItem;
            highItem.Click += (s, a) => VideoPage.Quality = "high";

            var mediumItem = GetTemplateChild("medium") as MenuFlyoutItem;
            mediumItem.Click += (s, a) => VideoPage.Quality = "medium";

            var lowItem = GetTemplateChild("low") as MenuFlyoutItem;
            lowItem.Click += (s, a) => VideoPage.Quality = "low";

            var mobileItem = GetTemplateChild("mobile") as MenuFlyoutItem;
            mobileItem.Click += (s, a) => VideoPage.Quality = "mobile";

            base.OnApplyTemplate();
        }
            
    }
}
