using UnityEngine;
using System.Collections;

namespace cpGames.Common
{
    public class TweenDeltaPosition : TweenPosition
    {
        public Vector3 delta;

        protected override void Awake()
        {
            Reset(); 
        }

        protected override void Transition(float val)
        {
            target.position = Vector3.Lerp(positionFrom, positionTo, val);
        }

        public override void Reset()
        {
            base.Reset();
            positionFrom = target.position;
            positionTo = positionFrom + delta; 
        }
    }
}