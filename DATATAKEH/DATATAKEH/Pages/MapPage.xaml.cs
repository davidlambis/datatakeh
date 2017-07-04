using DATATAKEH.Interfaces;
using DATATAKEH.Services;
using DATATAKEH.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace DATATAKEH.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{

        private GeolocatorMapService geolocatorMapService;

        public MapPage()
        {
            InitializeComponent();
            geolocatorMapService = new GeolocatorMapService();
            Locator();
        }

        private async void Locator()
        {
            await geolocatorMapService.geoLocator();

            /// var locator = CrossGeolocator.Current;
            //  locator.DesiredAccuracy = 50;
            //Encuentra la posicion actual
            ///  var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            var position = new Position(geolocatorMapService.Latitude, geolocatorMapService.Longitude);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(.3)));
        }



    }
}