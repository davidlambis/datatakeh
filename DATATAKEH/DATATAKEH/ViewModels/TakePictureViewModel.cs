using DATATAKEH.Interfaces;
using DATATAKEH.Models;
using DATATAKEH.Services;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PSC.Xamarin.MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DATATAKEH.ViewModels
{
    public class TakePictureViewModel : BaseViewModel
    {
        #region Attributes

        ICommand _cameraCommand, _previewImageCommand = null;
        ObservableCollection<GalleryImage> _images = new ObservableCollection<GalleryImage>();
        ImageSource _previewImage = null;
        private DataService dataService;
        private DialogService dialogService;
        private NavigationService navigationService;
        private ApiService apiService;
        private StoreCameraMediaOptions opciones_almacenamiento;
        private string aPpath;
        public Foto foto;
        public int resultado;
        private string nombrefoto;
        /////
        /*private ImageSource imageSource;
        private MediaFile file;
        byte[] imageAsBytes = null;*/

        #endregion

        #region Singleton

        static TakePictureViewModel instance;

        public static TakePictureViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new TakePictureViewModel();
            }

            return instance;
        }

        #endregion

        #region Constructors

        public TakePictureViewModel()
        {
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            apiService = new ApiService();
            opciones_almacenamiento = new StoreCameraMediaOptions();
            foto = new Foto();
            instance = this;
            
        }

        #endregion

        #region Properties
        public int FotoIdLocal { get; set; }

        public int FotoId { get; set; }

        public string RutaFoto { get; set; }

        public byte[] ImageArray { get; set; }

        public bool Estado { get; set; }

        public string NombreFoto
        {
            get { return nombrefoto; }
            set
            {
                SetProperty(ref nombrefoto, value);
            }
        }

        public int DirectionIdLocal { get; set; }

        #endregion

        #region Events

        //public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public ObservableCollection<GalleryImage> Images
        {
            get { return _images; }
            set { SetProperty(ref _images, value); }
        }

        public ImageSource PreviewImage
        {
            get { return _previewImage; }
            set
            {
                SetProperty(ref _previewImage, value);
            }
        }

        public ICommand CameraCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraCommand(), () => CanExecuteCameraCommand()); }
            
            
        }

        public bool CanExecuteCameraCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return false;
            }
            return true;

        }

        public async Task ExecuteCameraCommand()
        {
            if (string.IsNullOrEmpty(NombreFoto))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar nombre a la foto");
                return;
            }
            
            if (Images.Count == 4)
            {
                await dialogService.ShowMessage("Alerta", "Puedes tomar máximo 4 Fotos");
                return;
            }

            opciones_almacenamiento = new StoreCameraMediaOptions()
            {
                SaveToAlbum = true,
                Directory = "FotosDATAKEH",
                Name = "Foto.jpg",
                PhotoSize = PhotoSize.Custom,
                CompressionQuality = 92,
                CustomPhotoSize = 50 //Resize to 50% of original    
            };

            var file = await CrossMedia.Current.TakePhotoAsync(opciones_almacenamiento);

            if (file == null)
                return;

            //Get the public album path
            aPpath = file.AlbumPath;

            //Get private path
            var path = file.Path;

            //await dialogService.ShowMessage("File Album", aPpath);
            //await dialogService.ShowMessage("File Path", path);

            byte[] imageAsBytes = null;

            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                imageAsBytes = memoryStream.ToArray();
            }

            var resizer = DependencyService.Get<IImageResize>();

            imageAsBytes = resizer.ResizeImage(imageAsBytes, 1080, 1080);

            var imageSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));

            _images.Add(new GalleryImage { Source = imageSource, OrgImage = imageAsBytes });

            /*var resulDireccion = dataService.Get<Direction>(true).OrderByDescending(a => a.DirectionId).FirstOrDefault();
            foto.DirectionIdLocal = resulDireccion.DirectionIdLocal;*/
            var directionViewModel = DirectionViewModel.GetInstance();
            var direccion = directionViewModel.Direccion;
            var resulDireccion = dataService.Get<Direction>(false).Where(a => a.Direccion == direccion);
            foreach (var d in resulDireccion)
            {
                resultado = d.DirectionIdLocal;
            }
            
            foto.NombreFoto = NombreFoto;
            foto.DirectionIdLocal = resultado;
            foto.RutaFoto = aPpath;
            foto.ImageArray = imageAsBytes;

            /*
            try
            {
                var response = await apiService.PostFoto(aPpath, imageAsBytes, resulDireccion.DirectionId);
                var fotoService = (Foto)response.Result;
                foto.FotoId = fotoService.FotoId;
                FotoId = foto.FotoId;
                dataService.InsertFoto(foto);
            }
            catch (Exception e)
            {
                var error = e.Message.ToString();
            } */

            foto.Estado = false;
            dataService.InsertFoto(foto);
            NombreFoto = "";
            /*var takePictureViewModel = TakePictureViewModel.GetInstance();
            takePictureViewModel.NombreFoto = "";*/
            return;
        }

        public ICommand PreviewImageCommand
        {            
            get
            {
                return _previewImageCommand ?? new Command<Guid>((img) => {

                    var image = _images.Single(x => x.ImageId == img).OrgImage;

                    PreviewImage = ImageSource.FromStream(() => new MemoryStream(image));

                });
            }
        }

        public ICommand NavigateCommand { get { return new RelayCommand(Avanzar); } }

        private async void Avanzar()
        {
            if (Images.Count >= 0 && Images.Count < 4)
            {
                await dialogService.ShowMessage("Alerta", "Debes Ingresar las 4 fotos requeridas");
            }
            else
            {
                await navigationService.Navigate("MapPage");
            }
        }
    }
}

    


