using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;

namespace OManip.game
{
    public class ToolbarHUDView : HUDView
    {
        [Inject]
        public ToolSelectedSignal toolSelectedSignal { get; set; }

        protected override void Start()
        {
            base.Start();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override void ShowHud(TransformableObjectView tObject)
        {
            base.ShowHud(tObject);

            float delta = 360.0f / elements.Count;
            float angle = 0;

            foreach (var element in elements)
            {
                (element as ToolbarGizmoView).angle = angle;                
                angle += delta;
                element.Show(tObject);
            }
        }

        protected override void Update()
        {
            base.Update();
            transform.rotation = Camera.main.transform.rotation;
        }

        public void TranslateToolSelected()
        {
            toolSelectedSignal.Dispatch(new ToolModel() { toolType = ToolTypeEnum.Translate });
        }

        public void RotateToolSelected()
        {
            toolSelectedSignal.Dispatch(new ToolModel() { toolType = ToolTypeEnum.Rotate });
        }

        public void ScaleToolSelected()
        {
            toolSelectedSignal.Dispatch(new ToolModel() { toolType = ToolTypeEnum.Scale });
        }

        public void CloseToolbar()
        {

        }
    }
}