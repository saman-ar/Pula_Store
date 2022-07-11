using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Extensions
{
	public static class ListNullOrEmptyExtensions
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable == null || !enumerable.Any();
		}

		public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable != null && enumerable.Count() == 0;
		}

	}
}
