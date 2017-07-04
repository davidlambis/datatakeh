using DATATAKEH.Models;
using DATATAKEH.Pages;
using DATATAKEH.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DATATAKEH.Services
{
    public class NavigationService
    {
        private DataService dataService;

        public NavigationService()
        {
            dataService = new DataService();
        }

        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "FirstPage":
                    App.Current.MainPage = new NavigationPage(new FirstPage());
                    break;
                case "SecondPage":
                    await App.Navigator.PushAsync(new SecondPage());
                    break;
                case "ThirdPage":
                    await App.Navigator.PushAsync(new ThirdPage());
                    break;
                case "FourthPage":
                    await App.Navigator.PushAsync(new FourthPage());
                    break;
                case "FifthPage":
                    await App.Navigator.PushAsync(new FifthPage());
                    break;
                case "SixthPage":
                    await App.Navigator.PushAsync(new SixthPage());
                    break;
                case "SeventhPage":
                    await App.Navigator.PushAsync(new SeventhPage());
                    break;
                case "FotoPage":
                    await App.Navigator.PushAsync(new FotoPage());                            
                    break;
                case "NinthPage":
                    await App.Navigator.PushAsync(new NinthPage());
                    break;
                case "MapPage":
                    await App.Navigator.PushAsync(new MapPage());
                    break;
                case "TenthPage":
                    await App.Navigator.PushAsync(new TenthPage());
                    break;
                case "EleventhPage":
                    await App.Navigator.PushAsync(new EleventhPage());
                    break;
                case "LoginPage":
                    App.Current.MainPage = new LoginPage();
                    break;
                case "LogoutPage":
                    Logout();
                    break;
                default:
                    break;
            }
        }

        private void Logout()
        {
            App.CurrentUser.IsRemembered = false;
            dataService.UpdateUser(App.CurrentUser);
            var projectViewModel = ProjectViewModel.GetInstance();
            projectViewModel.ProjectName = "";
            projectViewModel.Ciudad = "";
            projectViewModel.EmpresaPropietaria = "";
            projectViewModel.EmpresaOperadora = "";
            var posteViewModel = PosteViewModel.GetInstance();
            posteViewModel.CodigoApoyo = "";
            posteViewModel.Condicion = "";
            posteViewModel.Material = "";
            posteViewModel.LongitudPoste = "";
            posteViewModel.ResistenciaMecanica = "";
            posteViewModel.Estado = "";
            posteViewModel.CantidadRetenidas = "";
            posteViewModel.Propiedad = "";
            posteViewModel.NivelTension = "";
            posteViewModel.AlturaDisponible = "";
            posteViewModel.AlturaMontaje = "";
            posteViewModel.TipoEstructura = "";
            posteViewModel.RedesBT = "";
            posteViewModel.Retenidas = "";
            posteViewModel.CablesOperador = "";
            posteViewModel.CablesComunicacionFinal = "";
            var vanoViewModel = VanoViewModel.GetInstance();
            vanoViewModel.TipoVano = "";
            vanoViewModel.Reserva = "";
            vanoViewModel.LongitudVano = "";
            vanoViewModel.TipoCableRed = "";
            vanoViewModel.TipoCableComunicacion = "";
            var equipoViewModel = EquipoViewModel.GetInstance();
            equipoViewModel.Condicion = "";
            equipoViewModel.IsAmplificador = "";
            equipoViewModel.IsFuente = "";
            equipoViewModel.IsCaja = "";
            equipoViewModel.IsEnergia = "";
            equipoViewModel.OtroEquipo = "";
            equipoViewModel.CodigoPlaca = "";
            var directionViewModel = DirectionViewModel.GetInstance();
            directionViewModel.Departamento = "";
            directionViewModel.Municipio = "";
            directionViewModel.Barrio = "";
            directionViewModel.Direccion = "";
            directionViewModel.Observaciones = "";
            App.Current.MainPage = new LoginPage();

        }

        internal void setMainPage(User user)
        {
            App.CurrentUser = user;
            App.Current.MainPage = new NavigationPage(new FirstPage());
        }
    }
}

