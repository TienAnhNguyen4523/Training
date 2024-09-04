// Importing necessary namespaces for authorization, JWT, and filters.
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Training.Authentication.ActionFilters;
using Training.Authentication.TokenValidators;

namespace Training.Authentication.Handlers
{
	// Interface definition for profile services, providing methods for JWT handling and bypassing authorization filters.
	public interface IProfileService
	{
		#region Methods
		// Method to generate a JWT based on claims and configuration.
		string GenerateJwt(Claim[] claims, JwtModel jwtConfiguration);

		// Method to decode a JWT into a specified type.
		T DecodeJwt<T>(string token);

		// Method to bypass the authorization filter based on context and requirements.
		void BypassAuthorizationFilter(AuthorizationHandlerContext authorizationHandlerContext,
			IAuthorizationRequirement requirement, bool bAnonymousAccessAttributeCheck = false);
		#endregion
	}

	// Implementation of the IProfileService interface.
	public class ProfileService : IProfileService
	{
		#region Methods

		// Generates a JWT based on claims and configuration.
		public string GenerateJwt(Claim[] claims, JwtModel jwtConfiguration)
		{
			var systemTime = DateTime.Now; // Get the current system time.
			var expiration = systemTime.AddSeconds(jwtConfiguration.LifeTime); // Calculate the expiration time.
			// Create the JWT with the provided configuration and claims.
			var jwt = new JwtSecurityToken(
				jwtConfiguration.Issuer,
				jwtConfiguration.Audience,
				claims,
				systemTime,
				expiration,
				jwtConfiguration.SigningCredentials
			);

			// Write the JWT to a string and return it.
			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}

		// Decodes a JWT into a specified type (placeholder implementation).
		public T DecodeJwt<T>(string token)
		{
			// This method currently returns the default value of the specified type.
			return default(T);
		}

		// Bypasses the authorization filter based on context and requirements.
		public void BypassAuthorizationFilter(AuthorizationHandlerContext authorizationHandlerContext,
			IAuthorizationRequirement requirement, bool bAnonymousAccessAttributeCheck)
		{
			// Check if the anonymous access attribute should be checked.
			if (bAnonymousAccessAttributeCheck)
			{
				// Cast the AuthorizationHandlerContext to AuthorizationFilterContext.
				var authorizationFilterContext = (AuthorizationFilterContext)authorizationHandlerContext.Resource;

				// If no ByPassAuthorizationAttribute is found, return without bypassing.
				if (!authorizationFilterContext.Filters.Any(x => x is ByPassAuthorizationAttribute))
					return;
			}

			// If the user does not have an identity with the name "Anonymous", add it.
			if (authorizationHandlerContext.User.Identities.All(x => x.Name != "Anonymous"))
				authorizationHandlerContext.User.AddIdentity(new GenericIdentity("Anonymous"));

			// Succeed the authorization requirement.
			authorizationHandlerContext.Succeed(requirement);
		}
		#endregion
	}
}