using UnityEngine;
using strange.extensions.mediation.impl;
using cpGames.Common;

namespace OManip.game
{
    public abstract class HUDElementView : View
    {
        protected TweenScale _tweenScale;

        protected TweenLocalPosition _tweenPosition;

        protected abstract Vector3 positionTo { get; }

        protected TransformableObjectView _tObject;
        
        public Transform control;

        public LineAttachment line;

        public float maxRadius = 0.5f;

        public float distance = 2.0f;

        protected override void Awake()
        {
            base.Awake();

            _tweenScale = control.GetComponent<TweenScale>();
            _tweenPosition = control.GetComponent<TweenLocalPosition>();
        }

        protected virtual void Update()
        {
            line.r2 = _tweenScale.transform.localScale.x * maxRadius;
        }

        public virtual void Show(TransformableObjectView tObject)
        {
            _tObject = tObject;

            gameObject.SetActive(true);

            _tweenScale.scaleFrom = _tweenScale.transform.localScale;
            _tweenScale.scaleTo = Vector3.one;
            _tweenScale.ResetAndPlay();

            _tweenPosition.positionFrom = _tweenPosition.transform.localPosition;
            _tweenPosition.positionTo = positionTo;
            _tweenPosition.ResetAndPlay();

            _tweenScale.callOnFinish = null;
        }

        public virtual void Hide()
        {
            _tweenPosition.positionFrom = _tweenPosition.transform.localPosition;
            _tweenPosition.positionTo = Vector3.zero;
            _tweenPosition.ResetAndPlay();

            _tweenScale.scaleFrom = _tweenScale.transform.localScale;
            _tweenScale.scaleTo = Vector3.zero;
            _tweenScale.ResetAndPlay();

            _tweenScale.callOnFinish =
                delegate
                {
                    gameObject.SetActive(false);
                    _tObject = null;
                };
        }
    }
}