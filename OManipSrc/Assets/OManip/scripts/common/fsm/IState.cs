using UnityEngine;
using System.Collections;

namespace cpGames.common
{
    public interface IState
    {
        IStateContext Context { get; set; }

        void Start();

        void Update();

        void FixedUpdate();

        void End();
    }
}