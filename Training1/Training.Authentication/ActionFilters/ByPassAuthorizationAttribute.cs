using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Authentication.ActionFilters
{
	// ByPassAuthorizationAttribute class is used to define a custom attribute
	// that can be applied to controllers or actions to bypass authorization
	internal class ByPassAuthorizationAttribute : Attribute, IFilterMetadata
	{
		// This class inherits from Attribute and implements IFilterMetadata.
		// IFilterMetadata is a marker interface used to indicate that this class
		// is intended to be used as a filter in the ASP.NET Core pipeline.
	}
}
