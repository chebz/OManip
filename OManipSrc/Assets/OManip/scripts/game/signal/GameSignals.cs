using strange.extensions.signal.impl;

namespace OManip.game
{
    public class ObjectClickedSignal : Signal<TransformableObjectView> { }

    public class ToolSelectedSignal : Signal<ToolModel> { }
}