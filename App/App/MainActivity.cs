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
using Android.Provider;
//using Android.OS;
namespace App
{
	[Activity (Label = "MySecurity", MainLauncher = true)]
	public class MainActivity : Activity,ILocationListener
	{
		private Location _currentLocation;
		private LocationManager _locationManager;
		private TextView _latitudeText;
		private TextView _longitudeText;
		//private TextView _addressText;
		//private TextView _timeText;
		private string _locationProvider;
		protected MediaPlayer player;
		//private double currLatitude;
		//private Location currLongitude;
		//List<string> l;
		int count;



		protected override void OnCreate(Bundle bundle)
		{
			player = MediaPlayer.Create (this,Resource.Raw.police_alarm);
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			//_addressText = FindViewById<TextView>(Resource.Id.textView3);
			_latitudeText = FindViewById<TextView>(Resource.Id.txtLatitude);
			_longitudeText = FindViewById<TextView> (Resource.Id.txtLongitude);
			_locationManager = (LocationManager)GetSystemService(LocationService);

			var criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Medium
			};
			var acceptableLocationProviders = _locationManager.GetProviders (criteriaForLocationService, false);
			Location l=_locationManager.GetLastKnownLocation(acceptableLocationProviders.First());

			if (l == null) {
				_latitudeText.Text = 17.4417641.ToString();
				_longitudeText.Text = 78.3807515.ToString();
			} else {
				_latitudeText.Text = l.Latitude.ToString();
				_longitudeText.Text = l.Longitude.ToString();
			}

//			FindViewById<TextView>(Resource.Id.address_button).Click += AddressButton_OnClick;
//			_addressText = FindViewById<TextView> (Resource.Id.address_button);
//			Address ();
			//FindViewById<TextView>(Resource.Id.add).Click += AddressButton_OnClick;
			//_timeText = FindViewById<TextView> (Resource.Id.txtSpeed);
			FindViewById<TextView>(Resource.Id.button1).Click += CallPolice_OnClick;
			FindViewById<TextView> (Resource.Id.button2).Click += alarm_click;
			FindViewById<TextView> (Resource.Id.showPopup).Click += contact;


//				var sendsos = FindViewById<Button> (Resource.Id.button3);
//			sendsos.Click += (sender, e) => {
//				if(_latitudeText.Text=="" && _longitudeText.Text=="")
//				{
//					Toast.MakeText(this,"Gps is looking for your latitude and longitude positions kindly hold on",ToastLength.Short).Show();
//				}
//				else{
//				SmsManager.Default.SendTextMessage("1234567890", null,"http://maps.google.com/maps?q="+_currentLocation.Latitude+","+_currentLocation.Longitude, null, null);
//				}
//				//SmsManager.Default.SendTextMessage(
//			}; 



			//	FindViewById<TextView> (Resource.Id.button4).Click += cell_loc;


			var sendsosIntent = FindViewById<Button> (Resource.Id.button3);

            
			sendsosIntent.Click += (sender, e) => {
//				double lat=_currentLocation.Latitude;
//				double lon=_currentLocation.Longitude;
				PopupMenu menu=new PopupMenu(this,sendsosIntent);
				menu.Inflate(Resource.Menu.Main);
				menu.MenuItemClick += (s1, arg1) => {
					//Console.WriteLine ("{0} selected", arg1.Item.TitleFormatted);
					//ISharedPreferences sh=GetSharedPreferences("Contacts",
					ISharedPreferences p = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
//						var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
//						var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	
					// 	ISharedPreferencesEditor e1 = p.Edit ();
//						e.PutString (name, number);
//						e.Commit ();
					String val=p.GetString("name_contact","");
					//	String val = p.GetString ("name", "");
						Toast.MakeText (this, val, ToastLength.Short).Show();
				};

				// Android 4 now has the DismissEvent
				menu.DismissEvent += (s2, arg2) => {
					Console.WriteLine ("menu dismissed"); 
				};

				menu.Show ();
				if(_latitudeText.Text=="")
				{
					Toast.MakeText(this,"Gps is looking for your latitude and longitude positions kindly hold on",ToastLength.Short).Show();
				}
				else {
					_locationManager = (LocationManager)GetSystemService(LocationService);

					var criteriaForLocationService1 = new Criteria
					{
						Accuracy = Accuracy.Medium
					};
					var acceptableLocationProviders1 = _locationManager.GetProviders (criteriaForLocationService1, false);
					Location l1=_locationManager.GetLastKnownLocation(acceptableLocationProviders1.First());



					string lo="http://maps.google.com/maps?q="+l1.Latitude+","+l1.Longitude+"&mode=driving";
					//string locationString = @"http://maps.google.com/maps/api/staticmap?center=" + _currentLocation.Latitude + "," + _currentLocation.Longitude;
					//var i=new Intent(Intent.ActionView,Android.Net.Uri.Parse(lo));
					//var intent=new Intent(Intent.ActionView,Android.Net.Uri.Parse("http://maps.google.com/maps?q=loc:56.3245,-3.24567"));

				//StartActivity(i);


//				var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
//				var smsIntent = new Intent (Intent.ActionSendto, smsUri);
//					smsIntent.PutExtra("address","jelly");
//				smsIntent.PutExtra("sms_body",lo);
			
					//StartActivity (smsIntent);

					ISharedPreferences p = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
					String val=p.GetString("number","");
					ISharedPreferences p2 = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
					String val2=p2.GetString("number2","");
					var s=val;
					var s2=val2;

					Intent i=new Intent(Android.Content.Intent.ActionView);
					i.PutExtra("address",s+";"+s2);
					i.PutExtra("sms_body","hey!");
					i.SetType("vnd.android-dir/mms-sms");
					StartActivity(i);




			}
				/*	var smsUri1=Android.Net.Uri.Parse("smsto:378437483");
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





//		async void AddressButton_OnClick(object sender, EventArgs eventArgs)
//		{
//			if (_currentLocation == null)
//			{
//				_addressText.Text = "Can't determine the current address.";
//				return;
//			}
//
//			Geocoder geocoder = new Geocoder(this);
//			IList<Address> addressList = await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);
//
//			Address address = addressList.FirstOrDefault();
//			if (address != null)
//			{
//				StringBuilder deviceAddress = new StringBuilder();
//				for (int i = 0; i < address.MaxAddressLineIndex; i++)
//				{
//					deviceAddress.Append(address.GetAddressLine(i))
//						.AppendLine(",");
//				}
//				_addressText.Text = deviceAddress.ToString();
//			}
//			else
//			{
//				_addressText.Text = "Unable to determine the address.";
//			}
//		}








//		private async void Address(){
//
//
//		//address newly added
////		async void AddressButton_OnClick(object sender, EventArgs eventArgs)
////		{
//			if (_currentLocation == null)
//			{
//				_addressText.Text = "Can't determine the current address.";
//				return;
//			}
//
//			Geocoder geocoder = new Geocoder(this);
//			IList<Address> addressList = await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);
//
//			Address address = addressList.FirstOrDefault();
//			if (address != null)
//			{
//				StringBuilder deviceAddress = new StringBuilder();
//				for (int i = 0; i < address.MaxAddressLineIndex; i++)
//				{
//					deviceAddress.Append(address.GetAddressLine(i))
//						.AppendLine(",");
//				}
//				_addressText.Text = deviceAddress.ToString();
//			}
//			else
//			{
//				_addressText.Text = "Unable to determine the address.";
//			}
//				//}
//		}


		private void Sos_Click(object sender,EventArgs args)
		{
			//currLatitude = Double.Parse (_currentLocation);
			//currLatitude=string.Format("{0}", _currentLocation.Latitude);
			//currLatitude = _currentLocation.Latitude.ToString();
			//	currLongitude = _currentLocation.Longitude.ToString();
			//	var geouri = Android.Net.Uri.Parse ("geo:",+currLatitude+currLongitude);
			//var mapIntent = new Intent (Intent.ActionView, geoUri);
			//StartActivity (mapIntent);
//			if (_latitudeText.Text == "") {
//
//			}


			var smsUri = Android.Net.Uri.Parse("smsto:1234567890");
			var smsIntent = new Intent (Intent.ActionSendto, smsUri);
			smsIntent.PutExtra("please help!",_currentLocation);
			//smsIntent.PutExtra ("sms_body", _currentLocation);  
			StartActivity (smsIntent);
		}


		private void InitializeLocationManager()
		{
			_locationManager = (LocationManager)GetSystemService(LocationService);
			//var status = _locationManager.GetGpsStatus (null).TimeToFirstFix;
			//Toast.MakeText (this, status, Toas).Show ();
			var criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Medium
			};
			var acceptableLocationProviders = _locationManager.GetProviders (criteriaForLocationService, false);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
//				Location ll=_locationManager.GetLastKnownLocation (LocationManager.NetworkProvider);
//				_latitudeText.Text = ll.Latitude.ToString ();
				//OnLocationChanged ();
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






		private void contact(object s,EventArgs e)
		{
			var intent = new Intent(Intent.ActionPick, ContactsContract.Contacts.ContentUri);
			var intent2 = new Intent (Intent.ActionPick, ContactsContract.CommonDataKinds.Phone.ContentUri);
			//	StartActivityForResult(intent, 100);
			StartActivityForResult(intent2,110);
		}





		private void  CallPolice_OnClick(object sender, EventArgs eventArgs)
		{
			var uri = Android.Net.Uri.Parse ("tel:100");
			var intent = new Intent (Intent.ActionView, uri);  
			StartActivity (intent);
		}
		private void alarm_click(object s,EventArgs ea)
		{
			StartActivity (typeof(AddContacts));
			//	player.Start();
			//StartPlayer (@"C:\Users\sonal.pathak\Desktop\Applications\Pocket_Security-master\Pocket_Security-master\Pocket Security\police_alarm.mp3");
		}
//		private void cell_loc(object s,EventArgs ea)
//		{
////			Intent i = new Intent ("C:\\Users\\sonal.pathak\\Documents\\Projects\\AndroidTabLayout\\AndroidTabLayout\\MainActivity.cs");
////			StartActivity (i);
//			//PackageManager pk = getPackageManager ();
//
//
//			Intent i4=new Intent(Intent.ActionMain);
//
////			PackageManager manager;
////			manager.GetPackageGids ("AndroidTablayout.AndroidTabLayout");
////			manager.GetLaunchIntentForPackage("AndroidTabLayout.AndroidTabLayout");
////			i4.AddCategory (Intent.CategoryLauncher);
////			StartActivity (i4);
//
//			//i4 = manager.getLaunchIntentForPackage("com.apk");//apk name
//
//			//i4.addCategory(Intent.CATEGORY_LAUNCHER);
//
//			//startActivity(i4);
//		}

		public void OnLocationChanged(Location location)
		{
			_currentLocation = location;
			if (_currentLocation == null)
			{
				_latitudeText.Text = "Unable to determine your location.";
				_longitudeText.Text = "";


				//new code
//				Geocoder geocoder = new Geocoder(this);
//				IList<Address> addressList =  await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);
//
//				Address address = addressList.FirstOrDefault();
//				if (address != null)
//				{
//					StringBuilder deviceAddress = new StringBuilder();
//					for (int i = 0; i < address.MaxAddressLineIndex; i++)
//					{
//						deviceAddress.Append(address.GetAddressLine(i))
//							.AppendLine(",");
//					}
//					_address.Text = deviceAddress.ToString();
//				}
//				else
//				{
//					_address.Text = "Unable to determine the address.";
//				}
//




			}
			else
			{
				//float bearing_degree = _currentLocation.HasBearing;
				//string direction=
				_latitudeText.Text = String.Format("{0}", _currentLocation.Latitude);
				_longitudeText.Text = string.Format ("{0}",_currentLocation.Longitude);
				//_timeText.Text = string.Format ("{0}",_currentLocation.Bearing);
				//var geoUri = Android.Net.Uri.Parse ("geo:42.374260,-71.120824");
				//var mapIntent = new Intent (Intent.ActionView, geoUri);
				//StartActivity (mapIntent);
			}
		}

		public void OnProviderDisabled(string provider) { }

		public void OnProviderEnabled(string provider) {
			Toast.MakeText (this, "kajdnjncd dmc ", ToastLength.Long);
		 }

		public void OnStatusChanged(string provider, Availability status, Bundle extras) { }
		public void OnCompletion(MediaPlayer p)
		{
			p.Stop ();
			p.Release ();
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			// Request 150 comes from inserting or editing a new contact


			// Request 100 comes from contact picker
			if(requestCode == 100)
			{

//			
//	
				if (data != null)
				{
					var cursor1 = ManagedQuery(data.Data, null, null, null, null);
					if(cursor1.Count > 0)
					{
						cursor1.MoveToNext();
						//Toast.MakeText(this, "Got contact " + cursor1.GetString(cursor1.GetColumnIndex(ContactsContract.ContactsColumns.DisplayName)), ToastLength.Long).Show();		
					}	
				}
				else
				{
					Toast.MakeText(this, "No contact picked", ToastLength.Long).Show();
				}
			}

			if (requestCode == 110) {
				//count = 0;
				if (data != null)
				{
					var cursor2 = ManagedQuery(data.Data, null, null, null, null);
					if(cursor2.Count > 0)
					{


						cursor2.MoveToNext();
						Toast.MakeText(this, "Got contact " +cursor2.GetString(cursor2.GetColumnIndex(ContactsContract.CommonDataKinds.Phone.Number))+ cursor2.GetString(cursor2.GetColumnIndex(ContactsContract.ContactsColumns.DisplayName)), ToastLength.Short).Show();	

						if (count == 0) {
							ISharedPreferences p = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
							var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
							var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	


							ISharedPreferencesEditor e = p.Edit ();
							e.PutString (name, number);
							e.PutString ("name_contact", name);
							e.PutString ("number", number);
							e.Commit ();
							String val = p.GetString (name, "");
							Toast.MakeText (this, val + name, ToastLength.Short).Show ();
						
							//break;
						} 
						if(count==1) {
							ISharedPreferences p = GetSharedPreferences ("Contacts2", FileCreationMode.WorldReadable);
							var name = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.ContactsColumns.DisplayName));
							var number = cursor2.GetString (cursor2.GetColumnIndex (ContactsContract.CommonDataKinds.Phone.Number));	


							ISharedPreferencesEditor e = p.Edit ();
							e.PutString (name, number);
							e.PutString ("name_contact", name);
							e.PutString ("number2", number);
							e.Commit ();
							String val = p.GetString (name, "");
							Toast.MakeText (this, val + name, ToastLength.Short).Show ();
						
							Toast.MakeText (this, "count is now 1", ToastLength.Short).Show();

						}

					



					}	
					count++;
				}
				else
				{
					Toast.MakeText(this, "No contact picked", ToastLength.Long).Show();
				}
			}
		}








	}
}


