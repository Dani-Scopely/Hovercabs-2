using Hovercabs.Managers;

namespace Hovercabs.FSM.States.Base
{
    public abstract class State
    {
        protected GameManager GameManager;
        protected string LastSceneId;
        
        protected State(GameManager gameManager, string lastSceneId = null)
        {
            GameManager = gameManager;
            LastSceneId = lastSceneId;
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
