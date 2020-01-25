using System;

[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : Attribute
{
    public ReadOnlyAttribute()
    {
    }
}