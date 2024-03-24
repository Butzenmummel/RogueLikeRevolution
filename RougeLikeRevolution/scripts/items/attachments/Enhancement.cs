using Scripts.Enums;

namespace Scipts.Attachments;

public partial class Enhancement : AttachmentBase
{
    public override void _Ready()
    {
        _type = AttachmentType.Enhancement;
    }
}