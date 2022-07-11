using Data.AppContext;
using Entities.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repo
{
	public class CategoryRepo
	{
		private readonly AppDbContext _context;
		public CategoryRepo(AppDbContext context)
		{
			_context = context;
		}

		public IList<Category> GetAllParentCategory()
		{
			var categories = _context.Categories.Where(c => c.ParentId == null).ToList();
			return categories;
		}

		public Category GetCategoryById(int Id)
		{
            throw new NotImplementedException();
		}

		public Category GetSubCategories(Category category)
		{
			_context.Attach(category);
			_context.Entry(category).Collection(c => c.SubCategories).Query().AsTracking().Load();

			return category;
		}

        public void Update(CategoryViewModel model)
        {
            throw new NotImplementedException();
        }

    }
}
