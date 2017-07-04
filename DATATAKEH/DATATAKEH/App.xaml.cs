using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using DATATAKEH.Pages;
using DATATAKEH.Services;
using DATATAKEH.Models;

namespace DATATAKEH
{
    public partial class App : Application
    {
        #region Attributes

        private DataService dataService;

        #endregion

        #region Properties
        //public static NavigationPage Navigator { get; internal set; }
        public static INavigation Navigator { get; internal set; }
        public static User CurrentUser { get; internal set; }

        #endregion

        public App()
        {
            InitializeComponent();

            dataService = new DataService();
            var user = dataService.GetUser();
            if( user!= null && user.IsRemembered)
            {
                App.CurrentUser = user;
                App.Current.MainPage = new NavigationPage(new FirstPage());
            }
            else
            {
                MainPage = new LoginPage();
            }

            

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
