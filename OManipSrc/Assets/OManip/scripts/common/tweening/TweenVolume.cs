using UnityEngine;

namespace cpGames.Common
{
    public class TweenVolume : MonoBehaviour
    {
        private float _ctr;

        public AudioSource source;

        public AnimationCurve curve;

        public float time = 1.0f;

        void OnEnable()
        {
            Reset();
        }

        void Update()
        {
            if (_ctr <= time)
            {
                SetVolume(_ctr / time);
                _ctr += Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        void SetVolume(float val)
        {
            source.volume = curve.Evaluate(val);
        }

        public void Reset()
        {
            _ctr = 0;
            SetVolume(0);
        }
    }
}