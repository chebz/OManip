using UnityEngine;
using System.Collections;

namespace cpGames.Common
{
    public class TweenPosition : Tween
    {
        private Vector3 _positionStart;

        public Vector3 positionFrom;

        public Vector3 positionTo;

        protected override void Awake()
        {
            _positionStart = target.position;
        }

        protected override void Transition(float val)
        {
            target.position = Vector3.Lerp(positionFrom, positionTo, val);
        }

        public override void Reset()
        {
            base.Reset();
            target.position = _positionStart;
        }
    }
}