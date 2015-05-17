using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;

namespace OManip.game
{
    public class GameContext : MVCSContext
    {
        public GameContext(MonoBehaviour view)
            : base(view)
        {
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>();
        }

        protected override void mapBindings()
        {
            injectionBinder.Bind<ObjectClickedSignal>().ToSingleton();
            injectionBinder.Bind<ToolSelectedSignal>().ToSingleton();
        }
    }
}