using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.References
{
	[System.Serializable]
	public sealed class ColorReference : BaseReference<Color, ColorVariable>
	{
	    public ColorReference() : base() { }
	    public ColorReference(Color value) : base(value) { }
	}
}