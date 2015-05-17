using UnityEngine;
using System.Collections;
using System;

namespace cpGames.Common
{
    public abstract class Tween : MonoBehaviour
    {
        public enum ModeEnum
        {
            Once,
            Loop,
            Reverse
        }

        private bool _playing = false;

        private float _t = 0;

        private float _sign = 1;

        private float _progress;

        private float _value;

        public ModeEnum mode;

        public bool playOnStart = true;

        public AnimationCurve curve;

        public float time = 1.0f;

        public Action callOnFinish = null;

        public Transform target;

        public bool IsPlaying { get { return _playing; } }

        public float progress { get { return _progress; } }

        public float value { get { return _value; } }

        protected virtual void Awake()
        {
        }

        protected virtual void OnEnable()
        {
            if (playOnStart)
            {
                Reset();
                _playing = true;
            }
        }

        protected virtual void OnDisable()
        {
            _playing = false;
        }

        void Update()
        {
            if (_playing)
            {
                if ((_t <= time && _sign > 0) || 
                    (_t >= 0 && _sign < 0))
                {
                    _progress = _t / time;
                    _value = curve.Evaluate(Mathf.Clamp01(_progress));
                    Transition(_value);
                    _t += Time.fixedDeltaTime * _sign;
                }
                else
                {
                    switch (mode)
                    {
                        case ModeEnum.Once:
                            _playing = false;
                            if (callOnFinish != null)
                                callOnFinish();
                            break;
                        case ModeEnum.Loop:
                            Reset();
                            break;
                        case ModeEnum.Reverse:
                            _sign *= -1;
                            break;
                    }
                }
            }
        }

        protected abstract void Transition(float val);

        public void Play()
        {
            _playing = true;
        }

        public void ResetAndPlay()
        {
            Reset();
            _playing = true;
        }

        public void Stop(bool reset)
        {
            _playing = false;
            if (reset)
                Reset();
        }

        public virtual void Reset()
        {
            _t = 0;
            _sign = 1;
        }

    }
}