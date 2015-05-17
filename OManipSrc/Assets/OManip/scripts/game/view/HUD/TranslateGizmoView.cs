using UnityEngine;

namespace OManip.game
{
    public class TranslateGizmoView : HUDElementView
    {
        protected override Vector3 positionTo
        {
            get { return _tObject.transform.rotation * dir.normalized * distance; }
        }

        public Vector3 dir;

        public DraggableControlView draggableControl;

        public float dragSpeed = 1.0f;

        protected override void Start()
        {
            base.Start();

            draggableControl.draggedSignal.AddListener(OnDragged);
        }

        protected override void OnDestroy()
        {
            draggableControl.draggedSignal.RemoveListener(OnDragged);

            base.OnDestroy();
        }

        void OnDragged(Vector3 delta)
        {
            var realDelta = Vector3.Project(delta, _tObject.transform.rotation * dir) * dragSpeed;
            _tObject.transform.position += realDelta;
        }

        protected override void Update()
        {
            base.Update();

            if (_tObject != null)
            {
                control.rotation = _tObject.transform.rotation * Quaternion.LookRotation(dir.normalized);
            }
        }
    }
}