using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Authentication.Requirements
{
	// SolidAccountRequirement class is used to define a custom authorization requirement.
	public class SolidAccountRequirement : IAuthorizationRequirement		
	{
		// This class implements the IAuthorizationRequirement interface,
		// indicating that it represents a requirement that needs to be fulfilled
		// for authorization to succeed.

		// Additional properties and methods can be added here to represent specific
		// requirements or conditions that need to be checked for authorization.
	}
}
