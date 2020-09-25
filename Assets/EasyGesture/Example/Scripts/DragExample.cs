using UnityEngine;
using System.Collections;

public class DragExample : MonoBehaviour {


	void OnGUI()
	{
		if(GUI.Button(new Rect(10,10,200,50),"Example"))
		{
			Application.LoadLevel(1);
		}
		GUI.Label(new Rect(10,70,250,50),"Drag your finger on screen to move cube.");

	}

	void OnEnable () {
		EasyGesture.onDrag += OnDrag;
	}
	void OnDisable () {
		EasyGesture.onDrag -= OnDrag;
	}


	void OnDrag(EasyGesture.Gesture type, float speed)
	{
		switch(type)
		{
		case EasyGesture.Gesture.DRAG_DOWN :
			transform.Translate(Vector3.down*speed*Time.deltaTime);
			break;
			
		case EasyGesture.Gesture.DRAG_UP :
			transform.Translate(Vector3.up*speed*Time.deltaTime);
			break;
			
		case EasyGesture.Gesture.DRAG_LEFT :
			transform.Translate(Vector3.left*speed*Time.deltaTime);
			break;
			
		case EasyGesture.Gesture.DRAG_RIGHT :
			transform.Translate(Vector3.right*speed*Time.deltaTime);
			break;
			
		}
	}
}
