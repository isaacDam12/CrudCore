using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Hospital
    {
        public static Dictionary<string,object>Add(ML.Hospital hospital)
        {
            string msg = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", msg } };

            try
            {
                using (DL.PracticaCoreContext context = new DL.PracticaCoreContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddHospital '{hospital.Nombre}','{hospital.Direccion}','{hospital.AnioConstruccion}',{hospital.Capacidad},{hospital.Especialidad.IdEspecialidad}");

                    if(query > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }

            }catch (Exception e)
            {
                msg = e.Message;
                diccionario["Mensaje"] = msg;
            }

            return diccionario;
        }

        public static Dictionary<string,object>Delete(int idHospital)
        {
            string msg = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", msg } };

            try
            {
                using (DL.PracticaCoreContext context = new DL.PracticaCoreContext())
                {
                    var query = (from hosp in context.Hospitals where hosp.IdHospital ==  idHospital select hosp).First();
                    context.Remove(query);

                    int filas = context.SaveChanges();

                    if (filas > 0)
                    {
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }

            }
            catch (Exception e)
            {
                msg = e.Message;
                diccionario["Mensaje"] = msg;
            }

            return diccionario;
        }

        public static Dictionary<string, object> GetAll()
        {
            string msg = "";
            ML.Hospital hospital = new ML.Hospital();
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", msg },{"Hospital",hospital} };

            try
            {
                using (DL.PracticaCoreContext context = new DL.PracticaCoreContext())
                {
                    var query = (from hosp in context.Hospitals join espe in context.Especialidads on hosp.IdEspecialidad equals espe.IdEspecialidad
                                 select new
                                 {
                                     IdHospital = hosp.IdHospital,
                                     Nombre = hosp.Nombre,
                                     Direccion = hosp.Direccion,
                                     AnioConstru = hosp.AnioConstruccion,
                                     Capacidad = hosp.Capacidad,
                                     IdEspecialidad = espe.IdEspecialidad,
                                     EspecialidadNombre = espe.Nombre
                                 }).ToList();

                    
                    if(query != null)
                    {
                        hospital.Hospitales = new List<ML.Hospital>();

                        foreach(var item in query)
                        {
                            ML.Hospital hospital1 = new ML.Hospital();

                            hospital1.IdHospital = item.IdHospital;
                            hospital1.Nombre = item.Nombre;
                            hospital1.Direccion = item.Direccion;
                            hospital1.AnioConstruccion = (DateTime)item.AnioConstru;
                            hospital1.Capacidad = item.Capacidad.Value;

                            hospital1.Especialidad = new ML.Especialidad();
                            hospital1.Especialidad.IdEspecialidad = item.IdEspecialidad;
                            hospital1.Especialidad.Nombre = item.EspecialidadNombre;

                            hospital.Hospitales.Add(hospital1);

                        }

                        diccionario["Hospital"] = hospital;
                        diccionario["Resultado"] = true;
                    }
                }

            }
            catch (Exception e)
            {
                msg = e.Message;
                diccionario["Mensaje"] = msg;
            }

            return diccionario;
        }
    }
}
