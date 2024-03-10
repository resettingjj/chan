using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*[CustomEditor(typeof(CompositeBehavior))]
public class CompositeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CompositeBehavior cb = (CompositeBehavior)target;

        Rect r = EditorGUILayout.BeginHorizontal();
        r.height = EditorGUIUtility.singleLineHeight;
        if (cb.behaviors == null || cb.behaviors.Length == 0)
        {
            EditorGUILayout.HelpBox("No behaviors in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
            r = EditorGUILayout.BeginHorizontal();
            r.height = EditorGUIUtility.singleLineHeight;
        }
        else
        {
            r.x = 30f;
            r.width = EditorGUIUtility.currentViewWidth - 95f;
            EditorGUILayout.Space(30);
            EditorGUILayout.LabelField("Behaviors", GUILayout.Width(r.xMax-100));
            r.x = EditorGUIUtility.currentViewWidth - 65f;
            r.width = 60f;
            EditorGUILayout.LabelField("Weights");
            r.y += EditorGUIUtility.singleLineHeight * 1.2f;
            r.y += EditorGUIUtility.singleLineHeight * 1.5f; 
            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < cb.behaviors.Length; i++)
            {
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                r.x = 5f;
                r.width = 20f;
                EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(30));
                r.x = 30f;
                r.width = EditorGUIUtility.currentViewWidth - 95f;
                cb.behaviors[i] = (FlockBehaviourScript)EditorGUILayout.ObjectField(cb.behaviors[i], typeof(FlockBehaviourScript), false);
                r.x = EditorGUIUtility.currentViewWidth - 65f;
                r.width = 60f;
                cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i], GUILayout.Width(100));
                
                r.y += EditorGUIUtility.singleLineHeight * 1.1f;
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(cb);
            }
        }

        EditorGUILayout.EndHorizontal();
        r.x = 5f;
        r.width = EditorGUIUtility.currentViewWidth - 10f;
        r.y += EditorGUIUtility.singleLineHeight * 0.5f;
        if (GUILayout.Button("Add Behavior"))
        {
            AddBehavior(cb);
            EditorUtility.SetDirty(cb);
        }

        r.y += EditorGUIUtility.singleLineHeight * 1.5f;
        if (cb.behaviors != null && cb.behaviors.Length > 0)
        {
            if (GUILayout.Button("Remove Behavior"))
            {
                RemoveBehavior(cb);
                EditorUtility.SetDirty(cb);
            }
        }

    }

    void AddBehavior(CompositeBehavior cb)
    {
        int oldCount = (cb.behaviors != null) ? cb.behaviors.Length : 0;
        FlockBehaviourScript[] newBehaviors = new FlockBehaviourScript[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];
        for (int i = 0; i < oldCount; i++) 
        {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[oldCount] = 1f;
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;
    }

    void RemoveBehavior(CompositeBehavior cb)
    {
        int oldCount = cb.behaviors.Length;
        if (oldCount == 1)
        {
            cb.behaviors = null;
            cb.weights = null;
            return;
        }
        FlockBehaviourScript[] newBehaviors = new FlockBehaviourScript[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];
        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviors[i] = cb.behaviors[i];
            newWeights[i] = cb.weights[i];
        }
        cb.behaviors = newBehaviors;
        cb.weights = newWeights;

    }
}*/
