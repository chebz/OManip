using UnityEngine;

namespace cpGames.Common
{
    public class TweenRotation : Tween
    {
        private Quaternion _rotationStart;

        public Vector3 rotateFrom;

        public Vector3 rotateTo;

        protected override void Awake()
        {
            _rotationStart = target.localRotation;
        }

        protected override void Transition(float val)
        {
            target.localRotation = Quaternion.Euler(Vector3.Lerp(rotateFrom, rotateTo, val));
        }

        public override void Reset()
        {
            base.Reset();
            target.localRotation = _rotationStart;
        }
    }
}