using Hovercabs.FSM.States.Base;
using Utils;

namespace Hovercabs.Events
{
    public class OnStateChanged : IEvent
    {
        public State State;
    }
}