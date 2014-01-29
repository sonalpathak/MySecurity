using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;



using Android.Locations;
using Android.Util;




namespace App
{
	[Activity (Label = "alarm")]			
	public class alarm : Activity
	{
		protected MediaPlayer _player;
		protected override void OnCreate (Bundle bundle)
		{
			_player = MediaPlayer.Create (this,Resource.Raw.police_alarm);
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.alarm);
			ImageButton start = FindViewById<ImageButton> (Resource.Id.Start_ALarm);
			start.Click += delegate {
				_player = MediaPlayer.Create (this,Resource.Raw.police_alarm);
				_player.Start ();

			
			};
			ImageButton stop = FindViewById<ImageButton> (Resource.Id.Stop_ALarm);
			stop.Click += delegate {
				_player.Stop ();
				_player.Release();
				_player=null;
//				Intent i = new Intent (this, typeof(MainActivity));
//				StartActivity (i);
			};
			//	FindViewById<TextView> (Resource.Id.Start_ALarm).Click+=Start_alarm_click;
			//FindViewById<TextView> (Resource.Id.Stop_ALarm).Click += Stop_alarm_click;


		}




//		protected override void OnResume()
//		{
//			base.OnResume ();
//			Button start = FindViewById<Button> (Resource.Id.Start_ALarm);
//			start.Click += delegate {
//				_player.Start ();
//
//
//			};
//			Button stop = FindViewById<Button> (Resource.Id.Stop_ALarm);
//			stop.Click += delegate {
//				_player.Stop ();
//				_player.Release();
//				_player=null;
//			};
//		}

//		protected override void OnRestart()
//		{
//			base.OnRestart ();
//			Button start = FindViewById<Button> (Resource.Id.Start_ALarm);
//			start.Click += delegate {
//				_player.Start ();
//
//
//			};
//			Button stop = FindViewById<Button> (Resource.Id.Stop_ALarm);
//			stop.Click += delegate {
//				_player.Stop ();
//				_player.Release();
//				_player=null;
//			};
//
//		}
		//FindViewById<TextView> (Resource.Id.Start_ALarm).Click+=Start_alarm_click;
		public void Start_alarm_click(object sender, EventArgs eventArgs)
		{
			_player.Start ();

		} 
		public void Stop_alarm_click(object sender, EventArgs eventArgs)
		{
			_player.Stop ();
			_player.Release();
			_player=null;
			Intent i = new Intent (this, typeof(MainActivity));
			StartActivity (i);
		}

	}
}

