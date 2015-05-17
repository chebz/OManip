using UnityEngine;

namespace cpGames.Common
{
    public class TweenScale : Tween
    {
        private Vector3 _scaleStart;

        public Vector3 scaleFrom;

        public Vector3 scaleTo;

        protected override void Awake()
        {
            base.Awake();
            _scaleStart = target.localScale;
        }

        protected override void Transition(float val)
        {
            target.localScale = Vector3.Lerp(scaleFrom, scaleTo, val);
        }

        public override void Reset()
        {
            base.Reset();
            target.localScale = _scaleStart;
        }
    }
}