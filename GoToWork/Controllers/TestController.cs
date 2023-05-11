using AspNetCore;
using GoToWork_DataAccess.Repository.IRepository;
using GoToWork_Models.Models;
using GoToWork_Models.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace GoToWork.Controllers
{
    public class TestController : Controller
    {
        private readonly IMyTestRepository _testRepo;

        public TestController(IMyTestRepository testRepo)
        {
            _testRepo = testRepo;
        }

        public ActionResult Index(int id, int count=0)
        {
            TestVM vm = new TestVM();

            vm.CountQ = _testRepo.GetAll(filter: c => c.LanguageId == id).Count();
            if(vm.CountQ==0)
            {
                return NotFound();
            }

            vm.Test =_testRepo.GetAll(filter: c => c.LanguageId == id).ToList()[count];
            vm.Count = count;
            vm.Test.Language = _testRepo.GetLanguage(vm.Test.LanguageId);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Testik(int myRadio,int languageId, int count)
        {
            TestVM vm = new TestVM()
            {
                Test = _testRepo.GetAll(filter: c => c.LanguageId == languageId).ToList()[count],
                CountQ = _testRepo.GetAll(filter: c => c.LanguageId == languageId).Count(),
                Option = myRadio,
                Count= count
            };
            return PartialView(vm);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            CreateTestVM vm = new CreateTestVM()
            {
                LanguageSelectList = _testRepo.GetAllDropdownList("Language")
            };
            return View(vm);
        }

		
		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateTestVM test)
        {
            if (ModelState.IsValid)
            {
                test.Testik.Language = _testRepo.GetLanguage(test.Testik.LanguageId);
                _testRepo.Add(test.Testik);
                _testRepo.Save();
            }
            return Redirect("~/Test/Index/" + test.Testik.Language.Id);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Moderation()
        {
            var items=_testRepo.GetAll();
            foreach (var item in items)
            {
                item.Language=_testRepo.GetLanguage(item.LanguageId);
            }
            return View(items);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            CreateTestVM test = new CreateTestVM();
            test.Testik= _testRepo.Find(id);
            if (test.Testik == null)
            {
                return NotFound();
            }

            test.Testik.Language = _testRepo.GetLanguage(test.Testik.LanguageId);
            test.LanguageSelectList = _testRepo.GetAllDropdownList("Language");
            return View(test);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateTestVM test)
        {
            if (ModelState.IsValid)
            {
                _testRepo.Update(test.Testik);
                _testRepo.Save();
                return RedirectToAction("Moderation");
            }
            return View(test);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var post = _testRepo.Find(id);
            _testRepo.Remove(post);
            _testRepo.Save();
            return RedirectToAction("Moderation");
        }
    }
}
