using UnityEngine;
using UnityEngine.UI;

namespace cpGames.Common
{
    [RequireComponent(typeof(Image))]
    public class TweenImageColor : Tween
    {        
        private Image _image;

        public Color colorFrom;

        public Color colorTo;

        protected override void Awake()
        {
            base.Awake();
            _image = GetComponent<Image>();
        }

        protected override void Transition(float val)
        {
            Color c = Color.Lerp(colorFrom, colorTo, val);
            _image.color = c;
        }

        public override void Reset()
        {
            base.Reset();
            Transition(curve.Evaluate(0));
        }
    }
}