using GoToWork_Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWork_DataAccess.Repository.IRepository
{
    public interface IPostRepository:IRepository <Post>
    {
        void Update(Post obj);
        IEnumerable<SelectListItem> GetAllDropdownList(string obj);
        public IEnumerable<Comment> GetComments(int id);
        void AddComment(Comment comment);
    }
}
