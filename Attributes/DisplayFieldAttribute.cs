using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class DisplayFieldAttribute : PropertyAttribute
{
    public string Label { get; }

    public DisplayFieldAttribute(string label)
    {
        Label = label;
    }

    public DisplayFieldAttribute() : this(null)
    {
    }
}