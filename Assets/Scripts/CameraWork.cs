using UnityEngine;
using System.Collections;

// Camera can be identified as "identifier"



struct CameraInfo {
	public int identifier;
	public Vector3 position;		// where it is located.
	public Quaternion rotation;		// how much rotated from axis.
	public float orthographicSize;	// how far from runner.
};

// Camera for tracing. 
// It traces players' position.
public class CameraWork : MonoBehaviour {
	float interpolationTimer = 1.0f;
	CameraInfo fromCamera, toCamera;
	
	// Use this for initialization
	void Start () {
		
		// 1. off all cameras excepts mainCamera.
		GameObject.Find("backCamera").GetComponent<Camera>().enabled = false;
		GameObject.Find("tracingCamera1").GetComponent<Camera>().enabled = false;
		GameObject.Find("tracingCamera2").GetComponent<Camera>().enabled = false;
		GameObject.Find("tracingCamera3").GetComponent<Camera>().enabled = false;
		GameObject.Find("tracingCamera4").GetComponent<Camera>().enabled = false;
		GameObject.Find("mainCamera").GetComponent<Camera>().enabled = true;
		
		
		// 2. set fromCamera, toCamera as backCamera's Information.
	 	fromCamera = new CameraInfo();
		toCamera = new CameraInfo();
		fromCamera.identifier = toCamera.identifier = 0;
		fromCamera.position = toCamera.position = GameObject.Find("backCamera").GetComponent<Camera>().transform.position;
		fromCamera.rotation = toCamera.rotation = GameObject.Find("backCamera").GetComponent<Camera>().transform.rotation;
		fromCamera.orthographicSize = toCamera.orthographicSize = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
		// If we need to trace another thing,
		if( interpolationTimer < 1.0f ) {
			if( toCamera.identifier != 0 )
				interpolationTimer += Time.deltaTime;
			else
				interpolationTimer += Time.deltaTime;
			
			// interpolates three information(orthographic size, position and rotation).
			camera.orthographicSize = Mathf.Lerp(fromCamera.orthographicSize, toCamera.orthographicSize, interpolationTimer);
			transform.position = Vector3.Lerp(fromCamera.position, toCamera.position, interpolationTimer);
			transform.rotation = Quaternion.Slerp(fromCamera.rotation, toCamera.rotation, interpolationTimer);
			
		} else {
			// else trace toCamera's position.
			interpolationTimer = 1.0f;
			camera.orthographicSize = Mathf.Lerp(fromCamera.orthographicSize, toCamera.orthographicSize, interpolationTimer);
			switch(toCamera.identifier) {
				case 1:
					transform.position = GameObject.Find("tracingCamera1").GetComponent<Camera>().transform.position;
					transform.rotation = GameObject.Find("tracingCamera1").GetComponent<Camera>().transform.rotation; 
					break;
				case 2:
					transform.position = GameObject.Find("tracingCamera2").GetComponent<Camera>().transform.position;
					transform.rotation = GameObject.Find("tracingCamera2").GetComponent<Camera>().transform.rotation; 
					break;
				case 3:
					transform.position = GameObject.Find("tracingCamera3").GetComponent<Camera>().transform.position;
					transform.rotation = GameObject.Find("tracingCamera3").GetComponent<Camera>().transform.rotation; 
					break;
				case 4:
					transform.position = GameObject.Find("tracingCamera4").GetComponent<Camera>().transform.position;
					transform.rotation = GameObject.Find("tracingCamera4").GetComponent<Camera>().transform.rotation; 
					break;
				case 0:
					transform.position = GameObject.Find("backCamera").GetComponent<Camera>().transform.position;
					transform.rotation = GameObject.Find("backCamera").GetComponent<Camera>().transform.rotation; 
					break;
			}
		}
	}
	
	public void ChangeCamera(int toCameraIdentifier) {
		// this statement means "same camera". So, we don't need to replace Cameras.
		if( toCamera.identifier == toCameraIdentifier ) return;
		
		
		// fromCamera also means toCamera's previous information.
		// 1. Set fromCamera information.
		fromCamera.identifier = toCamera.identifier;
		fromCamera.orthographicSize = toCamera.orthographicSize;
		fromCamera.position = transform.position;
		fromCamera.rotation = transform.rotation;
		
		// 2. Set toCamera information by current player's position.
		toCamera.identifier = toCameraIdentifier;
		int currentPlayerNumber = GameObject.Find("chiken_ccc__runner1").GetComponent<Runner>().GetCurrentPlayerNumber();
		int currentPlayerPosition = GameObject.Find("chiken_ccc__runner1").GetComponent<Runner>().GetPositionAtBoard(currentPlayerNumber);
		float rotationValueByTime = 3.14f/12*(6-(currentPlayerPosition+1)%24);
		Vector3 position = new Vector3(10*Mathf.Cos(rotationValueByTime), 0.0f, 10*Mathf.Sin(rotationValueByTime));
		switch(toCameraIdentifier) {
			case 1:
				toCamera.orthographicSize = 4;
				toCamera.position = position;
				toCamera.rotation = GameObject.Find("tracingCamera1").GetComponent<Camera>().transform.rotation;
				
				break;
			case 2:
				toCamera.orthographicSize = 4;
				toCamera.position = position;
				toCamera.rotation = GameObject.Find("tracingCamera2").GetComponent<Camera>().transform.rotation;
				break;
			case 3:
				toCamera.orthographicSize = 4;
				toCamera.position = position;
				toCamera.rotation = GameObject.Find("tracingCamera3").GetComponent<Camera>().transform.rotation;
				break;
			case 4:
				toCamera.orthographicSize = 4;
				toCamera.position = position;
				toCamera.rotation = GameObject.Find("tracingCamera4").GetComponent<Camera>().transform.rotation;
				break;
			case 0:
				toCamera.orthographicSize = 10;
				toCamera.position = GameObject.Find("backCamera").GetComponent<Camera>().transform.position;
				toCamera.rotation = GameObject.Find("backCamera").GetComponent<Camera>().transform.rotation;
				interpolationTimer = 0.0f;
				break;
		}
	}
	
	// See next image to go.
	void OnGUI() {
		if( GameObject.Find("chiken_ccc__runner1").GetComponent<Runner>().GetNumOfPlayer() == 0 ) return;
		int currentPlayerNumber = GameObject.Find("chiken_ccc__runner1").GetComponent<Runner>().GetCurrentPlayerNumber();
		int currentPlayerPosition = GameObject.Find("chiken_ccc__runner1").GetComponent<Runner>().GetPositionAtBoard(currentPlayerNumber);
		int number = (currentPlayerPosition+1)%12+1;
		GUI.DrawTexture(new Rect( 0.83f*Screen.width, 0.76f*Screen.height, 0.16f*Screen.width, 0.24f*Screen.height), Resources.Load("chiken_ccc__board_"+number) as Texture);
		GUI.DrawTexture(new Rect( 0.83f*Screen.width, 0.72f*Screen.height, 0.16f*Screen.width, 0.04f*Screen.height), Resources.Load("next") as Texture);
	}
}
