using UIKit;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Todo
{
	[Register("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

#if DEBUG
Xamarin.Calabash.Start();
#endif
            // affects all UISwitch controls in the app
            UISwitch.Appearance.OnTintColor = UIColor.FromRGB(0x91, 0xCA, 0x47);

			Forms.Init();
            Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) => {
                // http://developer.xamarin.com/recipes/testcloud/set-accessibilityidentifier-ios/
                if (null != e.View.AutomationId)
                {
                    e.NativeView.AccessibilityIdentifier = e.View.AutomationId;
                }
            };
            LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}
	}
}