using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class HospitalController : Controller
    {
        public IActionResult GetAll()
        {
            Dictionary<string,object>result = BL.Hospital.GetAll();

            bool rs = (bool)result["Resultado"];

            if(rs)
            {
                ML.Hospital hospital = (ML.Hospital)result["Hospital"];
                return View(hospital);
            }
            else
            {
                ViewBag.Mensaje = "Error en traer los datos";

                return PartialView("Modal");
            }
            
        }
    }
}
