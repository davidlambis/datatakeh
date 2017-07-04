using DATATAKEH.Models;
using DATATAKEH.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Windows.Input;

namespace DATATAKEH.ViewModels
{
    public class PosteViewModel
    {

        #region Attributes

        private NavigationService navigationService;

        private DialogService dialogService;

        private DataService dataService;

        public Poste poste;

        private ApiService apiService;

        public GeolocatorMapService geolocatorMapService;

        private int resultado;

        #endregion

        #region Singleton

        static PosteViewModel instance;

        public static PosteViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new PosteViewModel();
            }

            return instance;
        }

        #endregion

        #region Constructors

        public PosteViewModel()
        {

            navigationService = new NavigationService();
            dialogService = new DialogService();
            dataService = new DataService();
            apiService = new ApiService();
            geolocatorMapService = new GeolocatorMapService();
            poste = new Poste();
            instance = this;
            /* var resul = dataService.Get<Poste>(true).OrderByDescending(a => a.NumeroApoyo).FirstOrDefault();
              NumeroApoyo = (resul.NumeroApoyo)+1; */
             
        }

        #endregion

        #region Properties

        public int PosteIdLocal { get; set; }

        public int Poste_Id { get; set; }

        public string CodigoApoyo { get; set; }

        public string Condicion { get; set; }

        public string Material { get; set; }

        public string LongitudPoste { get; set; }

        public string ResistenciaMecanica { get; set; }

        public string Estado { get; set; }

        public string CantidadRetenidas { get; set; }

        public string Propiedad { get; set; }

        public string NivelTension { get; set; }

        public string AlturaDisponible { get; set; }

        public string AlturaMontaje { get; set; }

        public string TipoEstructura { get; set; }

        public string RedesBT { get; set; }

        public string Retenidas { get; set; }

        public string CablesOperador { get; set; }

        public string CablesComunicacionFinal { get; set; }

        public bool EstadoSubida { get; set; }

        public int ProjectId { get; set; }
        #endregion

        #region Commands

        public ICommand Poste1Command { get { return new RelayCommand(Poste1); } }

        private async void Poste1()
        {
            if (string.IsNullOrEmpty(CodigoApoyo))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Código de Apoyo");
                return;
            }

            if (string.IsNullOrEmpty(Condicion))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Condición");
                return;
            }

            if (string.IsNullOrEmpty(Material))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Material");
                return;
            }

            if (string.IsNullOrEmpty(LongitudPoste))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Longitud del Poste");
                return;
            }

            if (string.IsNullOrEmpty(ResistenciaMecanica))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Resistencia Mecánica");
                return;
            }

            if (string.IsNullOrEmpty(Estado))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Estado");
                return;
            } 
            // ProjectId = projectViewModel.ProjectId;
            //poste.ProjectId = ProjectId;
            //resulProject = dataService.Get<Project>(true).OrderByDescending(a => a.ProjectId).FirstOrDefault();

            var projectViewModel = ProjectViewModel.GetInstance();
            var nombreProyecto = projectViewModel.ProjectName;
            var resulProject = dataService.Get<Project>(false).Where(a => a.ProjectName == nombreProyecto);
            foreach (var r in resulProject)
            {
                resultado = r.ProjectIdLocal;
            }
            switch (Condicion)
            {
                case "Instalado":
                    Condicion = "I";
                    break;
                case "Retirado":
                    Condicion = "R";
                    break;
                case "Encontrado":
                    Condicion = "E";
                    break;
                case "Cambio":
                    Condicion = "C";
                    break;
                default:
                    break;
            }
            switch (Material)
            {
                case "Concreto":
                    Material = "CO";
                    break;
                case "Madera":
                    Material = "MA";
                    break;
                case "Torre Metálica":
                    Material = "TO";
                    break;
                case "Tubo":
                    Material = "TU";
                    break;
                default:
                    break;
            }
            switch (Estado)
            {
                case "Bueno":
                    Estado = "B";
                    break;
                case "Malo":
                    Estado = "M";
                    break;
                default:
                    break;
            }
            poste.ProjectIdLocal = resultado;
            poste.CodigoApoyo = CodigoApoyo;
            poste.Condicion = Condicion;
            poste.Material = Material;
            poste.LongitudPoste = LongitudPoste;
            poste.ResistenciaMecanica = ResistenciaMecanica;
            poste.Estado = Estado;
            await navigationService.Navigate("ThirdPage");
        }

        public ICommand Poste2Command { get { return new RelayCommand(Poste2); } }

        private async void Poste2()
        {
            if (string.IsNullOrEmpty(CantidadRetenidas))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Cantidad de Retenidas");
                return;
            }

            if (string.IsNullOrEmpty(Propiedad))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Propiedad");
                return;
            }

            if (string.IsNullOrEmpty(NivelTension))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Nivel de Tensión");
                return;
            }

            if (string.IsNullOrEmpty(AlturaDisponible))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Altura Disponible");
                return;
            }

            if (string.IsNullOrEmpty(AlturaMontaje))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Altura de Montaje");
                return;
            }

            if (string.IsNullOrEmpty(TipoEstructura))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Tipo de Estructura");
                return;
            }

            switch (Propiedad)
            {
                case "ElectroHuila":
                    Propiedad = "EH";
                    break;
                case "Gobernación":
                    Propiedad = "GO";
                    break;
                case "Municipio":
                    Propiedad = "MC";
                    break;
                case "Particular":
                    Propiedad = "PA";
                    break;
                default:
                    break;
            }
            poste.CantidadRetenidas = CantidadRetenidas;
            poste.Propiedad = Propiedad;
            poste.NivelTension = NivelTension;
            poste.AlturaDisponible = AlturaDisponible;
            poste.AlturaMontaje = AlturaMontaje;
            poste.TipoEstructura = TipoEstructura;
            await navigationService.Navigate("FourthPage");
        }

        public ICommand Poste3Command { get { return new RelayCommand(Poste3); } }

        private async void Poste3()
        {
            if (string.IsNullOrEmpty(RedesBT))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar si el cable está sobre las redes BT");
                return;
            }

            if (string.IsNullOrEmpty(Retenidas))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar si se requieren Retenidas");
                return;
            }

            if (string.IsNullOrEmpty(CablesOperador))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Cables de Operador E/P");
                return;
            }

            if (string.IsNullOrEmpty(CablesComunicacionFinal))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Total de cables de comunicación final");
                return;
            }

            poste.RedesBT = RedesBT;
            poste.Retenidas = Retenidas;
            poste.CablesOperador = CablesOperador;
            poste.CablesComunicacionFinal = CablesComunicacionFinal;

            //dataService.InsertPoste(poste);

            /*var resulPoste = dataService.Get<Poste>(true).OrderByDescending(a => a.NumeroApoyo).FirstOrDefault();
            NumeroApoyo = resulPoste.NumeroApoyo; */
            /*try
            {
              var response = await apiService.PostPoste(CodigoApoyo, Condicion, Material,
              LongitudPoste, ResistenciaMecanica, Estado, CantidadRetenidas, Propiedad, NivelTension,
              AlturaDisponible, AlturaMontaje, TipoEstructura, RedesBT, Retenidas, CablesOperador,
              CablesComunicacionFinal, resulProject.ProjectId);

              var posteService = (Poste)response.Result;
              poste.Poste_Id = posteService.Poste_Id;
              Poste_Id = poste.Poste_Id;
              dataService.InsertPoste(poste);
              await navigationService.Navigate("FifthPage");
            }
            catch (Exception e)
            {
                var error = e.Message.ToString();
            }*/
            await geolocatorMapService.geoLocator();
            poste.Latitud = geolocatorMapService.Latitude;
            poste.Longitud = geolocatorMapService.Longitude;
            poste.EstadoSubida = false;
            dataService.InsertPoste(poste);
            await navigationService.Navigate("FifthPage");
        }


        #endregion
    }
}
