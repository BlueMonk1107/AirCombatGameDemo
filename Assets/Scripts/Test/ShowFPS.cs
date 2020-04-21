using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour {

#if UNITY_EDITOR

	private GUIStyle _guiStyle;

	private void Start()
	{
		_guiStyle = new GUIStyle();
		_guiStyle.fontSize = 20;
		_guiStyle.normal.textColor = Color.green;
	}

	public void OnGUI()
	{
		
		GUI.Label(new Rect(0,0,200,100),(1/Time.deltaTime).ToString(),_guiStyle);
	}
#endif
	
}
