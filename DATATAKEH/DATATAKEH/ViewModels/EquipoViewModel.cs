using DATATAKEH.Models;
using DATATAKEH.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Windows.Input;

namespace DATATAKEH.ViewModels
{
    public class EquipoViewModel
    {
        #region Attributes

        private NavigationService navigationService;

        private DialogService dialogService;

        private DataService dataService;

        private ApiService apiService;

        public Equipo equipo;

        private int resultado;

        #endregion

        #region Singleton

        static EquipoViewModel instance;

        public static EquipoViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new EquipoViewModel();
            }

            return instance;
        }

        #endregion

        #region Constructors

        public EquipoViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            dataService = new DataService();
            apiService = new ApiService();
            equipo = new Equipo();
            instance = this;
        }

        #endregion


        #region Properties
        public int EquipoIdLocal { get; set; }

        public int EquipoId { get; set; }

        public string Condicion { get; set; }

        public string IsAmplificador { get; set; }

        public string IsFuente { get; set; }

        public string IsCaja { get; set; }

        public string IsEnergia { get; set; }

        public string OtroEquipo { get; set; }

        public string CodigoPlaca { get; set; }

        public int VanoId { get; set; }

        public bool Estado { get; set; }

        #endregion

        #region Commands

        public ICommand EquipoCommand1 { get { return new RelayCommand(Equipo1); } }

        private async void Equipo1()
        {
            if (string.IsNullOrEmpty(Condicion))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Condición");
                return;
            }
            if (string.IsNullOrEmpty(IsAmplificador))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar si hay amplificador");
                return;
            }
            if (string.IsNullOrEmpty(IsFuente))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar si hay fuente");
                return;
            }
            if (string.IsNullOrEmpty(IsCaja))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar si hay caja");
                return;
            }
            /*var resulVano = dataService.Get<Vano>(true).OrderByDescending(a => a.VanoId).FirstOrDefault();
            equipo.VanoId = resulVano.VanoId;*/
            var vanoViewModel = VanoViewModel.GetInstance();
            var tipoVano = vanoViewModel.TipoVano;
            var reserva = vanoViewModel.Reserva;
            var longitudVano = vanoViewModel.LongitudVano;
            var tipoCableRed = vanoViewModel.TipoCableRed;
            var tipoCableComunicacion = vanoViewModel.TipoCableComunicacion;
            var resulVano = dataService.Get<Vano>(false).Where(a => a.TipoVano == tipoVano &&
                                                               a.Reserva == reserva &&
                                                               a.LongitudVano == longitudVano &&
                                                               a.TipoCableRed == tipoCableRed &&
                                                               a.TipoCableComunicacion == tipoCableComunicacion);
            foreach (var r in resulVano)
            {
                resultado = r.VanoIdLocal;
            }
            equipo.VanoIdLocal = resultado;
            equipo.Condicion = Condicion;
            equipo.IsAmplificador = IsAmplificador;
            equipo.IsFuente = IsFuente;
            equipo.IsCaja = IsCaja;
            await navigationService.Navigate("SeventhPage");
        }

        public ICommand EquipoCommand2 { get { return new RelayCommand(Equipo2); } }

        private async void Equipo2()
        {
            if (string.IsNullOrEmpty(IsEnergia))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar si consume energía");
                return;
            }
            if (string.IsNullOrEmpty(OtroEquipo))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar otro equipo si hay");
                return;
            }
            if (string.IsNullOrEmpty(CodigoPlaca))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar código de placa");
                return;
            } 

            equipo.IsEnergia = IsEnergia;
            equipo.OtroEquipo = OtroEquipo;
            equipo.CodigoPlaca = CodigoPlaca;

            /*
            try
            {
                var response = await apiService.PostEquipo(Condicion, IsAmplificador, IsFuente, IsCaja, 
                                                         IsEnergia,OtroEquipo, CodigoPlaca, equipo.VanoId);
                var equipoService = (Equipo)response.Result;
                equipo.EquipoId = equipoService.EquipoId;
                EquipoId = equipo.EquipoId;
                dataService.InsertEquipo(equipo);
                await navigationService.Navigate("NinthPage");
            }
            catch (Exception e)
            {
                var error = e.Message.ToString();
            } */
            equipo.Estado = false;
            dataService.InsertEquipo(equipo);
            await navigationService.Navigate("NinthPage");
        }

        #endregion
    }
}
