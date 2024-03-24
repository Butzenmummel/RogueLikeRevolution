using Scripts.Enums;

namespace Scenes.Attachments;

public partial class Summon : AttachmentBase
{
    public override void _Ready()
    {
        _type = AttachmentType.Summon;
    }
}