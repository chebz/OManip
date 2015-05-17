using UnityEngine;
using System.Collections;

namespace cpGames.common
{
    public abstract class State : IState
    {
        private bool _transitioning = false;

        private IState _next = null;

        private float _delay;

        public abstract IStateContext Context { get; set; }

        public virtual void Start()
        {
            _next = null;
            _delay = 0;
            _transitioning = false;
        }

        public virtual void End()
        {
        }

        public virtual void Update()
        {
            if (_transitioning)
            {
                if (_delay > 0)
                    _delay -= Time.deltaTime;
                else
                {
                    _transitioning = false;
                    Context.SetState(_next);
                }
            }
        }

        public virtual void FixedUpdate()
        {
        }

        public void Transition(IState state, float delay)
        {
            if (delay == 0)
            {
                _transitioning = false;
                Context.SetState(state);
            }
            else
            {
                _next = state;
                _delay = delay;
                _transitioning = true;
            }
        }
    }
}