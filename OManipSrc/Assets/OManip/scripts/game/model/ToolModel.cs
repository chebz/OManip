public enum ToolTypeEnum
{
    Translate,
    Rotate,
    Scale
}

public class ToolModel
{
    public ToolTypeEnum toolType { get; set; }
}
