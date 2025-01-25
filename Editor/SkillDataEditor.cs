using System;
using System.Reflection;
using K10.EditorGUIExtention;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(SkillData), true)]
public class SkillDataEditor : Editor
{
    private SerializedProperty _eventsProp;

    protected virtual void OnEnable()
    {
        _eventsProp = serializedObject.FindProperty("events");
    }
        
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SeparationLine.Horizontal();
        GUILayout.Label( $"{_eventsProp.arraySize} Events", K10GuiStyles.bigBoldCenterStyle );
        SeparationLine.Horizontal();

        for (int i = 0; i < _eventsProp.arraySize; i++)
        {
            var evProp = _eventsProp.GetArrayElementAtIndex( i );
            DrawEvent( evProp );
        }
        serializedObject.ApplyModifiedProperties();

        // DrawDefaultInspector();
    }

    public void DrawEvent( SerializedProperty evProp )
    {
        var triggerProp = evProp.FindPropertyRelative( "_trigger" );
        
        EditorGUILayout.BeginVertical( EditorStyles.helpBox );
        // var boxColor = OverridingColorAttribute.TryGetColorFrom( triggerProp.managedReferenceValue, Colors.Gold );
        // GuiColorManager.New( boxColor );
        EditorGUILayout.BeginVertical( EditorStyles.helpBox );
        // GuiColorManager.Revert();
        triggerProp.DrawSerializedReferenceLayout();
        EditorGUILayout.EndVertical();
        
        var blocks = evProp.FindPropertyRelative( "_executionBlocks" );
        EditorGUILayout.PropertyField( blocks );
        
        // for( int i = 0; i < blocks.arraySize; i++ )
        // {
        //     var blockProp = blocks.GetArrayElementAtIndex( i );
        //     DrawBlock( blockProp );
        // }
        EditorGUILayout.EndVertical();
    }

    private void DrawBlock(SerializedProperty blockProp)
    {
        var conditionProp = blockProp.FindPropertyRelative( "_condition" );
        var fxs = blockProp.FindPropertyRelative( "_effects" );
        EditorGUILayout.PropertyField( blockProp );
    }
}