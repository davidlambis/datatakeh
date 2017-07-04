using DATATAKEH.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DATATAKEH.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private NavigationService navigationService;

        public LoginPage()
        {
            InitializeComponent();
            navigationService = new NavigationService();
        }

       


    }
}