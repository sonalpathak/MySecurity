using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using Android.Media;
using Android.Telephony;
using Android.Content.PM;
//using Android.OS;
namespace App
{
	[Activity (Label = "App", MainLauncher = true)]
	public class MainActivity1: Activity,ILocationListener
	{
		private Location _currentLocation;
		private LocationManager _locationManager;
		private TextView _latitudeText;
		private TextView _longitudeText;
		//private TextView _timeText;
		private string _locationProvider;
		protected MediaPlayer player;
		//private double currLatitude;
		//private Location currLongitude;



		protected override void OnCreate(Bundle bundle)
		{
			player = MediaPlayer.Create (this,Resource.Raw.police_alarm);
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			//_addressText = FindViewById<TextView>(Resource.Id.textView3);
			_latitudeText = FindViewById<TextView>(Resource.Id.txtLatitude);
			_longitudeText = FindViewById<TextView> (Resource.Id.txtLongitude);
			//_timeText = FindViewById<TextView> (Resource.Id.txtSpeed);
			FindViewById<TextView>(Resource.Id.button1).Click += CallPolice_OnClick;
			FindViewById<TextView> (Resource.Id.button2).Click += alarm_click;

			var sendsos = FindViewById<Button> (Resource.Id.button3);
			//correct on click
			sendsos.Click += (sender, e) => {
				if (_latitudeText.Text == "") {
					_locationManager = (LocationManager)GetSystemService(LocationService);
					//var status = _locationManager.GetGpsStatus (null).TimeToFirstFix;
					//Toast.MakeText (this, status, Toas).Show ();
					var criteriaForLocationService = new Criteria
					{
						Accuracy = Accuracy.Fine
					};
					string bestProvider = _locationManager.GetBestProvider(criteriaForLocationService, false);
					//var acceptableLocationProviders = _locationManager.GetProviders (criteriaForLocationService, true);
					Location l = _locationManager.GetLastKnownLocation (bestProvider);
					_latitudeText.Text = l.Latitude.ToString ();
				}
				else{

					SmsManager.Default.SendTextMessage("1234567890", null,"http://maps.google.com/maps?q="+_currentLocation.Latitude+","+_currentLocation.Longitude, null, null);
					//SmsManager.Default.SendTextMessage(
				}
			}; 
			FindViewById<TextView> (Resource.Id.button4).Click += cell_loc;


			var sendsosIntent = FindViewById<Button> (Resource.Id.button3);

			sendsosIntent.Click += (sender, e) => {
				double lat=_currentLocation.Latitude;
				double lon=_currentLocation.Longitude;
				string lo="http://maps.google.com/maps?q="+_currentLocation.Latitude+","+_currentLocation.Longitude+"&mode=driving";
				string locationString = @"				http://maps.google.com/maps/api/staticmap?center=" + _currentLocation.Latitude + "," + _currentLocation.Longitude;
				var i=new Intent(Intent.ActionView,Android.Net.Uri.Parse(lo));
				var intent=new Intent(Intent.ActionView,Android.Net.Uri.Parse("http://maps.google.com/maps?q=loc:56.3245,-3.24567"));

				//StartActivity(i);
				var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
				var smsIntent = new Intent (Intent.ActionSendto, smsUri);
				smsIntent.PutExtra("sms_body",lo);
				StartActivity (smsIntent);

				/*					var smsUri1=Android.Net.Uri.Parse("smsto:378437483");
				var url=Android.Net.Uri.Parse(String.Format("http://maps.google.com/maps?q=\"+_currentLocation.Latitude+\",\"+_currentLocation.Longitude"));
				var smsIntent1=new Intent(Intent.ActionSendto,url);
				StartActivity(smsIntent1);*/



				/*var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
				var smsIntent = new Intent (Intent.ActionSendto, smsUri);
				var ValueType=_currentLocation.Latitude*1E6;
				smsIntent.PutExtra("sms_body",ValueType);
				StartActivity (smsIntent);*/
			};

			InitializeLocationManager();
		}

		private void Sos_Click(object sender,EventArgs args)
		{
			//currLatitude = Double.Parse (_currentLocation);
			//currLatitude=string.Format("{0}", _currentLocation.Latitude);
			//currLatitude = _currentLocation.Latitude.ToString();
			//	currLongitude = _currentLocation.Longitude.ToString();
			//	var geouri = Android.Net.Uri.Parse ("geo:",+currLatitude+currLongitude);
			//var mapIntent = new Intent (Intent.ActionView, geoUri);
			//StartActivity (mapIntent);
			if (_latitudeText.Text == "") {
				_locationManager = (LocationManager)GetSystemService(LocationService);
				//var status = _locationManager.GetGpsStatus (null).TimeToFirstFix;
				//Toast.MakeText (this, status, Toas).Show ();
				var criteriaForLocationService = new Criteria
				{
					Accuracy = Accuracy.Fine
				};
				var acceptableLocationProviders = _locationManager.GetProviders (criteriaForLocationService, true);
				Location l = _locationManager.GetLastKnownLocation ("acceptableLocationProviders");
				_latitudeText.Text = l.Latitude.ToString ();
			} else {
				var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
				var smsIntent = new Intent (Intent.ActionSendto, smsUri);
				smsIntent.PutExtra("please help!",_currentLocation);
				//smsIntent.PutExtra ("sms_body", _currentLocation);  
				StartActivity (smsIntent);


			}


		}


		private void InitializeLocationManager()
		{
			_locationManager = (LocationManager)GetSystemService(LocationService);
			//var status = _locationManager.GetGpsStatus (null).TimeToFirstFix;
			//Toast.MakeText (this, status, Toas).Show ();
			var criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			var acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				_locationProvider = String.Empty;
			}
		}

		protected override void OnResume()
		{
			base.OnResume();
			_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
		}

		protected override void OnPause()
		{
			base.OnPause();
			_locationManager.RemoveUpdates(this);
		}

		private void  CallPolice_OnClick(object sender, EventArgs eventArgs)
		{
			var uri = Android.Net.Uri.Parse ("tel:100");
			var intent = new Intent (Intent.ActionView, uri);  
			StartActivity (intent);
		}
		private void alarm_click(object s,EventArgs ea)
		{
			StartActivity (typeof(alarm));
			//	player.Start();
			//StartPlayer (@"C:\Users\sonal.pathak\Desktop\Applications\Pocket_Security-master\Pocket_Security-master\Pocket Security\police_alarm.mp3");
		}
		private void cell_loc(object s,EventArgs ea)
		{
//			Intent i = new Intent ("C:\\Users\\sonal.pathak\\Documents\\Projects\\AndroidTabLayout\\AndroidTabLayout\\MainActivity.cs");
//			StartActivity (i);
			//PackageManager pk = getPackageManager ();


			Intent i4=new Intent(Intent.ActionMain);

//			PackageManager manager;
//			manager.GetPackageGids ("AndroidTablayout.AndroidTabLayout");
//			manager.GetLaunchIntentForPackage("AndroidTabLayout.AndroidTabLayout");
//			i4.AddCategory (Intent.CategoryLauncher);
//			StartActivity (i4);

			//i4 = manager.getLaunchIntentForPackage("com.apk");//apk name

			//i4.addCategory(Intent.CATEGORY_LAUNCHER);

			//startActivity(i4);
		}

		public void OnLocationChanged(Location location)
		{
			_currentLocation = location;
			if (_currentLocation == null)
			{
				_latitudeText.Text = "Unable to determine your location.";
				_longitudeText.Text = "";
			}
			else
			{
				//float bearing_degree = _currentLocation.HasBearing;
				string direction=
					_latitudeText.Text = String.Format("{0}", _currentLocation.Latitude);
				_longitudeText.Text = string.Format ("{0}",_currentLocation.Longitude);
				//_timeText.Text = string.Format ("{0}",_currentLocation.Bearing);
				//var geoUri = Android.Net.Uri.Parse ("geo:42.374260,-71.120824");
				//var mapIntent = new Intent (Intent.ActionView, geoUri);
				//StartActivity (mapIntent);
			}
		}

		public void OnProviderDisabled(string provider) { }

		public void OnProviderEnabled(string provider) { }

		public void OnStatusChanged(string provider, Availability status, Bundle extras) { }
		public void OnCompletion(MediaPlayer p)
		{
			p.Stop ();
			p.Release ();
		}

	}
}




