using UnityEngine;
using System.Collections;

namespace cpGames.Common
{
    public class TweenLocalPosition : Tween
    {
        private Vector3 _positionStart;

        public Vector3 positionFrom;

        public Vector3 positionTo;

        protected override void Awake()
        {
            _positionStart = target.localPosition;
        }

        protected override void Transition(float val)
        {
            target.localPosition = Vector3.Lerp(positionFrom, positionTo, val);
        }

        public override void Reset()
        {
            base.Reset();
            target.localPosition = _positionStart;
        }
    }
}