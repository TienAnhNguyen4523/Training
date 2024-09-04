using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Statistic;
using Tranining.Domain.Command.Users;
using Tranining.Domain.Service.Interface.Statistic;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Service.Implementation.Statistic
{
	public class StatisticService : IStatisticService
	{
		private readonly IMediator _mediator;
		public StatisticService(IMediator mediator)
		{
			_mediator = mediator;
		}
		public async Task<StatisticViewModel> RevenueStatistic(StatisticCommand request)
		{
			return await _mediator.Send(request);
		}
	}
}
