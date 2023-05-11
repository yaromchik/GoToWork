using GoToWork_DataAccess.Repository.IRepository;
using GoToWork_Models.Models;
using GoToWork_Models.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace GoToWork.Controllers
{
	public class LanguageController : Controller
	{
		private readonly ILanguageRepository _languageRepo;

		public LanguageController(ILanguageRepository languageRepo)
        {
			_languageRepo = languageRepo;
		}

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
		{
			var langs = _languageRepo.GetAll();
			return View(langs);
		}

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var obj=new Language();
			return View(obj);
        }

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Language obj)
		{
			if (ModelState.IsValid)
			{
				_languageRepo.Add(obj);
				_languageRepo.Save();
			}
			return RedirectToAction("Index");
		}

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Language lang = _languageRepo.Find(id);

            if (lang == null)
            {
                return NotFound();
            }

            return View(lang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Language obj)
        {
            if (ModelState.IsValid)
            {
                _languageRepo.Update(obj);
                _languageRepo.Save();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var obj = _languageRepo.Find(id);
            _languageRepo.Remove(obj);
            _languageRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
