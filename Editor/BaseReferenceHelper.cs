using System.Reflection;
using Type = System.Type;

namespace Assets.ScriptableObjectArchitecture.Editor
{
    public static class BaseReferenceHelper
    {
        private const BindingFlags NON_PUBLIC_BINDINGS_FLAG = BindingFlags.Instance | BindingFlags.NonPublic;
        private const string CONSTANT_VALUE_NAME = "_constantValue";
    
        public static Type GetReferenceType(FieldInfo fieldInfo)
        {
            return fieldInfo.FieldType;
        }
        public static Type GetValueType(FieldInfo fieldInfo)
        {
            var referenceType = GetReferenceType(fieldInfo);
        
            if (referenceType.IsGenericType)
            {
                referenceType = referenceType.GetGenericArguments()[0];
            }
            else if(referenceType.IsArray)
            {
                referenceType = referenceType.GetElementType();
            }

            var constantValueField = referenceType.GetField(CONSTANT_VALUE_NAME, NON_PUBLIC_BINDINGS_FLAG);

            return constantValueField.FieldType;
        }
    }
}
