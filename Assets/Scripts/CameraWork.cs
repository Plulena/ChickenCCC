using UnityEngine;
using System.Collections;

struct CameraInfo {
	public int identifier;
	public Vector3 position;
	public Quaternion rotation;
	public float orthographicSize;
};

public class CameraWork : MonoBehaviour {
	float interpolationTimer = 1.0f;
	CameraInfo fromCamera, toCamera;
	//Quaternion prevRotation, nextRotation;
	//Vector3 prevPosition, nextPosition;
	//float prevSize, nextSize;
	
	// Use this for initialization
	void Start () {
		GameObject.Find("backCamera").GetComponent<Camera>().enabled = false;
		GameObject.Find("tracingCamera1").GetComponent<Camera>().enabled = false;
		GameObject.Find("tracingCamera2").GetComponent<Camera>().enabled = false;
		GameObject.Find("tracingCamera3").GetComponent<Camera>().enabled = false;
		GameObject.Find("tracingCamera4").GetComponent<Camera>().enabled = false;
		GameObject.Find("mainCamera").GetComponent<Camera>().enabled = true;
		
		
	 	fromCamera = new CameraInfo();
		toCamera = new CameraInfo();
		fromCamera.identifier = toCamera.identifier = 0;
		fromCamera.position = toCamera.position = GameObject.Find("backCamera").GetComponent<Camera>().transform.position;
		fromCamera.rotation = toCamera.rotation = GameObject.Find("backCamera").GetComponent<Camera>().transform.rotation;
		fromCamera.orthographicSize = toCamera.orthographicSize = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if( interpolationTimer < 1.0f ) {
			if( toCamera.identifier != 0 )
				interpolationTimer += Time.deltaTime;
			else
				interpolationTimer += Time.deltaTime;
			
			camera.orthographicSize = Mathf.Lerp(fromCamera.orthographicSize, toCamera.orthographicSize, interpolationTimer);
			transform.position = Vector3.Lerp(fromCamera.position, toCamera.position, interpolationTimer);
			transform.rotation = Quaternion.Slerp(fromCamera.rotation, toCamera.rotation, interpolationTimer);
			
		} else {
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
		if( toCamera.identifier == toCameraIdentifier ) return;
		
		
		fromCamera.identifier = toCamera.identifier;
		fromCamera.orthographicSize = toCamera.orthographicSize;
		fromCamera.position = transform.position;
		fromCamera.rotation = transform.rotation;
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
				toCamera.rotation = GameObject.Find("tracingCamera5").GetComponent<Camera>().transform.rotation;
				break;
			case 0:
				toCamera.orthographicSize = 10;
				toCamera.position = GameObject.Find("backCamera").GetComponent<Camera>().transform.position;
				toCamera.rotation = GameObject.Find("backCamera").GetComponent<Camera>().transform.rotation;
				interpolationTimer = 0.0f;
				break;
		}
	}
	
	/*void OnGUI() {
		if( GUI.Button(new Rect(0, 0, 100, 100), "" + cameraNumber)) {
			cameraNumber = (cameraNumber+1)%5;
			interpolationTimer = 0.0f;
			switch(cameraNumber) {
				case 1:
					prevPosition = GameObject.Find("backCamera").GetComponent<Camera>().transform.position;
					nextPosition = GameObject.Find("tracingCamera1").GetComponent<Camera>().transform.position;
					prevRotation = GameObject.Find("backCamera").GetComponent<Camera>().transform.rotation;
					nextRotation = GameObject.Find("tracingCamera1").GetComponent<Camera>().transform.rotation;
					prevSize = 12;
					nextSize = 3;
					break;
				case 2:
					prevPosition = GameObject.Find("tracingCamera1").GetComponent<Camera>().transform.position;
					nextPosition = GameObject.Find("tracingCamera2").GetComponent<Camera>().transform.position;
					prevRotation = GameObject.Find("tracingCamera1").GetComponent<Camera>().transform.rotation;
					nextRotation = GameObject.Find("tracingCamera2").GetComponent<Camera>().transform.rotation;
					prevSize = 3;
					break;
				case 3:
					prevPosition = GameObject.Find("tracingCamera2").GetComponent<Camera>().transform.position;
					nextPosition = GameObject.Find("tracingCamera3").GetComponent<Camera>().transform.position;
					prevRotation = GameObject.Find("tracingCamera2").GetComponent<Camera>().transform.rotation;
					nextRotation = GameObject.Find("tracingCamera3").GetComponent<Camera>().transform.rotation;
					break;
				case 4:
					prevPosition = GameObject.Find("tracingCamera3").GetComponent<Camera>().transform.position;
					nextPosition = GameObject.Find("tracingCamera4").GetComponent<Camera>().transform.position;
					prevRotation = GameObject.Find("tracingCamera3").GetComponent<Camera>().transform.rotation;
					nextRotation = GameObject.Find("tracingCamera4").GetComponent<Camera>().transform.rotation;
					break;
				case 0:
					prevPosition = GameObject.Find("tracingCamera4").GetComponent<Camera>().transform.position;
					nextPosition = GameObject.Find("backCamera").GetComponent<Camera>().transform.position;
					prevRotation = GameObject.Find("tracingCamera4").GetComponent<Camera>().transform.rotation;
					nextRotation = GameObject.Find("backCamera").GetComponent<Camera>().transform.rotation;
					prevSize = 3;
					nextSize = 12;
					break;
			}
		}
	}*/
}
