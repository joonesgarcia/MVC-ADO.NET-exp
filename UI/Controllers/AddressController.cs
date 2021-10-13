using DAL.DataAccess;
using DAL.Models;
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
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();
            Address a = AddressDAO.FindById(id);
            if (a == null) return NotFound();
            return View(a);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Guid id, Address p)
        {
            if (id != p.Id) return BadRequest();
            try
            {
                AddressDAO.Update(id, p);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();
            Address a = AddressDAO.FindById(id);
            if (a == null) return NotFound();
            return View(a);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(Address p)
        {
            if (p.Id != p.Id) return BadRequest();
            try
            {
                AddressDAO.Delete(p);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}
