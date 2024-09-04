using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Training.Data.Infrastructure.Interfaces;
using Training.Entity.EntityModel;
using Tranining.Domain.Command.Users;

namespace Tranining.Domain.Handler.Users
{
	public class DeleteUserHandler : UserBaseHandler, IRequestHandler<DeleteUserCommand, bool>
	{
		public DeleteUserHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

		public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			
			
			var user = await _unitOfWork.User.GetOne(x => x.Id == request.Id);
			var userDetail = await _unitOfWork.UserDetail.GetOne(x => x.UserId == user.Id);
			var userRole = await _unitOfWork.UserRole.GetOne(x=>x.UserId == user.Id);

			await _unitOfWork.UserRole.Delete(userRole);
			await _unitOfWork.UserDetail.Delete(userDetail);
			await _unitOfWork.User.Delete(user);
			
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
