using DATATAKEH.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Services
{
    public class ApiService
    {
        //LOGIN(GET)
        public async Task<Response> Login(string cedula, string password)
        {
            try
            {
                var loginRequest = new LoginRequest
                {
                    Cedula = cedula,
                    Password = password
                };

                var request = JsonConvert.SerializeObject(loginRequest);
                //var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();

                client.BaseAddress = new Uri("http://datatakeweb.azurewebsites.net/api/Users/LoginUser/");
                var url = cedula + "/" + password; ;

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o Contraseña incorrectos"
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Inicio de sesión éxitoso",
                    Result = user
                };

            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }

        //PROJECTS(POST)
        public async Task<Response> PostProject(string projectName, string ciudad, string empresaPropietaria,
                                                string empresaOperadora, int userId)
        {
            try
            {
                var projectRequest = new ProjectRequest
                {
                    ProjectName = projectName,
                    Ciudad = ciudad,
                    EmpresaPropietaria = empresaPropietaria,
                    EmpresaOperadora = empresaOperadora,
                    UserId = userId
                };

                var request = JsonConvert.SerializeObject(projectRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://datatakeweb.azurewebsites.net/");
                var url = "api/Projects/";

                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se pudieron subir los datos"
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var project = JsonConvert.DeserializeObject<Project>(result); 

                return new Response
                {
                    IsSuccess = true,
                    Message = "Datos subidos correctamente",
                    Result = project
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }

        //POSTES(POST)
        public async Task<Response> PostPoste(string codigoApoyo, string condicion, 
            string material, string longitudPoste, string resistenciaMecanica, string estado,
            string cantidadRetenidas, string propiedad, string nivelTension,string alturaDisponible, 
            string alturaMontaje,string tipoEstructura, string redesBT, string retenidas,string cablesOperador,
            string cablesComunicacionFinal, double latitud, double longitud, int projectId)
        {
            try
            {
                var posteRequest = new PosteRequest
                {                 
                    CodigoApoyo = codigoApoyo,
                    Condicion = condicion,
                    Material = material,
                    LongitudPoste = longitudPoste,
                    ResistenciaMecanica = resistenciaMecanica,
                    Estado = estado,
                    CantidadRetenidas = cantidadRetenidas,
                    Propiedad = propiedad,
                    NivelTension = nivelTension,
                    AlturaDisponible = alturaDisponible,
                    AlturaMontaje = alturaMontaje,
                    TipoEstructura = tipoEstructura,
                    RedesBT = redesBT,
                    Retenidas = retenidas,
                    CablesOperador = cablesOperador,
                    CablesComunicacionFinal = cablesComunicacionFinal,
                    Latitud = latitud,
                    Longitud = longitud,
                    ProjectId = projectId
                };

                var request = JsonConvert.SerializeObject(posteRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://datatakeweb.azurewebsites.net");
                var url = "/api/Postes";

                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se pudieron subir los datos"
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var poste = JsonConvert.DeserializeObject<Poste>(result); 

                return new Response
                {
                    IsSuccess = true,
                    Message = "Datos subidos correctamente",
                    Result = poste                
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }

        //VANOS(POST)
        public async Task<Response> PostVano(string tipoVano, string reserva, string longitudVano, 
                                            string tipoCableRed, string tipoCableComunicacion ,int numeroApoyo)
        {
            try
            {
                var vanoRequest = new VanoRequest
                {
                    TipoVano = tipoVano,
                    Reserva = reserva,
                    LongitudVano = longitudVano,
                    TipoCableRed = tipoCableRed,
                    TipoCableComunicacion = tipoCableComunicacion,
                    Poste_Id = numeroApoyo
                };

                var request = JsonConvert.SerializeObject(vanoRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://datatakeweb.azurewebsites.net");
                var url = "/api/Vanoes";

                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se pudieron subir los datos"
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var vano = JsonConvert.DeserializeObject<Vano>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Datos subidos correctamente",
                    Result = vano
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }

        //EQUIPOS(POST)
        public async Task<Response> PostEquipo(string condicion, string isAmplificador, string isFuente,
                                           string isCaja, string isEnergia, string otroEquipo,
                                           string codigoPlaca, int vanoId )
        {
            try
            {
                var equipoRequest = new EquipoRequest
                {
                    Condicion = condicion,
                    IsAmplificador = isAmplificador,
                    IsFuente = isFuente,
                    IsCaja = isCaja,
                    IsEnergia = isEnergia,
                    OtroEquipo = otroEquipo,
                    CodigoPlaca = codigoPlaca,
                    VanoId = vanoId
                };

                var request = JsonConvert.SerializeObject(equipoRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://datatakeweb.azurewebsites.net");
                var url = "/api/Equipoes";

                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se pudieron subir los datos"
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var equipo = JsonConvert.DeserializeObject<Equipo>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Datos subidos correctamente",
                    Result = equipo
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }

        //DIRECCIONES(POST)
        public async Task<Response> PostDirection(string departamento, string municipio, string barrio,
                                                 string direccion, string observaciones, int equipoId)
        {
            try
            {
                var directionRequest = new DirectionRequest
                {
                   Departamento = departamento,
                   Municipio = municipio,
                   Barrio = barrio,
                   Direccion = direccion,
                   Observaciones = observaciones,
                   EquipoId = equipoId
                };

                var request = JsonConvert.SerializeObject(directionRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://datatakeweb.azurewebsites.net");
                var url = "/api/Directions";

                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se pudieron subir los datos"
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var direction = JsonConvert.DeserializeObject<Direction>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Datos subidos correctamente",
                    Result = direction
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }

        //FOTOS(POST)
        public async Task<Response> PostFoto(string rutaFoto, byte[] imageArray, string nombreFoto,
            int directionId)
        {
            try
            {
                var fotoRequest = new FotoRequest
                {
                    RutaFoto = rutaFoto,
                    ImageArray = imageArray,
                    NombreFoto = nombreFoto,
                    DirectionId = directionId
                };

                var request = JsonConvert.SerializeObject(fotoRequest);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://datatakeweb.azurewebsites.net");
                var url = "/api/Fotoes";

                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "No se pudieron subir los datos"
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var foto = JsonConvert.DeserializeObject<Foto>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Datos subidos correctamente",
                    Result = foto
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
    }
}
