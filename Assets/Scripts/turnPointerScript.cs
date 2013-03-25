using UnityEngine;
using System.Collections;

public class turnPointerScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int currentPlayerNumber = GameObject.Find("chiken_ccc__runner1").GetComponent<Runner>().GetCurrentPlayerNumber();
		string currentRunner = "chiken_ccc__runner"+(currentPlayerNumber+1);
		Vector3 runnerPosition = GameObject.Find(currentRunner).GetComponent<Runner>().transform.position;
		transform.position = new Vector3(runnerPosition.x, 5+Mathf.Sin(Time.realtimeSinceStartup), runnerPosition.z);
	}
}
