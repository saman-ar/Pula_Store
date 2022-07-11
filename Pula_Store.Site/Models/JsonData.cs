using Site.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models
{
	public class JsonData
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public List<SubCategoryViewModel> Data { get; set; }
	}
}
