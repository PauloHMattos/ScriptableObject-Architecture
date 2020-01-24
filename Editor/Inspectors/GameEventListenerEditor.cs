//using System;
//using System.Reflection;
//using UnityEditor;

//namespace ScriptableObjectArchitecture.Editor
//{
//    [CustomEditor(typeof(GameEventListener), true)]
//    public class GameEventListenerEditor : DebuggableGameEventListenerEditor
//    {
//        //protected MethodInfo _raiseMethod;

//        //protected override void OnEnable()
//        //{
//        //    base.OnEnable();
//        //    _raiseMethod = target.GetType().BaseType.GetMethod(nameof(IGameEventListener.OnEventRaised));
//        //}

//        //protected override void CallMethod(object value)
//        //{
//        //    _raiseMethod.Invoke(target, null);
//        //}

//        protected override object GetDebugValue(SerializedProperty property)
//        {
//            return null;
//        }
//    }

//    //public abstract class BaseGameEventListenerEditor : BaseListenerEditor
//    //{
//    //    private IStackTraceObject Target { get { return (IStackTraceObject)target; } }

//    //    private StackTrace _stackTrace;


//    //    protected override void OnEnable()
//    //    {
//    //        _stackTrace = new StackTrace(Target, true);
//    //        _stackTrace.OnRepaint.AddListener(Repaint);

//    //        base.OnEnable();
//    //    }


//    //    protected override void DrawRaiseButton()
//    //    {
//    //        base.DrawRaiseButton();
//    //        DrawStackTrace();
//    //    }

//    //    protected virtual void DrawStackTrace()
//    //    {
//    //        _stackTrace.Draw();
//    //    }
//    //} 
//}