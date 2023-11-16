using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Areas.AdminArea.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
