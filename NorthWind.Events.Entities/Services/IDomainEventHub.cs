using NorthWind.Events.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind.Events.Entities.Services
{
	public interface IDomainEventHub<EventType>
	where EventType : IDomainEvent
	{
		Task Raise(EventType eventTypeInstance);
	}

}
