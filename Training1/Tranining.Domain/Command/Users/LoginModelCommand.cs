using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.ViewModel.User;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Command.Users
{
	public class LoginModelCommand : IRequest<TokenViewModel>
	{
		public string UserName { get; set; }
		public string Password { get; set; }	
	}
}
