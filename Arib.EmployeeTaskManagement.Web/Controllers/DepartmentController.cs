using Microsoft.AspNetCore.Mvc;

namespace Arib.EmployeeTaskManagement.Web.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
