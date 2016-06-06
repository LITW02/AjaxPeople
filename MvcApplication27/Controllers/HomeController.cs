using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication27.Models;
using People.Data;

namespace MvcApplication27.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            IndexViewModel viewModel = new IndexViewModel();
            viewModel.People = manager.GetAll();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddPerson(Person person)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            manager.AddPerson(person);
            return Json(person);
        }

        public ActionResult GetAll()
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            return Json(manager.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeletePerson(int id)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            manager.DeletePerson(id);
            return Json(new { status = "Person deleted" });
        }

        public ActionResult GetPerson(int id)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            return Json(manager.GetPerson(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePerson(Person person)
        {
            var manager = new PersonManager(Properties.Settings.Default.ConStr);
            manager.UpdatePerson(person);
            return Json(person);
        }

    }
}
