using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Users;

namespace Tranining.Domain.Handler.Users
{
	internal class UpdateUserHandler : UserBaseHandler, IRequestHandler<UpdateUserCommand, bool>
	{
		public UpdateUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			
			var user = await _unitOfWork.User.GetOne(x => x.Id == request.Id);
			var userDetail = await _unitOfWork.UserDetail.GetOne(x => x.UserId == user.Id);
			

			if (user == null)
			{
				throw new ArgumentException("Cannot find id");

			}
			else
			{
				userDetail.FirstName = request.FirstName;
				userDetail.LastName = request.LastName;
				userDetail.FullName = string.Concat(request.FirstName, " ", request.LastName);
				userDetail.Address = request.Address;
				userDetail.BirthOfDate = request.BirthOfDate;
				userDetail.Email = request.Email;
				userDetail.Gender = request.Gender;
				userDetail.Password = Encrypt(request.Password);
				userDetail.Phone = request.Phone;
				userDetail.National = request.National;
				userDetail.NationalId = request.NationalId;
			}
		


			_unitOfWork.UserDetail.Update(userDetail);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
