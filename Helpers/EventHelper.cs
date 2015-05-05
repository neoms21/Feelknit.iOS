using System;
using DSoft.Messaging;

namespace Feelknit.iOS
{
	public static class EventHelper
	{

		public static MessageBusEventHandler RegisterEvent(string eventId, Action<object, MessageBusEvent> action)
		{
			var eventHandlerAction = new MessageBusEventHandler () {
				EventId = eventId,
				EventAction = action,
			};

			MessageBus.Default.Register (eventHandlerAction);
			return eventHandlerAction;

		}

		public static void DeRegisterEvent(MessageBusEventHandler action)
		{
			MessageBus.Default.DeRegister(action);
		}
	}
}

