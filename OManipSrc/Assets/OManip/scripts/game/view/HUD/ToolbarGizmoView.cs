using UnityEngine;

namespace OManip.game
{
    public class ToolbarGizmoView : HUDElementView
    {
        protected override Vector3 positionTo
        {
            get { return Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up * distance; }
        }

        public float angle = 0;
    }
}