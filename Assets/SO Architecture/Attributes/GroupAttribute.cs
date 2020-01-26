using System;
using UnityEditor;

[AttributeUsage(AttributeTargets.Field)]
public class GroupAttribute : Attribute
{
    public string header;
    public bool hidden;

    public GroupAttribute(string header, bool hidden = false)
    {
        this.header = header;
        this.hidden = hidden;
    }
}
