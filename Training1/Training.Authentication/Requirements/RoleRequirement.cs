using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Authentication.Requirements
{
	// RoleRequirement class is used to define a custom authorization requirement based on user roles.
	public class RoleRequirement : IAuthorizationRequirement
	{
	}
}
