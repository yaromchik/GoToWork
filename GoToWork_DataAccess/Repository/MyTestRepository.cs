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
    public class MyTestRepository : Repository<MyTest>, IMyTestRepository
    {
        private readonly ApplicationDbContext _db;

        public MyTestRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
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

        public void Update(MyTest obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.GuideUrl=obj.GuideUrl;
                objFromDb.Question=obj.Question;
                objFromDb.Answer_1=obj.Answer_1;
                objFromDb.Answer_2=obj.Answer_2;
                objFromDb.Answer_3=obj.Answer_3;
                objFromDb.Answer_4=obj.Answer_4;
                objFromDb.Number_true=obj.Number_true;
                objFromDb.LanguageId=obj.LanguageId;
            }
        }
    }
}

