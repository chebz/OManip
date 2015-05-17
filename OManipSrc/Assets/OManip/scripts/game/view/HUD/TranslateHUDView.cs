using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

namespace OManip.game
{
    public class TranslateHUDView : HUDView
    {

        public override void ShowHud(TransformableObjectView tObject)
        {
            base.ShowHud(tObject);

            elements.ForEach(x => x.Show(tObject));
        }
    }
}