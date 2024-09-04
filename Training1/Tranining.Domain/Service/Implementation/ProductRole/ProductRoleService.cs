using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Categories;
using Tranining.Domain.Command.ProductRoles;
using Tranining.Domain.Service.Interface.IProductRoleService;

namespace Tranining.Domain.Service.Implementation.ProductRole
{
	public class ProductRoleService : IProductRoleService
	{
		private readonly IMediator _mediator;
		public ProductRoleService(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<bool> CreateProductRole(CreateProductRoleCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> DeleteProductRole(DeleteProductRoleCommand request)
		{
			return await _mediator.Send(request);
		}
		public async Task<bool> UpdateProductRole(UpdateProductRoleCommand request)
		{
			return await _mediator.Send(request);
		}
	}
}
