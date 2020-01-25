using System;

[AttributeUsage(AttributeTargets.Method)]
public class ButtonAttribute : Attribute
{
    public string Text;

    public ButtonAttribute(string buttonText = "")
    {
        Text = buttonText;
    }
}