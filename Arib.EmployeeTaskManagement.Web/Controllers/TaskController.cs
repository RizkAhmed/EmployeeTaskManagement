using Microsoft.AspNetCore.Mvc;

namespace Arib.EmployeeTaskManagement.Web.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
