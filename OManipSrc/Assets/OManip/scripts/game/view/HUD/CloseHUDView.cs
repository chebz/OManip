using UnityEngine;

namespace OManip.game
{
    public class CloseHUDView : HUDView
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

            elements.ForEach(x => x.Show(tObject));
        }

        protected override void Update()
        {
            base.Update();
            transform.rotation = Camera.main.transform.rotation;
        }

        public void CloseToolbar()
        {
            toolSelectedSignal.Dispatch(null);
        }
    }
}