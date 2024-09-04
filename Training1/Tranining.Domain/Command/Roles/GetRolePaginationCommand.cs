using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Users;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;

namespace Tranining.Domain.Command.Roles
{
	public class GetRolePaginationCommand : IRequest<PaginationSet<RoleBViewModel>>
	{
		public List<SearchObjForCondition>? SearchRole { get; set; }
		public List<SearchObjForCondition>? FilterRole { get; set; }
		public SearchObjForCondition? Sort { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
	}
}
