using System;

[AttributeUsage(AttributeTargets.Field)]
public class RequiredAttribute : Attribute
{
    public RequiredAttribute()
    {
    }
}
