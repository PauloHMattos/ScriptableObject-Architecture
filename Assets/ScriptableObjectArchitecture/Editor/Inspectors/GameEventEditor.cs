﻿using ScriptableObjectArchitecture.Events.Game_Events;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor.Inspectors
{
    [CustomEditor(typeof(GameEventBase), true)]
    public sealed class GameEventEditor : BaseGameEventEditor
    {
        private GameEvent Target => (GameEvent)target;

        protected override void DrawRaiseButton()
        {
            if (GUILayout.Button("Raise"))
            {
                Target.Raise();
            }
        }
    } 
}