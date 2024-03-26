using Scripts.Enums;

namespace Scripts.Attachments;

public partial class Summon : AttachmentBase
{
    public override void _Ready()
    {
        _type = AttachmentType.Summon;
    }
}