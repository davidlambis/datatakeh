using DATATAKEH.Models;
using DATATAKEH.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Windows.Input;

namespace DATATAKEH.ViewModels
{
    public class VanoViewModel
    {
        #region Attributes

        private NavigationService navigationService;

        private DialogService dialogService;

        private DataService dataService;

        public Vano vano;

        private ApiService apiService;

        private int resultado;

        #endregion

        #region Singleton

        static VanoViewModel instance;

        public static VanoViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new VanoViewModel();
            }

            return instance;
        }

        #endregion

        #region Constructors

        public VanoViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            dataService = new DataService();
            apiService = new ApiService();
            vano = new Vano();
            instance = this;
        }

        #endregion

        #region Properties
        public int VanoIdLocal { get; set; }
        
        public int VanoId { get; set; }

        public string TipoVano { get; set; }

        public string Reserva { get; set; }

        public string LongitudVano { get; set; }

        public string TipoCableRed { get; set; }

        public string TipoCableComunicacion { get; set; }

        public bool Estado { get; set; }

        public int Poste_Id { get; set; }

        #endregion

        #region Commands

        public ICommand VanoCommand { get { return new RelayCommand(Vano); } }

        private async void Vano()
        { 
            if (string.IsNullOrEmpty(TipoVano))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Tipo de Vano");
                return;
            }
            if (string.IsNullOrEmpty(Reserva))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Reserva");
                return;
            }
            if (string.IsNullOrEmpty(LongitudVano))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Longitud del Vano");
                return;
            }
            if (string.IsNullOrEmpty(TipoCableRed))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Tipo de Cable de Red");
                return;
            }
            if (string.IsNullOrEmpty(TipoCableComunicacion))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Tipo de Cable de Comunicación");
                return;
            } 
            /*var resulPoste = dataService.Get<Poste>(true).OrderByDescending(a => a.Poste_Id).FirstOrDefault();
            vano.Poste_Id = resulPoste.Poste_Id;*/
            var posteViewModel = PosteViewModel.GetInstance();
            var codigoPoste = posteViewModel.CodigoApoyo;
            var resulPoste = dataService.Get<Poste>(false).Where(a => a.CodigoApoyo == codigoPoste);
            foreach (var r in resulPoste)
            {
                resultado = r.PosteIdLocal;
            }

            switch (TipoCableRed)
            {
                case "Aluminio Desnudo":
                    TipoCableRed = "ACSR";
                    break;
                case "Aluminio Aislado":
                    TipoCableRed = "ASC";
                    break;
                case "Trenzado":
                    TipoCableRed = "TRE";
                    break;
                case "Cobre Aislado":
                    TipoCableRed = "CUA";
                    break;
                case "Cobre Desnudo":
                    TipoCableRed = "CUD";
                    break;
                default:
                    break;
            }

            switch (TipoCableComunicacion)
            {
                case "Fibra":
                    TipoCableComunicacion = "FO";
                    break;
                case "Coaxial RG6":
                    TipoCableComunicacion = "RG6";
                    break;
                case "Coaxial RG11":
                    TipoCableComunicacion = "RG11";
                    break;
                case "Coaxial 500":
                    TipoCableComunicacion = ".500";
                    break;
                default:
                    break;
            }

            vano.PosteIdLocal = resultado;
            vano.TipoVano = TipoVano;
            vano.Reserva = Reserva;
            vano.LongitudVano = LongitudVano;
            vano.TipoCableRed = TipoCableRed;
            vano.TipoCableComunicacion = TipoCableComunicacion;

            /*try
            {

             var response = await apiService.PostVano(TipoVano, Reserva, LongitudVano, TipoCableRed,
                                                   TipoCableComunicacion, resulPoste.Poste_Id);         
             var vanoService = (Vano)response.Result;
             vano.VanoId = vanoService.VanoId;
             VanoId = vano.VanoId; 
             dataService.InsertVano(vano);


             await navigationService.Navigate("SixthPage");
            }
            catch (Exception e)
             {
                 var error = e.Message.ToString();
             } */
            vano.Estado = false;
            dataService.InsertVano(vano);
            await navigationService.Navigate("SixthPage");
        }

        #endregion
    }
}
