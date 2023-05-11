using GoToWork_Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWork_DataAccess.Repository.IRepository
{
	public interface IMyTestRepository : IRepository<MyTest>
	{
		void Update(MyTest obj);
		IEnumerable<SelectListItem> GetAllDropdownList(string obj);
	}
}
