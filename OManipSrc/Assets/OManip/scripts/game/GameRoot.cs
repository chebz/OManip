using strange.extensions.context.impl;
using UnityEngine;

namespace OManip.game
{
    public class GameRoot : ContextView
    {
        void Awake()
        {
            context = new GameContext(this);
        }
    }
}