using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranining.Domain.ViewModel.User;

namespace Tranining.Domain.Command.Statistic
{
	public class StatisticCommand : IRequest<StatisticViewModel>
	{
		public int? Season { get; set; }
		public int? Month { get; set; }
		public int? Year { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}
