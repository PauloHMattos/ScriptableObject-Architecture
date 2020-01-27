using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.References
{
	[System.Serializable]
	public sealed class Color32Reference : BaseReference<Color32, Color32Variable>
	{
	    public Color32Reference() : base() { }
	    public Color32Reference(Color32 value) : base(value) { }
	}
}