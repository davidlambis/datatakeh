using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Services
{
    public class GeolocatorMapService
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public async Task geoLocator()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                //Encuentra la posicion actual
                var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                Latitude = location.Latitude;
                Longitude = location.Longitude;

            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }
    }
}
