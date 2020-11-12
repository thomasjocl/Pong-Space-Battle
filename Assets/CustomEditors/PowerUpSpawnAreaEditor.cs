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
        SerializedProperty m_maxPowersOnScreen;
        List<bool> m_powerUpsFold;

        string[] PropertiesToExclude = new string[] { "powerUps", "maxPowersOnScreen" };

        private void OnEnable()
        {
            m_powerUpsFold = new List<bool>();
            m_powerUps = serializedObject.FindProperty("powerUps");
            m_maxPowersOnScreen = serializedObject.FindProperty("maxPowersOnScreen");
            lastCount = m_powerUps.arraySize;
        }

        public override void OnInspectorGUI()
        { 
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, PropertiesToExclude);

            EditorGUILayout.PropertyField(m_maxPowersOnScreen, new GUIContent("Max PU on Screen"));

            EditorGUILayout.PropertyField(m_powerUps.FindPropertyRelative("Array.size"), new GUIContent("N° PowerUps"));

            if (lastCount < m_powerUps.arraySize)
            {
                Debug.Log("nulos");
                for (int e = 0; e < (m_powerUps.arraySize - lastCount); e++)
                {
                    m_powerUps.GetArrayElementAtIndex(lastCount + e).FindPropertyRelative("Prefab").objectReferenceValue = null;
                    m_powerUps.GetArrayElementAtIndex(lastCount + e).FindPropertyRelative("Probability").floatValue = 0f;
                }
            }

            lastCount = m_powerUps.arraySize;

            float total = 0f;

            for (int i = 0; i < m_powerUps.arraySize; i++)
            {
                total += m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("Probability").floatValue;
            }

            total = (total == 0) ? 100 : total;

            for (int i = 0; i < m_powerUps.arraySize; i++)
            {
                if (m_powerUpsFold.Count < m_powerUps.arraySize)
                {
                    m_powerUpsFold.AddRange(Enumerable.Repeat(true, (m_powerUps.arraySize - m_powerUpsFold.Count)));
                }
                else
                    m_powerUpsFold.RemoveRange(m_powerUps.arraySize, (m_powerUpsFold.Count - m_powerUps.arraySize));

                m_powerUpsFold[i] = EditorGUILayout.Foldout(m_powerUpsFold[i], "PowerUp " + (i + 1).ToString());

                m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("RealProbability").floatValue =
                    m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("Probability").floatValue / total * 100;

                if (m_powerUpsFold[i])
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("Prefab"));
                    EditorGUILayout.PropertyField(m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("CurrentOnScreen"), new GUIContent("Current"));
                    EditorGUILayout.PropertyField(m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("MaxCurrentOnScreen"), new GUIContent("Max allowed"));

                    if (m_powerUps.arraySize == 1)
                    {
                        m_powerUps.GetArrayElementAtIndex(0).FindPropertyRelative("Probability").floatValue = 100f;
                        GUI.enabled = false;
                        EditorGUILayout.Slider(m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("Probability"), 0f, 100f);
                        GUI.enabled = true;
                    }
                    else
                    {
                        EditorGUILayout.Slider(m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("Probability"), 0f, 100f);
                    }

                    EditorGUILayout.LabelField("Real Probability : " +
                        m_powerUps.GetArrayElementAtIndex(i).FindPropertyRelative("RealProbability").floatValue.ToString("0.00") + "%");
                    EditorGUI.indentLevel--;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
