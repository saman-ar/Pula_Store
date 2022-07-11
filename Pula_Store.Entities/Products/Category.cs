using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Entities.Products
{
	public class Category
	{
		public int Id { get; set; }

		//[MaxLength(100,ErrorMessage ="خیلی بزرگ شد")]
		public string Name { get; set; }

		public int? ParentId { get; set; }
		[ForeignKey(nameof(ParentId))]
		public Category ParentCategory { get; set; }

		public ICollection<Category> SubCategories { get; set; }

		public ICollection<Product> Products { get; set; }

		public bool IsRemoved { get; set; }

        ///implementing Interface
    }

    public class TrackedEntity : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        internal T NotifyiFChanged<T>(
            T fieldValue,
            T newValue,
            [CallerMemberName] string propertyName="")
        {
            if (!fieldValue?.Equals(newValue) ?? true)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return newValue;
        }
    }
}
