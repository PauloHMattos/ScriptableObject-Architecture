using System.Reflection;
using Type = System.Type;

namespace Assets.ScriptableObjectArchitecture.Editor
{
    public static class BaseReferenceHelper
    {
        private const BindingFlags NonPublicBindingsFlag = BindingFlags.Instance | BindingFlags.NonPublic;
        private const string ConstantValueName = "_constantValue";
    
        public static Type GetReferenceType(FieldInfo fieldInfo)
        {
            return fieldInfo.FieldType;
        }
        public static Type GetValueType(FieldInfo fieldInfo)
        {
            Type referenceType = GetReferenceType(fieldInfo);
        
            if (referenceType.IsGenericType)
            {
                referenceType = referenceType.GetGenericArguments()[0];
            }
            else if(referenceType.IsArray)
            {
                referenceType = referenceType.GetElementType();
            }

            FieldInfo constantValueField = referenceType.GetField(ConstantValueName, NonPublicBindingsFlag);

            return constantValueField.FieldType;
        }
    }
}
