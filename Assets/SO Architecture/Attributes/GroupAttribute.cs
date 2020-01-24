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

[AttributeUsage(AttributeTargets.Class)]
public class HelpBoxAttribute : Attribute
{
    public string Message;
    public HelpBoxType Type;

    public HelpBoxAttribute(string message, HelpBoxType type)
    {
        Message = message;
        Type = type;
    }
}

public enum HelpBoxType
{
    //
    // Resumo:
    //     Neutral message.
    None = 0,
    //
    // Resumo:
    //     Info message.
    Info = 1,
    //
    // Resumo:
    //     Warning message.
    Warning = 2,
    //
    // Resumo:
    //     Error message.
    Error = 3
}

/*
[AttributeUsage(AttributeTargets.Field)]
public class DrawAttribute : Attribute
{
    public string header;

    public GroupAttribute(string header)
    {
        this.header = header;
        /*
         * _reorderableList = new ReorderableList(
                serializedObject,
                CollectionItemsProperty,
                ELEMENT_DRAGGABLE,
                LIST_DISPLAY_HEADER,
                LIST_DISPLAY_ADD_BUTTON,
                LIST_DISPLAY_REMOVE_BUTTON)
            {
                drawHeaderCallback = DrawHeader,
                drawElementCallback = DrawElement,
            };
         //*
    }
}
//*/