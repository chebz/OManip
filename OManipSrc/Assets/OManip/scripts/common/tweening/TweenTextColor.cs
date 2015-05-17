using UnityEngine;
using UnityEngine.UI;

namespace cpGames.Common
{
    public class TweenTextColor : Tween
    {
        private Color _colorStart;

        private Text _text;

        public Color colorFrom;

        public Color colorTo;

        void Awake()
        {
            _text = target.GetComponent<Text>();
            _colorStart = _text.color;
        }

        protected override void Transition(float val)
        {
            _text.color = Color.Lerp(colorFrom, colorTo, val);
        }

        public override void Reset()
        {
            base.Reset();
            _text.color = _colorStart;
        }
    }
}