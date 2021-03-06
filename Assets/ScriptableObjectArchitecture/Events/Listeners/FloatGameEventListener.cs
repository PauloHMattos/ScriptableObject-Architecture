﻿using ScriptableObjectArchitecture.Events.Game_Events;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Listeners
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_LISTENER_SUBMENU + "float Event Listener")]
    public sealed class FloatGameEventListener : BaseGameEventListener<float, FloatGameEvent, FloatUnityEvent>
    {
    }
}