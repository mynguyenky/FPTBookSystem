using Microsoft.AspNetCore.Mvc;

namespace ASM2.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
