using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using GoToWork_DataAccess;
using GoToWork_DataAccess.Repository.IRepository;
using GoToWork_Models.Models;
using GoToWork_Models.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GoToWork.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(IPostRepository postRepo, IWebHostEnvironment webHostEnvironment)
        {
            _postRepo = postRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index(int id, int page = 0)
        {
            PostIndexVM postVM = new PostIndexVM();
            int postInPage = 4;
            int count = _postRepo.GetAll(filter: c => c.IsVerified == true & c.LanguageId == id).Count();
            postVM.CountPage = (int)Math.Ceiling((double)count / (double)postInPage);
            postVM.Page = page;

            if (page != 0)
            {
                postVM.Postic = _postRepo.GetAll(filter: c => c.IsVerified == true & c.LanguageId == id).Skip(page* postInPage).Take(postInPage);
                if (postVM.Postic.Count() == 0)
                {
                    return NotFound();
                }
                postVM.Title = _postRepo.GetLanguage(id).Description;
                postVM.Language = _postRepo.GetLanguage(id).Name;
            }
            else
            {
                postVM.Postic = _postRepo.GetAll(filter: c => c.IsVerified == true & c.LanguageId == id).Take(postInPage);
                if (postVM.Postic.Count()==0)
                {
                    return NotFound();
                }
                postVM.Title = _postRepo.GetLanguage(id).Description;
                postVM.Language = _postRepo.GetLanguage(id).Name;
            }

            return View(postVM);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Moderation()
        {
            IEnumerable<Post> objList = _postRepo.GetAll(filter: c => c.IsVerified == false);

            foreach (var obj in objList)
            {
                obj.Language = _postRepo.GetLanguage(obj.LanguageId);
            }
            return View(objList);
        }

        public IActionResult Create()
        {
            IndexVM postVM = new IndexVM()
            {
                LanguageSelectList = _postRepo.GetAllDropdownList("Language")
            };
            return View(postVM);
        }

        public IActionResult Details(int id, int page)
        {
            DetailsVM postic = new DetailsVM
            {
                Page=page,
                Postic = _postRepo.Find(id),
                CommentList = _postRepo.GetComments(id)
            };

            if(postic.Postic==null)
            {
                return NotFound();
            }    

            postic.Postic.Language = _postRepo.GetLanguage(postic.Postic.LanguageId);
            return View(postic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IndexVM post)
        {
            if (ModelState.IsValid)
            {
                post.Postic.Date = DateTime.Now;
                post.Postic.Language = _postRepo.GetLanguage(post.Postic.LanguageId); /*_db.Language.First(u => post.Postic.LanguageId == u.Id);*/
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (files.Count != 0)
                {
                    string upload = webRootPath + "/images/post";
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    post.Postic.Image = fileName + extension;
                }
                _postRepo.Add(post.Postic);
                _postRepo.Save();
                return Redirect("~/Post/Index/" + post.Postic.Language.Id);
            }
            else return NotFound();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            IndexVM obj = new IndexVM()
            {
                Postic = _postRepo.Find(id),
                LanguageSelectList = _postRepo.GetAllDropdownList("Language")
            };
            if (obj.Postic == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IndexVM post)
        {
            if (ModelState.IsValid)
            {
                post.Postic.Date = DateTime.Now;
                post.Postic.Language = _postRepo.GetLanguage(post.Postic.LanguageId);

                _postRepo.Update(post.Postic);
                _postRepo.Save();
                return Redirect("~/Post/Details/" + post.Postic.Id);
            }
            return View(post);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var post = _postRepo.Find(id);
            _postRepo.Remove(post);
            _postRepo.Save();
            return Redirect("~/Post/Index/" + post.LanguageId);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(string name, int id, string textik)
        {
            if (textik != null)
            {
                Comment comment = new Comment()
                {
                    Name = name,
                    Date = DateTime.Now,
                    PostId = id,
                    Text = textik
                };
                _postRepo.AddComment(comment);
                _postRepo.Save();
                return Redirect("~/Post/Details/" + id);
            }
            return NotFound();
        }
    }
}

