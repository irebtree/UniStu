using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(XAIBehaviours))]
public class XAIBehavioursEditor : Editor {

	XAIBehaviours fsm = null;

	GUIContent content = new GUIContent();
	GUIStyle style = new GUIStyle();

	string[] statesName;
	void OnEnable(){
		fsm = serializedObject.targetObject as XAIBehaviours;
		statesName = ComponentInfoHelper.GetStatesTypeName();
		InitStates();
	}
	int popupIndex = 0;
	public override void OnInspectorGUI ()
	{
		XBaseState[] states = fsm.GetAllStates();
	//	Debug.Log("====  " + states[0].name);
		//DrawDefaultInspector();
		serializedObject.Update();
		string updateName;
		GUILayout.Space(10);
		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUILayout.BeginHorizontal();
		updateName = GUILayout.TextField( states[0].name, GUILayout.MaxWidth(90));

		popupIndex = EditorGUILayout.Popup(popupIndex,statesName, GUILayout.MaxWidth(90));
		updateName = statesName[popupIndex];
		UpdateStateName(updateName, states[0]);
		//Debug.Log(statesName[popupIndex]);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();

		GUILayout.Space(10);
		EditorGUILayout.BeginVertical(GUI.skin.box);
		GUI.backgroundColor = Color.green;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("health"));
		content.text = "MaxHealth";
		EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHealth"), content);
		EditorGUILayout.EndVertical();
		GUILayout.Space(10);

		GUI.backgroundColor = GUI.color;
		//GUI.contentColor = Color.red;
		EditorGUILayout.BeginVertical(GUI.skin.box);
		GUILayout.Label("Field of view Setting");
		EditorGUILayout.PropertyField(serializedObject.FindProperty("viewAngle"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("viewRadius"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("mashResolution"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("viewMeshFilter"));
		EditorGUILayout.EndVertical();
		GUILayout.Space(10);

		serializedObject.ApplyModifiedProperties();
	}

	void UpdateStateName(string name, XBaseState s)
	{
		s.name = name;
		serializedObject.ApplyModifiedProperties();
	}

	void InitStates()
	{
		XBaseState idleState = new XIdleState();
		idleState.name = "Idle";

		List<XBaseState> stateList = new List<XBaseState>();
		stateList.Add(idleState);
		fsm.ReplaceAllStates(stateList.ToArray());
	}
}
