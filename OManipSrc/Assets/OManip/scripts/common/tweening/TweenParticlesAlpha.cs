using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace cpGames.Common
{
    public class TweenParticlesAlpha : MonoBehaviour
    {
        private List<ParticleSystem> _particles;

        private List<Color> _originalColors = new List<Color>();

        private float _ctr;

        public Transform particlesRoot;

        public AnimationCurve curve;

        public float time = 1.0f;

        public bool disableAfterDone = false;

        void Awake()
        {
            _particles = Utils.FindAllChildrenRecursively<ParticleSystem>(particlesRoot);
            _particles.ForEach(x => _originalColors.Add(x.startColor));
        }

        void OnEnable()
        {
            Reset();
        }

        void Update()
        {
            if (_ctr <= time)
            {
                SetAlpha(_ctr / time);
                _ctr += Time.deltaTime;
            }
            else
            {
                if (disableAfterDone)
                    particlesRoot.gameObject.SetActive(false);
                _ctr = 0;
            }
        }

        void SetAlpha(float val)
        {
            float alpha = curve.Evaluate(val);
            for (int i = 0; i < _particles.Count; i++)
            {
                Color c = _originalColors[i];
                c.a = alpha;
                _particles[i].startColor = c;
            }
        }

        public void Reset()
        {
            _ctr = 0;
            SetAlpha(0);
            if (!particlesRoot.gameObject.activeSelf)
                particlesRoot.gameObject.SetActive(true);
        }
    }
}