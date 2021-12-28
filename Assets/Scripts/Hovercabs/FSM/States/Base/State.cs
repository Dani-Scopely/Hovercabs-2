using Hovercabs.Managers;

namespace Hovercabs.FSM.States.Base
{
    public abstract class State
    {
        protected GameManager GameManager;
        
        protected State(GameManager gameManager)
        {
            GameManager = gameManager;
        }

        public virtual void Start()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void Stop()
        {
            
        }
    }
}
