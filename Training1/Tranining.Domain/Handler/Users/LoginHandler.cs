using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Domain.ViewModel.User;
using Tranining.Domain.Command.Users;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Handler.Users
{
	public class LoginHandler : UserBaseHandler,IRequestHandler<LoginModelCommand, TokenViewModel>
	{
		public LoginHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<TokenViewModel> Handle(LoginModelCommand request, CancellationToken cancellationToken)
		{
			var account = await _unitOfWork.User.GetQueryable(x => x.UserId == request.UserName)
				.Select(x => new AccountViewmodel
				{
					Id = x.Id,
					UserId = x.UserId,
					Password = x.UserDetails.Password,
					Email = x.UserDetails.Email,
					FullName = x.UserDetails.FullName,
					Address = x.UserDetails.Address,
					PhoneNumber = x.UserDetails.Phone,
					Roles = x.UserRoles
					.Select(r => new RoleBViewModel
					{
						Id = r.Id,
						Name = r.Role.RoleName
					}).ToList(),
					BirthOfDate = x.UserDetails.BirthOfDate,
					Gender = x.UserDetails.Gender,
					National = x.UserDetails.National,
					NationalId = x.UserDetails.NationalId,
					Permissions = x.UserPermission.ListPermission
				}).FirstOrDefaultAsync();
			if (account == null)
			{
				throw new ArgumentException("Username does not exist");
			}
			
			if(account.Password != Encrypt(request.Password))
			{
				throw new ArgumentException("Incorrect password");
			}

			account.Password = Decrypt(account.Password);
			var token = new TokenViewModel();
			token.UserProfile = account;
			return token;
				
		}
		
	}
}
