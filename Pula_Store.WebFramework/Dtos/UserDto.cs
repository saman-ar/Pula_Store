using Entities.AppIdentities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFramework.Dtos
{
	public class UserDto
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public bool IsActive { get; set; }
	}

	//public class AppUserDtos
	//{
	//	public List<UserDto> UserDtos { get; set; }
	//}

}
