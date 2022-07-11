using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.Extensions
{
	public static class ContextExtensions
	{
		public static bool DbExist(this DatabaseFacade database)
		{
			return (database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();
		}
	}
}
