using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.CustomEditors
{
    [CustomEditor(typeof(PowerUpSpawnArea))]
    [CanEditMultipleObjects]
    public class PowerUpSpawnAreaEditor : Editor
    {
        int lastCount;
        //int m_powerUpListSize;
        SerializedProperty m_powerUps;
        List<bool> m_PowerUpsFold;

        private void OnEnable()
        {
            m_powerUps = serializedObject.FindProperty("powerUps");  
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_powerUps.FindPropertyRelative("Array.size"), new GUIContent("N° PowerUps"));

            if (lastCount < m_powerUps.arraySize)
            {
                for (int e = 0; e < (m_powerUps.arraySize - lastCount); e++)
                {
                    m_powerUps.GetArrayElementAtIndex(lastCount + e).FindPropertyRelative("Prefab").objectReferenceValue = null;
                    m_powerUps.GetArrayElementAtIndex(lastCount + e).FindPropertyRelative("Probability").floatValue = 0f;
                }
            }

            lastCount = m_powerUps.arraySize;

            for (int i = 0; i < m_powerUps.arraySize; i++)
            {
                if (m_PowerUpsFold.Count < m_powerUps.arraySize)
                {
                    m_PowerUpsFold.AddRange(Enumerable.Repeat(true, (m_powerUps.arraySize - m_PowerUpsFold.Count)));
                }
                else
                    m_PowerUpsFold.RemoveRange(m_powerUps.arraySize, (m_PowerUpsFold.Count - m_powerUps.arraySize));

                m_PowerUpsFold[i] = EditorGUILayout.Foldout(m_PowerUpsFold[i], "PowerUp " + (i + 1).ToString());

                if (m_PowerUpsFold[i])
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("Prefab"));
                    EditorGUILayout.Slider(m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("Probability"), 0f, 20f);
                    EditorGUI.indentLevel--;
                }
            } 

            serializedObject.ApplyModifiedProperties();
        }
    }
}
