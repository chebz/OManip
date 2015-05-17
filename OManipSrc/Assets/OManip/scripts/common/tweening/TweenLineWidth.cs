using UnityEngine;
using System.Collections;

namespace cpGames.Common
{
    public class TweenLineWidth : Tween
    {
        private LineRenderer _line;

        public float widthFrom;

        public float widthTo;

        protected override void Awake()
        {
            base.Awake();
            _line = target.GetComponent<LineRenderer>();
        }

        protected override void Transition(float val)
        {
            float width = Mathf.Lerp(widthFrom, widthTo, val);
            _line.SetWidth(width, width);
        }
    }
}