using Microsoft.AspNetCore.Mvc;

namespace Ftr_pt2
{
    public class Readme : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
