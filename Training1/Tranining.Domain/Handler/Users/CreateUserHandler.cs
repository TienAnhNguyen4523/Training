using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.Users;
using Training.Entity.EntityModel;

namespace Tranining.Domain.Handler.Users
{
	public class CreateUserHandler : UserBaseHandler, IRequestHandler<CreateUserCommand,bool>
	{
		public CreateUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			var checkUser = await CheckUserExist(request.UserId);
			if (checkUser)
			{
				throw new ArgumentException("Username already exists");

			}
			var id = Guid.NewGuid();
			var user = new User
			{
				Id = id,
				UserId = request.UserId,
				isActive = true
			};
			var userDetail = new UserDetail
			{
				UserId = id,
				FirstName = request.FirstName,
				LastName = request.LastName,
				FullName = string.Concat(request.FirstName," ",request.LastName),
				Address = request.Address,
				BirthOfDate = request.BirthOfDate,
				Email = request.Email,
				Gender = request.Gender,
				Password = Encrypt(request.Password),
				Phone = request.Phone,
				National = request.National,
				NationalId = request.NationalId
			};
			_unitOfWork.User.Insert(user);
			_unitOfWork.UserDetail.Insert(userDetail);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
