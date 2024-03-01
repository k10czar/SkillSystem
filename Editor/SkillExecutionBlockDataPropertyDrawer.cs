using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomPropertyDrawer( typeof(SkillExecutionBlockData) )]
public class SkillExecutionBlockDataPropertyDrawer : PropertyDrawer
{
    Dictionary<SerializedProperty,ReorderableList> _dictionary = new();
    Dictionary<string,float> _preCalcHeight = new();

    public override float GetPropertyHeight( SerializedProperty property, GUIContent label )
    {
        var conditionProp = property.FindPropertyRelative( "_condition" );
        var list = GetReorderableList( property.FindPropertyRelative( "_effects" ) );
        var path = conditionProp.propertyPath;
        var h = conditionProp.CalcSerializedReferenceHeight();
        _preCalcHeight[ path ] = h;
        return h + EditorGUIUtility.standardVerticalSpacing + ( list?.GetHeight() ?? 0 );
    }

    public float GetCachedHeight( SerializedProperty property )
    {
        if( property == null ) return 0;
        if( _preCalcHeight.TryGetValue( property.propertyPath, out var height ) ) return height;
        return 0;
    }

    public ReorderableList GetReorderableList( SerializedProperty property )
    {
        if( property == null ) return null;
        if( _dictionary.TryGetValue( property, out var list ) ) return list;

        list = new ReorderableList( property.serializedObject, property, true, false, true, true );

        list.elementHeightCallback += ( int index ) => {
            var element = property.GetArrayElementAtIndex( index );
            return element.CalcSerializedReferenceHeight();
        };

        list.drawElementCallback += ( Rect rect, int index, bool isActive, bool isFocused ) => {
            var element = property.GetArrayElementAtIndex( index );
            element.DrawSerializedReference( rect );
        };

        return list;
    }

    public override void OnGUI( Rect rect, SerializedProperty property, GUIContent label )
    {
        var conditionProp = property.FindPropertyRelative( "_condition" );
        var fxs = property.FindPropertyRelative( "_effects" );

        var conditionHeight = GetCachedHeight( conditionProp );
        conditionProp.DrawSerializedReference( rect.RequestTop( conditionHeight ) );
        rect = rect.CutTop( conditionHeight );
        
        var list = GetReorderableList( fxs );
        list.DoList( rect );
    }
}
