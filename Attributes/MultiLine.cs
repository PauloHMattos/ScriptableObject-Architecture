using System;

[AttributeUsage(AttributeTargets.Class)]
public class MultiLine : Attribute
{

}


[AttributeUsage(AttributeTargets.Field)]
public class GroupAttribute : Attribute
{
    public string header;

    public GroupAttribute(string header)
    {
        this.header = header;
    }
}
