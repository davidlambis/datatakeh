using DATATAKEH.Models;
using DATATAKEH.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DATATAKEH.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attributes

        private NavigationService navigationService;

        private DialogService dialogService;

        private DataService dataService;

        private ApiService apiService;

        private NetService netService;

        //TODO private ApiService apiService;

        //Es para el activity indicator (El que carga en el login) 
        private bool isRunning;

        #endregion

        #region Singleton

        static LoginViewModel instance;

        public static LoginViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginViewModel();
            }

            return instance;
        }

        #endregion



        #region Constructors

        public LoginViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            dataService = new DataService();
            apiService = new ApiService();
            netService = new NetService();
            IsRemembered = true;
            instance = this;
        }
        #endregion


        #region Properties

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Cedula { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsUser { get; set; }

        public bool IsRemembered { get; set; }

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


        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged; 

        #endregion

        #region Commands

        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Cedula))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar tu número de Cédula");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Contraseña");
                return;
            }

            IsRunning = true;
            var response = new Response();
            if (netService.IsConnected())
            {
                response = await apiService.Login(Cedula, Password);
            }
            else
            {
                response = dataService.Login(Cedula, Password);
            }

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }

            var user = (User)response.Result;
            var usuario = new User();
            usuario.UserId = user.UserId;
            usuario.UserName = user.UserName;
            usuario.Cedula = user.Cedula;
            Cedula = usuario.Cedula;
            usuario.Password = user.Password;
            usuario.Email = user.Email;
            usuario.IsAdmin = user.IsAdmin;
            usuario.IsUser = user.IsUser;
            usuario.IsRemembered = IsRemembered;
            dataService.InsertUser(usuario);
            dataService.UpdateUser(usuario);
            /*var resul = dataService.Get<User>(true).OrderByDescending(a => a.UserId).FirstOrDefault();
            UserId = resul.UserId; */
            UserId = usuario.UserId;
            //Acceder al Main Page
            navigationService.setMainPage(user);
        } 

        #endregion
    }
}
