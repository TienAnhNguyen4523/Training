// Importing necessary namespaces for token generation and handling.
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Authentication.TokenValidators
{
	// JwtModel class is used to represent the structure and configuration of a JSON Web Token (JWT).
	public class JwtModel
	{
		#region Properties

		// Issuer of the token, typically the application or service issuing the token.
		public string Issuer { get; set; }

		// Subject of the token, generally the user or entity the token represents.
		public string Subject { get; set; }

		// Audience of the token, which represents the recipients that the token is intended for.
		public string Audience { get; set; }

		// Lifetime of the token in minutes.
		public int LifeTime { get; set; }

		// Security key used for signing the token. This should be kept secret and secure.
		public string SecurityKey { get; set; }

		// Private field to hold the signing credentials.
		private SigningCredentials _signingCredentials;

		// Private field to hold the symmetric security key.
		private SymmetricSecurityKey _symmetricSecurityKey;

		// Property to get the symmetric security key. It initializes the key if it is not already initialized.
		public SymmetricSecurityKey SigningKey
		{
			get
			{
				if (_symmetricSecurityKey == null)
					// Encoding the security key to byte array using ASCII encoding.
					_symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
				return _symmetricSecurityKey;
			}
		}

		// Property to get the signing credentials. It initializes the credentials if they are not already initialized.
		public SigningCredentials SigningCredentials
		{
			get
			{
				if (_signingCredentials == null)
					// Creating signing credentials using the symmetric security key and the HMAC SHA256 algorithm.
					_signingCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
				return _signingCredentials;
			}
		}

		#endregion
	}
}