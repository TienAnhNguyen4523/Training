using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.ViewModel.User;
using Tranining.Domain.ViewModel;
using Training.Domain.ViewModel.User;

namespace Tranining.Domain.Service.Interface.User
{
	public interface IUserService
	{
		Task<bool> CreateAccount(CreateUserCommand request);
		Task<bool> UpdateAccount(UpdateUserCommand request);
		Task<bool> DeleteAccount(DeleteUserCommand request);
		Task<List<AccountViewmodel>> Searching(GetUserSearchingCommand request);
		Task<List<AccountViewmodel>> Filtering(GetUserFilteringCommand request);

		Task<PaginationSet<AccountViewmodel>> Listing(GetUserPaginationCommand request);
		Task<TokenViewModel> Login(LoginModelCommand request);
		
	}
}
