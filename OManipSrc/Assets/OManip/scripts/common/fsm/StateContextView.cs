using strange.extensions.mediation.impl;

namespace cpGames.common
{
    public class StateContextView : View, IStateContext
    {
        public IState ActiveState { get; set; }

        protected override void OnDestroy()
        {
            SetState(null);
            base.OnDestroy();
        }

        public void SetState(IState state)
        {
            if (ActiveState == state)
                return;

            if (ActiveState != null)
                ActiveState.End();

            ActiveState = state;

            if (ActiveState != null)
            {
                ActiveState.Context = this;
                ActiveState.Start();
            }
        }

        protected virtual void Update()
        {
            if (ActiveState != null)
                ActiveState.Update();
        }

        protected virtual void FixedUpdate()
        {
            if (ActiveState != null)
                ActiveState.FixedUpdate();
        }
    }
}