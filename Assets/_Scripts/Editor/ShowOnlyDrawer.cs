using System;
using UnityEditor;
using UnityEngine;
 
[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        string valueStr;
        switch (prop.propertyType)
        {
            case SerializedPropertyType.Integer:
                valueStr = prop.intValue.ToString();
                break;
            case SerializedPropertyType.Boolean:
                valueStr = prop.boolValue.ToString();
                break;
            case SerializedPropertyType.Float:
                valueStr = prop.floatValue.ToString("0.00000");
                break;
            case SerializedPropertyType.String:
                valueStr = prop.stringValue;
                break;
            case SerializedPropertyType.Enum:
                valueStr = prop.enumNames[prop.enumValueIndex];
                break;
            case SerializedPropertyType.ObjectReference:
                try {
                    valueStr = prop.objectReferenceValue.ToString();
                } catch (NullReferenceException) {
                    valueStr = $"None ({prop.type})";
                }
                break;
            case SerializedPropertyType.Color:
                valueStr = prop.colorValue.ToString();
                break;
            case SerializedPropertyType.Vector2:
                valueStr = prop.vector2Value.ToString();
                break;
            case SerializedPropertyType.Vector3:
                valueStr = prop.vector3Value.ToString();
                break;
            case SerializedPropertyType.Vector4:
                valueStr = prop.vector4Value.ToString();
                break;
            case SerializedPropertyType.Rect:
                valueStr = prop.rectValue.ToString();
                break;
            case SerializedPropertyType.Vector2Int:
                valueStr = prop.vector2IntValue.ToString();
                break;
            case SerializedPropertyType.Vector3Int:
                valueStr = prop.vector3IntValue.ToString();
                break;
            case SerializedPropertyType.RectInt:
                valueStr = prop.rectIntValue.ToString();
                break;
            case SerializedPropertyType.BoundsInt:
                valueStr = prop.boundsIntValue.ToString();
                break;
            case SerializedPropertyType.Bounds:
                valueStr = prop.boundsValue.ToString();
                break;
            case SerializedPropertyType.AnimationCurve:
                valueStr = prop.animationCurveValue.ToString();
                break;
            case SerializedPropertyType.Quaternion:
                valueStr = prop.quaternionValue.ToString();
                break;
            case SerializedPropertyType.ExposedReference:
                valueStr = prop.exposedReferenceValue.ToString();
                break;
            case SerializedPropertyType.ManagedReference:
                valueStr = prop.managedReferenceValue.ToString();
                break;
            case SerializedPropertyType.Hash128:
                valueStr = prop.hash128Value.ToString();
                break;
            case SerializedPropertyType.Generic:
            case SerializedPropertyType.FixedBufferSize:
            case SerializedPropertyType.Character:
            case SerializedPropertyType.LayerMask:
            case SerializedPropertyType.ArraySize:
            case SerializedPropertyType.Gradient:
            default:
                valueStr = "(not supported)";
                break;
        }
        
        EditorGUI.LabelField(position,label.text, valueStr);
    }
}