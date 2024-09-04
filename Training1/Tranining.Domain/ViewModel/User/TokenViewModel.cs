// Importing necessary namespaces for the class.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.ViewModel.User;

namespace Training.Domain.ViewModel.User
{
	// TokenViewModel class is used to represent a token with its associated details.
	public class TokenViewModel
	{
		#region Properties

		// The actual token string.
		public string Code { get; set; }

		// The expiration time of the token in a double format, typically representing a timestamp.
		public double Expiration { get; set; }

		// The lifetime of the token in minutes.
		public int LifeTime { get; set; }

		// User profile associated with the token.
		public AccountViewmodel UserProfile { get; set; }

		#endregion

		#region Constructors

		// Default constructor.
		public TokenViewModel() { }

		// Parameterized constructor to initialize all properties.
		public TokenViewModel(string code, double expiration, int lifeTime, AccountViewmodel userProfileViewModel)
		{
			Code = code;  // Assign the token code.
			Expiration = expiration;  // Assign the token expiration time.
			LifeTime = lifeTime;  // Assign the token lifetime.
			UserProfile = userProfileViewModel;  // Assign the associated user profile.
		}

		#endregion
	}
}