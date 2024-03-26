using Scripts.Enums;

namespace Scripts.Attachments;

public partial class Enhancement : AttachmentBase
{
    public override void _Ready()
    {
        _type = AttachmentType.Enhancement;
    }
}