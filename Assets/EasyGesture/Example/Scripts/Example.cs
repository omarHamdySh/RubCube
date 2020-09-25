using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

	void OnGUI()
	{
		if(GUI.Button(new Rect(10,10,200,50),"Drag Example"))
		{
			Application.LoadLevel(0);
		}
		GUI.Label(new Rect(10,70,250,100),"Swipe - Move Cube\n1 Tap - Change color to red\n2 Tap - Change color to green\n3 Tap - Change color to blue");
	}


	void OnEnable () {
		EasyGesture.onSwipe += OnSwipe;
		EasyGesture.onZoom += OnZoom;
		EasyGesture.onTap += OnTap;
	}
	void OnDisable () {
		EasyGesture.onSwipe -= OnSwipe;
		EasyGesture.onZoom -= OnZoom;
		EasyGesture.onTap -= OnTap;
	}


	void OnSwipe(EasyGesture.Gesture type, float speed)
	{
		switch(type)
		{
		case EasyGesture.Gesture.SWIPE_DOWN :
			transform.Translate(Vector3.down*speed*Time.deltaTime);
			break;

		case EasyGesture.Gesture.SWIPE_UP :
			transform.Translate(Vector3.up*speed*Time.deltaTime);
			break;

		case EasyGesture.Gesture.SWIPE_LEFT :
			transform.Translate(Vector3.left*speed*Time.deltaTime);
			break;

		case EasyGesture.Gesture.SWIPE_RIGHT :
			transform.Translate(Vector3.right*speed*Time.deltaTime);
			break;
		}
	}

	void OnZoom(EasyGesture.Gesture type, float value, float speed)
	{
		float val = Camera.main.fieldOfView;
		switch(type)
		{
		case EasyGesture.Gesture.ZOOM_IN :
			Camera.main.fieldOfView = Mathf.Clamp(val - value*speed*Time.deltaTime,10,60);
			break;
			
		case EasyGesture.Gesture.ZOOM_OUT :
			Camera.main.fieldOfView = Mathf.Clamp(val + value*speed*Time.deltaTime,10,60);
			break;
		}
	}

	void OnTap(int tapCount)
	{
		switch(tapCount)
		{
		case 1:GetComponent<Renderer>().material.color = Color.red;
			break;

		case 2:GetComponent<Renderer>().material.color = Color.green;
			break;

		case 3:GetComponent<Renderer>().material.color = Color.blue;
			break;

			// you can handle rest
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
