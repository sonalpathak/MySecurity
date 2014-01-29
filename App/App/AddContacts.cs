//using System;
//using Android.App;
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
using Xamarin.Contacts;
using Android.Telephony;
using Android.Content.PM;
using Android.Provider;

namespace App
{

	[Activity (Label = "AddContact", MainLauncher = false)]
	public class AddContacts : Activity
	{
//		ListView listview;
//		List<string> contactList;
		int count;
		TextView name1;
		TextView name2;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.contacts);
			Button save = FindViewById<Button> (Resource.Id.saveButton);
			Button cancel = FindViewById<Button> (Resource.Id.cancelButton);
			name1 = FindViewById<TextView> (Resource.Id.name1);
			name2 = FindViewById<TextView> (Resource.Id.name2);
			FindViewById<Button> (Resource.Id.contacts).Click+=contact;
			ISharedPreferences p = GetSharedPreferences ("Contacts", FileCreationMode.WorldReadable);
			String val=p.GetString("name_contact","");
			name1.Text = val;
//			contactList = new List<string> ();
//			var book = new AddressBook (this) {
//				PreferContactAggregation = true
//
//			};
//			foreach (Contact c in book) {
//				contactList.Add (c.DisplayName);
//			}
//			listview = FindViewById<ListView> (Resource.Id.listView);
//			listview.Adapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItemMultipleChoice, contactList);
//			listview.ChoiceMode = ChoiceMode.Multiple;
			save.Click += delegate {
				StartActivity(typeof(MainActivity));
//				
			};

			//Button save=FindViewById(Resource.Id.
		}
		private void contact(object s,EventArgs e)
		{
			var intent = new Intent(Intent.ActionPick, ContactsContract.Contacts.ContentUri);
			var intent2 = new Intent (Intent.ActionPick, ContactsContract.CommonDataKinds.Phone.ContentUri);
			//	StartActivityForResult(intent, 100);
			StartActivityForResult(intent2,110);
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
							name1.Text = val;



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
							var n = name;
							name2.Text = name+ val;

						}
						if (count == 2) {
							Toast.MakeText (this, "you cannot stored more than 2 contacts", ToastLength.Short).Show();
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

