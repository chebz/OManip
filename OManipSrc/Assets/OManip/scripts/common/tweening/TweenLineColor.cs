using UnityEngine;
using System.Collections;

namespace cpGames.Common
{
    public class TweenLineColor : Tween
    {
        private LineRenderer _line;

        public Color colorFrom;

        public Color colorTo;

        protected override void Awake()
        {
            base.Awake();
            _line = target.GetComponent<LineRenderer>();
        }

        protected override void Transition(float val)
        {
            Color c = Color.Lerp(colorFrom, colorTo, val);
            _line.SetColors(c, c);
        }
    }
}