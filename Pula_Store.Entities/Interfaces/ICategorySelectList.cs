using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Interfaces
{
	public interface ICategorySelectList
	{
		ICollection<SelectListItem> ParentCategories { get; set; }
		ICollection<SelectListItem> SubCategories { get; set; }
	}
}
