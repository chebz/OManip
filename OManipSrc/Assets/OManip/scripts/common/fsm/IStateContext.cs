using UnityEngine;
using System.Collections;

namespace cpGames.common
{
    public interface IStateContext
    {
        IState ActiveState { get; set; }

        void SetState(IState state);
    }
}