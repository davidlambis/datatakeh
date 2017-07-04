using DATATAKEH.Models;
using DATATAKEH.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DATATAKEH.ViewModels
{
    public class DirectionViewModel
    {
        #region Attributes

        private NavigationService navigationService;

        private DialogService dialogService;

        private DataService dataService;

        private ApiService apiService;

        public Direction direction;

       

        public int resultado;

        #endregion

        #region Singleton

        static DirectionViewModel instance;

        public static DirectionViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new DirectionViewModel();
            }

            return instance;
        }

        #endregion
        
        #region Constructors

        public DirectionViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            dataService = new DataService();
            apiService = new ApiService();
            direction = new Direction();
            instance = this;
        }

        #endregion

        #region Properties

        public int DirectionIdLocal { get; set; }

        public int DirectionId { get; set; }

        public string Departamento { get; set; }

        public string Municipio { get; set; }

        public string Barrio { get; set; }

        public string Direccion { get; set; }

        public string Observaciones { get; set; }

        public bool Estado { get; set; }

        public int EquipoIdLocal { get; set; }
        #endregion

        #region Commands

        public ICommand DirectionCommand { get { return new RelayCommand(Address); } }

        private async void Address()
        {
            if (string.IsNullOrEmpty(Departamento))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Departamento");
                return;
            }
            if (string.IsNullOrEmpty(Municipio))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Municipio");
                return;
            }
            if (string.IsNullOrEmpty(Barrio))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Barrio");
                return;
            }
            if (string.IsNullOrEmpty(Direccion))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Dirección");
                return;
            }
            if (string.IsNullOrEmpty(Observaciones))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Observaciones");
                return;
            } 
            /*var resulEquipo = dataService.Get<Equipo>(true).OrderByDescending(a => a.EquipoId).FirstOrDefault();
            direction.EquipoIdLocal = resulEquipo.EquipoIdLocal;*/
            var equipoViewModel = EquipoViewModel.GetInstance();
            var codigoPlaca = equipoViewModel.CodigoPlaca;
            var resulEquipo = dataService.Get<Equipo>(false).Where(a => a.CodigoPlaca == codigoPlaca);
            foreach(var e in resulEquipo)
            {
                resultado = e.EquipoIdLocal;
            }
            direction.EquipoIdLocal = resultado;
            direction.Departamento = Departamento;
            direction.Municipio = Municipio;
            direction.Barrio = Barrio;
            direction.Direccion = Direccion;
            direction.Observaciones = Observaciones;
            /*await geolocatorMapService.geoLocator();
            direction.Latitud = geolocatorMapService.Latitude;
            direction.Longitud = geolocatorMapService.Longitude;*/

            /*
            try
            {
                var response = await apiService.PostDirection(Departamento, Municipio,Barrio, Direccion, 
                                                                Georreferenciacion, Observaciones, resulEquipo.EquipoId);
                var directionService = (Direction)response.Result;
                direction.DirectionId = directionService.DirectionId;
                DirectionId = direction.DirectionId;
                dataService.InsertDirection(direction);
            }
            catch (Exception e)
            {
                var error = e.Message.ToString();
            } */

            direction.Estado = false;
            dataService.InsertDirection(direction);
            await navigationService.Navigate("FotoPage");
        }

        #endregion
    }

}

