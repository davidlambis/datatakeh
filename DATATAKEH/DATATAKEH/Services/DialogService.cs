using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Services
{
    public class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
            //Coge la activa, es decir en la que está, no el MainPage como tal
            await App.Current.MainPage.DisplayAlert(title, message, "Aceptar");
        }
    }
}
    