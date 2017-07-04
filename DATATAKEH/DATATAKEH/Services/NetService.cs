using DATATAKEH.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DATATAKEH.Services
{
    public class NetService
    {
        public bool IsConnected()
        {
            var networkConnection = DependencyService.Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();
            var networkStatus = networkConnection.IsConnected;
            return networkStatus;
        }
    }
}
