using UnityEngine;
using System.Collections;

namespace cpGames.Common
{
    public class TweenDeltaRectPosition : Tween
    {
        private RectTransform _rectTransform;

        private Vector3 _positionFrom;

        private Vector3 _positionTo;

        public Vector3 delta;

         protected override void Awake()
        {
            base.Awake();
            _rectTransform = target.GetComponent<RectTransform>();
        }

         protected override void Transition(float val)
         {
             _rectTransform.position = Vector3.Lerp(_positionFrom, _positionTo, val);
         }

        public override void Reset()
        {
            base.Reset();
            _positionFrom = _rectTransform.position;
            _positionTo = _positionFrom + delta;
        }
    }
}