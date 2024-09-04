using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.Command.Statistic;
using Tranining.Domain.Service.Implementation.Statistic;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Service.Interface.Statistic
{
	public interface IStatisticService
	{
		Task<StatisticViewModel> RevenueStatistic(StatisticCommand request);
	}
}
