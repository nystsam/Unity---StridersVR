using UnityEngine;
using System.Collections;

public class CustomMouse : MonoBehaviour {

	public Texture2D cursorTexture;

	private int cursorSizeX = 40;
	private int cursorSizeY = 40;

	void Start () 
	{
		Cursor.visible = false;
	}

	void OnGUI()
	{
		GUI.DrawTexture (new Rect(Event.current.mousePosition.x-cursorSizeX/10, Event.current.mousePosition.y-cursorSizeY/10, cursorSizeX, cursorSizeY), 
		                 cursorTexture);
	}
}
