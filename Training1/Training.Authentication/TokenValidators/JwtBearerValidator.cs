using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Training.Authentication.TokenValidators
{
	// JwtBearerValidator class is used to validate JWT tokens.
	public class JwtBearerValidator : ISecurityTokenValidator
	{
		#region Methods

		// This method determines if the validator can read the token.
		public bool CanReadToken(string securityToken)
		{
			//Always return true indicating that this validator can read any token.
			return true;
		}

		// This method validates the token and returns the claims principal.
		public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
			out SecurityToken validatedToken)
		{
			// Create an instance of JwtSecurityTokenHandler to handle the token validation.
			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

			// Validate the token and return the claims principal.
			var claimsPrincipal = jwtSecurityTokenHandler.ValidateToken(securityToken, validationParameters, out validatedToken);

			return claimsPrincipal;
		}

		#endregion

		#region Properties

		// Property indicating if the validator can validate tokens.
		public bool CanValidateToken => true;

		// Property to get or set the maximum token size in bytes.
		public int MaximumTokenSizeInBytes { get; set; }

		#endregion
	}
}