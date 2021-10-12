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
    public class PatientController : Controller
    {
        public IActionResult Index()
        {          
            return View(PatientDAO.FindAll());
        }
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();
            Patient p = PatientDAO.FindById(id);
            if (p == null) return NotFound();
            return View(p);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit (Guid id, Patient p)
        {
            if (id != p.Id) return BadRequest();
            try
            {
                PatientDAO.Update(id, p);
                return RedirectToAction(nameof(Index));
            }catch (Exception e)
            {
                return NotFound();
            }           
        }
        public IActionResult Delete(Guid? id)
        {
            if (id == null) return NotFound();
            Patient p = PatientDAO.FindById(id);
            if (p == null) return NotFound();
            return View(p);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(Guid id)
        {
            if (id != id) return BadRequest();
            try
            {
                PatientDAO.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Insert(Patient p)
        {
            PatientDAO.Insert(p);
            return RedirectToAction(nameof(Index));
        }
    }
}
