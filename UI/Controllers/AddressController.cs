using DAL.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Controllers
{
    public class AddressController : Controller
    {
        public IActionResult Index()
        {
            return View(AddressDAO.FindAll());
        }
    }
}
