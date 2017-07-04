using DATATAKEH.Models;
using DATATAKEH.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DATATAKEH.ViewModels
{
    public class ProjectViewModel
    {
        #region Attributes

        private NavigationService navigationService;

        private DialogService dialogService;

        private DataService dataService;

        private ApiService apiService;

        private NetService netService;

       

        private int resultado;

        #endregion

        #region Singleton

        static ProjectViewModel instance;

        public static ProjectViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new ProjectViewModel();
            }

            return instance;
        }

        #endregion

        #region Constructors

        public ProjectViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            dataService = new DataService();
            apiService = new ApiService();
            netService = new NetService();
            instance = this;
        }

        #endregion

        #region Properties

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Ciudad { get; set; }

        public string EmpresaPropietaria { get; set; }

        public string EmpresaOperadora { get; set; }
            
        public bool Estado { get; set; }

        #endregion

        #region Commands

        public ICommand ProjectCommand { get { return new RelayCommand(Project); } }

        private async void Project()
        {
            if (string.IsNullOrEmpty(ProjectName))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar proyecto");
                return;
            }

            if (string.IsNullOrEmpty(Ciudad))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Ciudad");
                return;
            }

            if (string.IsNullOrEmpty(EmpresaPropietaria))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Empresa Propietaria");
                return;
            }

            if (string.IsNullOrEmpty(EmpresaOperadora))
            {
                await dialogService.ShowMessage("Error", "Debes ingresar Empresa Operadora");
                return;
            }
            //try
            //{
                var loginViewModel = LoginViewModel.GetInstance();
                var cedulaUsuario = loginViewModel.Cedula;
                if(cedulaUsuario == null)
                {
                    cedulaUsuario = App.CurrentUser.Cedula;
                }
               
                var project = new Project();
            //var resulUser = dataService.Get<User>(true).OrderByDescending(a => a.UserId).FirstOrDefault();
                 var resulUser = dataService.Get<User>(true).Where(a => a.Cedula == cedulaUsuario);
                foreach (var r in resulUser)
                {
                    resultado = r.UserId;
                }
                project.UserId = resultado;
                project.ProjectName = ProjectName;
                project.Ciudad = Ciudad;
                project.EmpresaPropietaria = EmpresaPropietaria;
                project.EmpresaOperadora = EmpresaOperadora;
                Estado = false;

                /*var resul = dataService.Get<Project>(true).OrderByDescending(a => a.ProjectId).FirstOrDefault();
                ProjectId = resul.ProjectId;*/
               /* if (netService.IsConnected())
                {
                    var query = dataService.Get<Project>(false).Where(a => a.Estado = false);
                    foreach (var q in query)
                    {

                    }
                    var response = await apiService.PostProject(ProjectName, Ciudad, EmpresaPropietaria, EmpresaOperadora, resulUser.UserId);
                    var proyecto = (Project)response.Result;
                    project.ProjectId = proyecto.ProjectId;
                    ProjectId = project.ProjectId;
                    Estado = true;
                } */

                project.Estado = Estado;
                dataService.InsertProject(project);
                await navigationService.Navigate("SecondPage");
         }
           /* catch (Exception e)
            {
                var error = e.Message.ToString();
            } */
          
     }
            

            #endregion
        
}
