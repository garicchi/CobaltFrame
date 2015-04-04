using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;

namespace CobaltFrame.Mac
{
	public partial class AppDelegate : NSApplicationDelegate
	{
		MainGame _game;

		public AppDelegate ()
		{
		}

		public override void FinishedLaunching (NSObject notification)
		{
			this._game = new MainGame ();
			this._game.Run ();
		}
	}
}

