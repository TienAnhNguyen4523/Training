using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Command.Roles;
using Tranining.Domain.Service.Interface.User;
using Tranining.Domain.ViewModel;
using Tranining.Domain.ViewModel.User;
using Training.Domain.ViewModel.User;

namespace Tranining.Domain.Service.Implementation.User
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;
        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<bool> CreateAccount(CreateUserCommand request)
        {
            return await _mediator.Send(request);
        }
		public async Task<bool> UpdateAccount(UpdateUserCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeleteAccount(DeleteUserCommand request)
		{
			return await _mediator.Send(request);
		}
        public async Task<PaginationSet<AccountViewmodel>> Listing(GetUserPaginationCommand request)
        {
            try
            {
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }
        }
		public async Task<List<AccountViewmodel>> Searching(GetUserSearchingCommand request)
		{
			try
			{
				return await _mediator.Send(request);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.ToString());
			}
		}
		public async Task<List<AccountViewmodel>> Filtering(GetUserFilteringCommand request)
		{
			try
			{
				return await _mediator.Send(request);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.ToString());
			}
		}
		public async Task<TokenViewModel> Login(LoginModelCommand request)
		{
			try
			{
				return await _mediator.Send(request);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.ToString());
			}
		}
	}
}
