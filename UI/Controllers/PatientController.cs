using DAL.DataAccess;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UI.Models;
using UI.Services;

namespace BLL.Controllers
{
    public class PatientController : Controller
    {
        private readonly EmailService _emailService;
        public PatientController(EmailService emailservice)
        {
            _emailService = emailservice;
        }

        public IActionResult Index()
        {          
            return View(PatientDAO.FindAll());
        }
        public IActionResult Edit(Guid? id)
        {
            if (id == null) return NotFound();

            Patient p = PatientDAO.FindById(id);

            if (p == null) return NotFound();
        

            return View("Form",p);
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
            var patient = new Patient();

            patient.Adresses.Add(new Address());

            return View("Form",patient);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Insert(Patient p)
        {
            PatientDAO.Insert(p);
            _emailService.SendEmail(new UserEmailOptions()
            {
                Body = $"Olá {p.Name}, seu cadastro no sistema Hospital Central foi confirmado! ",
                Subject = "Seja bem vindo ao Hospital Central",
                ToEmails = new List<string>() { p.Email }
            });
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult AddAddress(Patient patient)
        {
            patient.Adresses.Add(new Address());
            return PartialView("_Address", patient);
        }
    }
}
