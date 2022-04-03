using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleAjax3.Data;
using PeopleAjax3.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleAjax3.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connString = @"Data Source=.\sqlexpress;Initial Catalog=FurnitureStore;Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var peopleRepo = new PeopleRepository(_connString);
            return Json(peopleRepo.GetAllPeople());
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            var peopleRepo = new PeopleRepository(_connString);
            peopleRepo.AddPerson(person);
        }

        [HttpPost]
        public void EditPerson(Person person)
        {
            var peopleRepo = new PeopleRepository(_connString);
            peopleRepo.EditPerson(person);
        }

        [HttpPost]
        public void DeletePerson(int id)
        {
            var peopleRepo = new PeopleRepository(_connString);
            peopleRepo.DeletePerson(id);
        }
    }
}
