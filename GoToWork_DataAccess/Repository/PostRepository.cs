using GoToWork_DataAccess.Repository.IRepository;
using GoToWork_Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoToWork_DataAccess.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _db;

        public PostRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void AddComment(Comment comment)
        {
            _db.Comments.Add(comment);
        }

        public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
        {
            if (obj== "Language")
            {
                return  _db.Language.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            }
            return null;
        }
        public IEnumerable<Comment> GetComments(int id)
        {
            var comments = _db.Comments.Where(u => u.PostId == id);
            return comments; 
        }
       

        public void Update(Post obj)
        {
            var objFromDb = base.FirstOrDefault(u=>u.Id==obj.Id);
            if (objFromDb != null)
            {
                objFromDb.IsVerified = true;
                objFromDb.Answer = obj.Answer;
                objFromDb.ShortQuestion = obj.ShortQuestion;
                objFromDb.Question = obj.Question;
                objFromDb.AuthorName = obj.AuthorName;
                objFromDb.Date = obj.Date;
                //objFromDb.Image = obj.Image;
                objFromDb.LanguageId= obj.LanguageId;
            }
        }
    }
}

