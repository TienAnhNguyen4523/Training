using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Data.Infrastructure.Interfaces;
using Tranining.Domain.Command.ExportBills;

namespace Tranining.Domain.Handler.ExportBills
{
	public class DeleteExportBillHandler : BaseHandler,IRequestHandler<DeleteExportBillCommand,bool>
	{
		//public DeleteExportBillHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		public DeleteExportBillHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext) : base(unitOfWork, httpContext)
		{
		}

		public async Task<bool> Handle(DeleteExportBillCommand request,CancellationToken cancellationToken)
		{
			var exportBill = await _unitOfWork.ExportBill.GetById(request.Id);
			var listRole = String.Join(",", GetCurrentRoles());
			
			if (GetCurrentUserId() != exportBill.CreatedBy && !listRole.Contains("Admin"))
			{
				throw new ArgumentException("U can't change the data that you are not creator except admin");
			}
			
			if (exportBill == null)
			{
				throw new ArgumentException("Not Found");
			}
			await _unitOfWork.ExportBill.Delete(exportBill);
			return await _unitOfWork.CommitAsync() > 0;
		}
	}
}
