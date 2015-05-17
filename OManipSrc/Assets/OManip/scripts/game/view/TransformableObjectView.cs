using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.EventSystems;

namespace OManip.game
{
    [RequireComponent(typeof(Collider))]
    public class TransformableObjectView : View
    {
        [Inject]
        public ObjectClickedSignal objectClickedSignal { get; set; }

        public Vector3 scaleOffset = Vector3.zero;

        public Vector3 rotOffset = Vector3.zero;

        void OnMouseDown()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                objectClickedSignal.Dispatch(this);
        }
    }
}