using DATATAKEH.Data;
using DATATAKEH.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DATATAKEH.Services
{
    public class DataService
    {
        //GENERIC
        public List<T> Get<T>(bool withChildren) where T : class
        {
            using (var da = new DataAccess())
            {
                return da.GetList<T>(withChildren).ToList();
            }
        }

        //USUARIOS
        public void InsertUser(User user)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var p = da.GetList<User>(false).Where(a => a.Cedula == user.Cedula);
                    if (p.Count() == 0)
                    {
                        da.Insert(user);
                    } 
                   
                }
            }
            catch
            {

            }
        }

        public User GetUser()
        {
            using (var da = new DataAccess())
            {
                return da.First<User>(true);
            }
        }

        public Response UpdateUser(User user)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var p = da.GetList<User>(false).Where(a => a.Cedula == user.Cedula);
                    if (p.Count() != 0)
                    {
                        da.Update(user);
                    }                     
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Usuario actualizado correctamente",
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

        public Response Login(string cedula, string password)
        {
            try
            {
                using ( var da = new DataAccess())
                {
                    var user = da.First<User>(true);
                    if(user == null)
                    {
                        return new Response
                        {
                            IsSuccess = false,
                            Message = "No hay conexión a Internet y no hay un usuario registrado en el dispositivo"
                        };
                    }
                    if(user.Cedula == cedula && user.Password == password)
                    {
                        return new Response
                        {
                            IsSuccess = true,
                            Message = "Login Ok",
                            Result = user
                        };
                    }

                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o Contraseña Incorrectos"
                    };
                }
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

        //PROYECTOS
        public Project InsertProject(Project project)
        {
            try
            {
                using(var da = new DataAccess())
                {
                    var p = da.GetList<Project>(false).Where(a => a.UserId == project.UserId &&
                                                                 a.ProjectName == project.ProjectName &&
                                                                 a.Ciudad == project.Ciudad &&
                                                                 a.EmpresaPropietaria == project.EmpresaPropietaria &&
                                                                 a.EmpresaOperadora == project.EmpresaOperadora);
                     if (p.Count() == 0)
                     {
                         da.Insert(project);
                     }
                    else
                    {
                        project = p.FirstOrDefault();
                    }
                    
                }
                return project;
            }
            catch
            {
                return null;
            }           
        }

        

        public Project GetProject()
        {
            using (var da = new DataAccess())
            {
                return da.First<Project>(true);
            }
        }

        public Response UpdateProject(Project project)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.Update(project);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Proyecto actualizado correctamente",
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


        //POSTES
        public Poste InsertPoste(Poste poste)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var p = da.GetList<Poste>(false).Where(a => a.CodigoApoyo == poste.CodigoApoyo
                                                            && a.ProjectIdLocal == poste.ProjectIdLocal);

                    if (p.Count() == 0)
                    {
                        da.Insert(poste);
                    }
                    else
                    {
                        poste = p.FirstOrDefault();
                    }
                   
                }
                return poste;
            }
            catch
            {
                return null;
            }
        }

        public Response UpdatePoste(Poste poste)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.Update(poste);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Poste actualizado correctamente",
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

        //VANOS
        public Vano InsertVano(Vano vano)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var p = da.GetList<Vano>(false).Where(a => a.TipoVano == vano.TipoVano &&
                                                                a.Reserva == vano.Reserva &&
                                                                a.LongitudVano == vano.LongitudVano &&
                                                                a.TipoCableRed == vano.TipoCableRed &&
                                                                a.TipoCableComunicacion == vano.TipoCableComunicacion &&
                                                                a.PosteIdLocal == vano.PosteIdLocal);

                    if (p.Count() == 0)
                    {
                        da.Insert(vano);
                    }
                    else
                    {
                        vano = p.FirstOrDefault();
                    }
                   
                }
                return vano;
            }
            catch
            {
                return null;
            }
        }

        public Response UpdateVano(Vano vano)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.Update(vano);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Vano actualizado correctamente",
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


        //EQUIPOS
        public Equipo InsertEquipo(Equipo equipo)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var p = da.GetList<Equipo>(false).Where(a => a.CodigoPlaca == equipo.CodigoPlaca &&
                                                                 a.VanoIdLocal == equipo.VanoIdLocal);

                    if (p.Count() == 0)
                    {
                        da.Insert(equipo);
                    }
                    else
                    {
                        equipo = p.FirstOrDefault();
                    }
                   
                }
                return equipo;
            }
            catch
            {
                return null;
            }
        }

        public Response UpdateEquipo(Equipo equipo)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.Update(equipo);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Equipo actualizado correctamente",
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


        //DIRECCIÓN/OBSERVACIONES
        public Direction InsertDirection(Direction direction)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var p = da.GetList<Direction>(false).Where(a => a.Direccion == direction.Direccion &&
                                                                    a.EquipoIdLocal == direction.EquipoIdLocal);

                    if (p.Count() == 0)
                    {
                        da.Insert(direction);
                    }
                    else
                    {
                        direction = p.FirstOrDefault();
                    }
                   
                }
                return direction;
            }
            catch
            {
                return null;
            }
        }

        public Response UpdateDirection(Direction direction)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.Update(direction);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Dirección actualizada correctamente",
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

        //FOTOS
        public Foto InsertFoto(Foto foto)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    var p = da.GetList<Foto>(false).Where(a => a.RutaFoto == foto.RutaFoto);

                    if (p.Count() == 0)
                    {
                        da.Insert(foto);
                    }
                    else
                    {
                        foto = p.FirstOrDefault();
                    }
                   
                }
                return foto;
            }
            catch
            {
                return null;
            }
        }

        public Response UpdateFoto(Foto foto)
        {
            try
            {
                using (var da = new DataAccess())
                {
                    da.Update(foto);
                }

                return new Response
                {
                    IsSuccess = true,
                    Message = "Foto actualizada correctamente",
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

        public Foto GetFoto()
        {
            using (var da = new DataAccess())
            {
                return da.First<Foto>(true);
            }
        }

    }
}
