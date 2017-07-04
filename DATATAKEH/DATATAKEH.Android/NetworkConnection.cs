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
using DATATAKEH.Interfaces;
using Android.Net;
using Xamarin.Forms;
using DATATAKEH.Droid;

[assembly: Dependency(typeof(NetworkConnection))]
namespace DATATAKEH.Droid
{
    public class NetworkConnection: INetworkConnection
    {

        public bool IsConnected { get; set; }
        public void CheckNetworkConnection()
        {
            var connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            if (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }
    }
}