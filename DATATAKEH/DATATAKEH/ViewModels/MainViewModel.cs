using DATATAKEH.Interfaces;
using DATATAKEH.Models;
using DATATAKEH.Pages;
using DATATAKEH.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PSC.Xamarin.MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DATATAKEH.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        #region Attributes

        private NavigationService navigationService;

        private DataService dataService;

        private DialogService dialogService;

        private ApiService apiService;

        private NetService netService;

        private GeolocatorMapService geolocatorMapService;

        private bool isRunning;

        //private Foto foto;

        #endregion

        #region Properties
        //UI PROPERTIES
        public string ProjectName { get; set; }

        public string PageName { get; set; }

        public LoginViewModel NewLogin { get; set; }

        public ProjectViewModel NewProject { get; set; }

        public PosteViewModel NewPoste { get; set; }

        public VanoViewModel NewVano { get; set; }

        public EquipoViewModel NewEquipo { get; set; }

        public DirectionViewModel NewDirection { get; set; }

        public Foto foto { get; set; }

        public bool IsRunning
        {
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        //public TakePictureViewModel NewPicture { get; set; }
        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors
        public MainViewModel()
        {
            navigationService = new NavigationService();
            //dialogService = new DialogService();
            NewLogin = new LoginViewModel();
            NewProject = new ProjectViewModel();
            geolocatorMapService = new GeolocatorMapService();
            NewPoste = new PosteViewModel();
            NewVano = new VanoViewModel();
            NewEquipo = new EquipoViewModel();
            NewDirection = new DirectionViewModel();
            //NewPicture = new TakePictureViewModel();
            //foto = new Foto();
            dataService = new DataService();
            dialogService = new DialogService();
            apiService = new ApiService();
            netService = new NetService();
        }
        #endregion

        #region Commands

        //Para el menú poder reconocer el evento para cada item
        public ICommand NavigateCommand { get { return new RelayCommand<string>(Navigate); } }

        private async void Navigate(string PageName)
        {
            await navigationService.Navigate(PageName);

        }

        public ICommand SyncCommand { get { return new RelayCommand(Sync); } }

        private async void Sync()
        {
            if (netService.IsConnected())
            {
                var queryProject = dataService.Get<Project>(false).OrderByDescending(a => a.ProjectIdLocal).Where<Project>(a => a.Estado == false);
                IsRunning = true;
                foreach (var q in queryProject)
                {
                    try
                    {
                        var response = await apiService.PostProject(q.ProjectName, q.Ciudad, q.EmpresaPropietaria, q.EmpresaOperadora, q.UserId);
                        var project = new Project();
                        var projectService = (Project)response.Result;
                        project.ProjectIdLocal = q.ProjectIdLocal;
                        project.ProjectId = projectService.ProjectId;
                        project.ProjectName = q.ProjectName;
                        project.Ciudad = q.Ciudad;
                        project.EmpresaPropietaria = q.EmpresaPropietaria;
                        project.EmpresaOperadora = q.EmpresaOperadora;
                        project.UserId = q.UserId;
                        //project.Estado = true;
                        dataService.UpdateProject(project);
                        //POSTE
                        var queryPoste = dataService.Get<Poste>(false).OrderByDescending(a => a.PosteIdLocal).Where<Poste>(a => a.EstadoSubida == false &&
                                                                                                                           a.ProjectIdLocal == q.ProjectIdLocal);
                        foreach (var r in queryPoste)
                        {
                            var responsePoste = await apiService.PostPoste(r.CodigoApoyo, r.Condicion, r.Material,
                               r.LongitudPoste, r.ResistenciaMecanica, r.Estado, r.CantidadRetenidas, r.Propiedad,
                               r.NivelTension, r.AlturaDisponible, r.AlturaMontaje, r.TipoEstructura, r.RedesBT,
                               r.Retenidas, r.CablesOperador, r.CablesComunicacionFinal, r.Latitud, r.Longitud,
                               project.ProjectId);
                            var poste = new Poste();
                            var posteService = (Poste)responsePoste.Result;
                            poste.PosteIdLocal = r.PosteIdLocal;
                            poste.Poste_Id = posteService.Poste_Id;
                            poste.CodigoApoyo = r.CodigoApoyo;
                            poste.Condicion = r.Condicion;
                            poste.Material = r.Material;
                            poste.LongitudPoste = r.LongitudPoste;
                            poste.ResistenciaMecanica = r.ResistenciaMecanica;
                            poste.Estado = r.Estado;
                            poste.CantidadRetenidas = r.CantidadRetenidas;
                            poste.Propiedad = r.Propiedad;
                            poste.NivelTension = r.NivelTension;
                            poste.AlturaDisponible = r.AlturaDisponible;
                            poste.AlturaMontaje = r.AlturaMontaje;
                            poste.TipoEstructura = r.TipoEstructura;
                            poste.RedesBT = r.RedesBT;
                            poste.Retenidas = r.Retenidas;
                            poste.CablesOperador = r.CablesOperador;
                            poste.CablesComunicacionFinal = r.CablesComunicacionFinal;
                            poste.Latitud = r.Latitud;
                            poste.Longitud = r.Longitud;
                            poste.ProjectIdLocal = project.ProjectIdLocal;
                            poste.EstadoSubida = true;
                            dataService.UpdatePoste(poste);

                            //VANO
                            var queryVano = dataService.Get<Vano>(false).OrderByDescending(a => a.VanoIdLocal).Where<Vano>(a => a.Estado == false &&
                                                                                                                            a.PosteIdLocal == poste.PosteIdLocal);
                            foreach (var v in queryVano)
                            {
                                var responseVano = await apiService.PostVano(v.TipoVano, v.Reserva, v.LongitudVano,
                                    v.TipoCableRed, v.TipoCableComunicacion, poste.Poste_Id);
                                var vano = new Vano();
                                var vanoService = (Vano)responseVano.Result;
                                vano.VanoIdLocal = v.VanoIdLocal;
                                vano.VanoId = vanoService.VanoId;
                                vano.TipoVano = v.TipoVano;
                                vano.Reserva = v.Reserva;
                                vano.LongitudVano = v.LongitudVano;
                                vano.TipoCableRed = v.TipoCableRed;
                                vano.TipoCableComunicacion = v.TipoCableComunicacion;
                                vano.PosteIdLocal = poste.PosteIdLocal;
                                vano.Estado = true;
                                dataService.UpdateVano(vano);
                                
                                 //EQUIPO
                                 var queryEquipo = dataService.Get<Equipo>(false).OrderByDescending(a => a.EquipoIdLocal).Where<Equipo>(a => a.Estado == false &&
                                                                                                                                        a.VanoIdLocal == vano.VanoIdLocal);
                                 foreach(var e in queryEquipo)
                                 {
                                     var responseEquipo = await apiService.PostEquipo(e.Condicion, e.IsAmplificador, e.IsFuente,
                                         e.IsCaja, e.IsEnergia, e.OtroEquipo, e.CodigoPlaca, vano.VanoId);
                                     var equipo = new Equipo();
                                     var equipoService = (Equipo)responseEquipo.Result;
                                     equipo.EquipoIdLocal = e.EquipoIdLocal;
                                     equipo.EquipoId = equipoService.EquipoId;
                                     equipo.Condicion = e.Condicion;
                                     equipo.IsAmplificador = e.IsAmplificador;
                                     equipo.IsFuente = e.IsFuente;
                                     equipo.IsCaja = e.IsCaja;
                                     equipo.IsEnergia = e.IsEnergia;
                                     equipo.OtroEquipo = e.OtroEquipo;
                                     equipo.CodigoPlaca = e.CodigoPlaca;
                                     equipo.VanoIdLocal = vano.VanoIdLocal;
                                     equipo.Estado = true;
                                     dataService.UpdateEquipo(equipo);

                                    //DIRECCION
                                    var queryDirection = dataService.Get<Direction>(false).OrderByDescending(a => a.DirectionIdLocal).Where<Direction>(a => a.Estado == false &&
                                                                                                                                                        a.EquipoIdLocal == equipo.EquipoIdLocal);
                                    foreach(var d in queryDirection)
                                    {
                                        var responseDirection = await apiService.PostDirection(d.Departamento, d.Municipio, d.Barrio, d.Direccion,
                                             d.Observaciones, equipo.EquipoId);
                                        var direction = new Direction();
                                        var directionService = (Direction)responseDirection.Result;
                                        direction.DirectionIdLocal = d.DirectionIdLocal;
                                        direction.DirectionId = directionService.DirectionId;
                                        direction.Departamento = d.Departamento;
                                        direction.Municipio = d.Municipio;
                                        direction.Barrio = d.Barrio;
                                        direction.Direccion = d.Direccion;
                                        direction.Observaciones = d.Observaciones;
                                        direction.EquipoIdLocal = equipo.EquipoIdLocal;
                                        direction.Estado = true;
                                        dataService.UpdateDirection(direction);

                                        //FOTO
                                        var queryFoto = dataService.Get<Foto>(false).OrderByDescending(a => a.FotoIdLocal).Where<Foto>(a => a.Estado == false &&
                                                                                                                                        a.DirectionIdLocal == direction.DirectionIdLocal);
                                        foreach(var f in queryFoto)
                                        {
                                            var responseFoto = await apiService.PostFoto(f.RutaFoto,
                                                f.ImageArray,f.NombreFoto, direction.DirectionId);
                                            var foto = new Foto();
                                            var fotoService = (Foto)responseFoto.Result;
                                            foto.FotoIdLocal = f.FotoIdLocal;
                                            foto.FotoId = fotoService.FotoId;
                                            foto.RutaFoto = f.RutaFoto;
                                            foto.ImageArray = f.ImageArray;
                                            foto.NombreFoto = f.NombreFoto;
                                            foto.DirectionIdLocal = direction.DirectionIdLocal;
                                            foto.Estado = true;
                                            dataService.UpdateFoto(foto);
                                        }

                                    }
                                 }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        var error = e.Message.ToString();
                    }
                }
                IsRunning = false;
                await dialogService.ShowMessage("Ok", "Información sincronizada correctamente");
                await navigationService.Navigate("EleventhPage");
            }
            else
            {
                await dialogService.ShowMessage("Error", "No hay conexión a internet");
            }
        }

        public ICommand ClearCommand { get { return new RelayCommand(Clear); } }

        private async void Clear()
        {
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
            navigationService.setMainPage(App.CurrentUser);
        }

        #endregion


    }
}
