using cpGames.common;

namespace OManip.game
{
    public class HUDControllerView : StateContextView
    {
        abstract class HUDState : State
        {
            protected HUDControllerView _context;

            public override IStateContext Context { get { return _context; } set { _context = (HUDControllerView)value; } }

            public virtual void ObjectClicked(TransformableObjectView tObject)
            {
                if (tObject != _context._tObject)
                {
                    _context._tObject = tObject;
                    Transition(_context._unselectedState, 0);
                }
            }
        }

        class UnselectedHUDState : HUDState
        {
            public override void Start()
            {
                base.Start();

                if (_context._tObject != null)
                    Transition(_context._toolbarState, 0);
            }

            public override void ObjectClicked(TransformableObjectView tObject)
            {
                if (tObject != _context._tObject && tObject != null)
                {
                    _context._tObject = tObject;
                    Transition(_context._toolbarState, 0);
                }
            }
        }

        class ToolbarHUDState : HUDState
        {
            public override void Start()
            {
                base.Start();
                _context.toolbarHUD.ShowHud(_context._tObject);
                _context.backHUD.ShowHud(_context._tObject);
            }

            public override void End()
            {
                _context.toolbarHUD.HideHud();
                _context.backHUD.HideHud();
                base.End();
            }
        }

        class TranslateHUDState : HUDState
        {
            public override void Start()
            {
                base.Start();
                _context.translateHUD.ShowHud(_context._tObject);
                _context.backHUD.ShowHud(_context._tObject);
            }

            public override void End()
            {
                _context.translateHUD.HideHud();
                _context.backHUD.HideHud();
                base.End();
            }
        }

        class ScaleHUDState : HUDState
        {
            public override void Start()
            {
                base.Start();
                _context.scaleHUD.ShowHud(_context._tObject);
                _context.backHUD.ShowHud(_context._tObject);
            }

            public override void End()
            {
                _context.scaleHUD.HideHud();
                _context.backHUD.HideHud();
                base.End();
            }
        }

        class RotateHUDState : HUDState
        {
            public override void Start()
            {
                base.Start();
                _context.rotateHUD.ShowHud(_context._tObject);
                _context.backHUD.ShowHud(_context._tObject);
            }

            public override void End()
            {
                _context.rotateHUD.HideHud();
                _context.backHUD.HideHud();
                base.End();
            }
        }

        private UnselectedHUDState _unselectedState = new UnselectedHUDState();

        private ToolbarHUDState _toolbarState = new ToolbarHUDState();

        private TranslateHUDState _translateState = new TranslateHUDState();

        private ScaleHUDState _scaleState = new ScaleHUDState();

        private RotateHUDState _rotateState = new RotateHUDState();

        private TransformableObjectView _tObject;

        [Inject]
        public ObjectClickedSignal objectClickedSignal { get; set; }

        [Inject]
        public ToolSelectedSignal toolSelectedSignal { get; set; }

        public HUDView toolbarHUD;

        public HUDView translateHUD;

        public HUDView rotateHUD;

        public HUDView scaleHUD;

        public HUDView backHUD;

        protected override void Start()
        {
            base.Start();

            SetState(_unselectedState);

            objectClickedSignal.AddListener(ObjectClicked);
            toolSelectedSignal.AddListener(ToolSelected);
        }

        protected override void OnDestroy()
        {
            objectClickedSignal.RemoveListener(ObjectClicked);
            toolSelectedSignal.RemoveListener(ToolSelected);
            base.OnDestroy();
        }

        void ObjectClicked(TransformableObjectView tObject)
        {
            (ActiveState as HUDState).ObjectClicked(tObject);
        }

        void ToolSelected(ToolModel tool)
        {
            if (tool == null)
            {
                _tObject = null;
                SetState(_unselectedState);
                return;
            }

            switch (tool.toolType)
            {
                case ToolTypeEnum.Translate:
                    SetState(_translateState);
                    break;
                case ToolTypeEnum.Scale:
                    SetState(_scaleState);
                    break;
                case ToolTypeEnum.Rotate:
                    SetState(_rotateState);
                    break;
            }
        }
    }
}