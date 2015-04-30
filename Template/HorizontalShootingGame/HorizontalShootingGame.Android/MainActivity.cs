using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;

namespace HorizontalShootingGame.Android
{
	[Activity(Label = "CobaltFrame.Android.Test"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, AlwaysRetainTaskState = true
		, Theme="@android:style/Theme.Black.NoTitleBar.Fullscreen"
		, LaunchMode = LaunchMode.SingleInstance
		, ScreenOrientation = ScreenOrientation.Landscape
		, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
	public class MainActivity : Microsoft.Xna.Framework.AndroidGameActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			
			base.OnCreate(bundle);
			var g = new MainGame();
			this.Window.AddFlags (WindowManagerFlags.Fullscreen);
			SetContentView((View)g.Services.GetService(typeof(View)));


			g.Run();
		}


	}
}


