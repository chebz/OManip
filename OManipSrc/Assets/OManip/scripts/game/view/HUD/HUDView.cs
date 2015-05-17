using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

namespace OManip.game
{
    public class HUDView : View
    {
        protected bool _visible = false;

        protected TransformableObjectView _tObject;

        public List<HUDElementView> elements;
        
        public virtual void ShowHud(TransformableObjectView tObject)
        {
            _visible = true;
            _tObject = tObject;
        }

        public virtual void HideHud()
        {
            _visible = false;
            _tObject = null;
            
            elements.ForEach(x => x.Hide());
        }

        protected virtual void Update()
        {
            if (_tObject != null)
            {
                transform.position = _tObject.transform.position;                
            }
        }
    }
}