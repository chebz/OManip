using UnityEngine;
using cpGames.common;
using strange.extensions.signal.impl;

namespace OManip.game
{
    [RequireComponent(typeof(Renderer))]
    public class DraggableControlView : StateContextView
    {
        abstract class DraggableState : State
        {
            protected DraggableControlView _context;

            public override IStateContext Context { get { return _context; } set { _context = (DraggableControlView)value; } }

            public virtual void OnMouseEnter()
            {
            }

            public virtual void OnMouseExit()
            {
            }

            public virtual void OnMouseDown()
            {
            }

            public virtual void OnMouseUp()
            {
            }
        }

        class UnselectedDraggableState : DraggableState
        {
            public override void Start()
            {
                base.Start();

                _context._renderer.material.color = _context.normalColor;
            }

            public override void OnMouseEnter()
            {
                Transition(_context._hoverState, 0);
            }
        }

        class HoverDraggableState : DraggableState
        {
            public override void Start()
            {
                base.Start();

                _context._renderer.material.color = _context.hoverColor;
            }

            public override void OnMouseExit()
            {
                Transition(_context._unselectedState, 0);
            }

            public override void OnMouseDown()
            {
                Transition(_context._draggingState, 0);
            }
        }

        class DraggingDraggableState : DraggableState
        {
            private Vector3 _posOld;

            private bool _isMouseOver = true;

            public override void Start()
            {
                base.Start();

                _context._renderer.material.color = _context.draggingColor;
                _posOld = ClosestPointOnLine(Camera.main.ScreenPointToRay(Input.mousePosition), _context.transform.position);
            }

            public override void OnMouseUp()
            {
                if (_isMouseOver)
                    Transition(_context._hoverState, 0);
                else
                    Transition(_context._unselectedState, 0);
            }

            public override void OnMouseEnter()
            {
                _isMouseOver = true;
            }

            public override void OnMouseExit()
            {
                _isMouseOver = false;
            }

            public override void Update()
            {
                base.Update();
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.green);

                Vector3 posNew = ClosestPointOnLine(ray, _context.transform.position);

                Debug.DrawLine(_context.transform.position, posNew, Color.blue);

                Vector3 delta = posNew - _posOld;

                if (delta.magnitude > 0)
                {
                    _context.draggedSignal.Dispatch(delta);
                    _posOld = posNew;
                }
            }

            public Vector3 ClosestPointOnLine(Ray ray, Vector3 point)
            {
                Vector3 p = ray.origin + ray.direction * Vector3.Dot(ray.direction, point - ray.origin);
                return p;
            }
        }

        private UnselectedDraggableState _unselectedState = new UnselectedDraggableState();

        private HoverDraggableState _hoverState = new HoverDraggableState();

        private DraggingDraggableState _draggingState = new DraggingDraggableState();

        private Renderer _renderer;

        public Color normalColor;

        public Color hoverColor;

        public Color draggingColor;

        public Signal<Vector3> draggedSignal = new Signal<Vector3>();
        
        protected override void Awake()
        {
            base.Awake();

            _renderer = GetComponent<Renderer>();
        }

        protected override void Start()
        {
            base.Start();

            SetState(_unselectedState);
        }

        void OnMouseEnter()
        {
            (ActiveState as DraggableState).OnMouseEnter();
        }

        void OnMouseExit()
        {
            (ActiveState as DraggableState).OnMouseExit();
        }

        void OnMouseDown()
        {
            (ActiveState as DraggableState).OnMouseDown();
        }

        void OnMouseUp()
        {
            (ActiveState as DraggableState).OnMouseUp();
        }
    }
}