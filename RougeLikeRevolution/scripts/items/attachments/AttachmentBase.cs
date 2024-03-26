using System.Collections.Generic;
using Godot;
using Scripts.Enums;

namespace Scripts.Attachments;

public partial class AttachmentBase : Node2D
{
	protected Texture2D _icon;
	protected string _name;
	protected string _tooltip;
	protected List<string> _description;

	protected AttachmentType _type = AttachmentType.Base;
	protected Rarity _rarity;
	protected int _level = 0;
	protected int _maxLevel = 2;
	protected int _cost;

	public string WriteDescription()
	{
		string description = "";
		foreach (string line in _description)
		{
			description += line + "\n";
		}
		return description;
	}
}
