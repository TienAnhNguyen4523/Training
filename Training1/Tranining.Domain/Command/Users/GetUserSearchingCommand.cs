using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Command.Users
{
	public class GetUserSearchingCommand : IRequest<List<AccountViewmodel>>
	{
		public string SearchConditions { get; set; }
	}


}
