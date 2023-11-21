using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class AdminsController : Controller
    {
        public ViewResult SignUp()
        {
            return View();
        }


        public ViewResult LogIn()
        {
            return View();
        }
    }
}
