using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct playerInfo {
	public int currentBoardPosition;
	public string identifier;
	
	public playerInfo(int _currentBoardPosition, string _identifier) {
		currentBoardPosition = _currentBoardPosition;
		identifier = _identifier;
	}
};

public class Runner : NetworkTool {
	static int currentPlayerNumber = 0;
	int flyingStep = 0;
	float interpolationTime = 0.0f;
	float secondTimer = 0.0f;
	playerInfo[] playerInfoList;
	int maxPlayer = 4;
	int numOfPlayer = 0;
	string latestIdentifier = "";
	
	// Use this for initialization
	void Start () {
		playerInfoList = new playerInfo[maxPlayer];
		connect();
	}
	
	public void SetPositionAtBoard(int number, int position) {
		playerInfoList[number].currentBoardPosition = position;
	}
	
	public int GetPositionAtBoard(int number) {
		return playerInfoList[number].currentBoardPosition;
	}
	
	public int GetCurrentPlayerNumber() {
		return currentPlayerNumber;
	}
	
	// Update is called once per frame
	void Update () {
		secondTimer += Time.deltaTime;
		if( secondTimer > 0.5f ) {
			GET( getURL(channelPort, "recv")+"?id="+ identifier +"&since="+rss );
			secondTimer = 0.0f;
		}
		
		string runnerName = "chiken_ccc__runner" + (currentPlayerNumber+1);
		
		if( name.Equals(runnerName) && playerInfoList[currentPlayerNumber].identifier == latestIdentifier) {
			int currentBoardNumber = playerInfoList[currentPlayerNumber].currentBoardPosition%12;
			if( positionValue == (playerInfoList[currentPlayerNumber].currentBoardPosition+1)%12+1 ) {
				GoNext();
				positionValue = -1;
			} else if(positionValue > 0 ) {
				GameObject.Find("resultBoard").GetComponent<resultBoard>().ChangeWrongAnswer(positionValue);
				GameObject.Find("mainCamera").GetComponent<CameraWork>().ChangeCamera(0);
				currentPlayerNumber = (currentPlayerNumber+1)%numOfPlayer;
				positionValue = -1;
			} else if(positionValue == 0 ) {
				GameObject.Find("resultBoard").GetComponent<resultBoard>().ChangeDefault();
			}
			GoNextByIntersection();
		}
	}
	
	void OnGUI() {
		for( int counter = 0; counter < numOfPlayer; counter++ ) {
			GUI.TextField(new Rect(0,30*counter,350,30), "player " + (counter+1) + playerInfoList[counter].identifier + " joined.");
		}
	}
	// Go to the next board...
	public void GoNext () {
		flyingStep = 1;
		//rotationValueByTime = 3.14f/12*(6-++currentBoardPosition%24);
		//transform.position = new Vector3(100*Mathf.Cos(rotationValueByTime), 0.0f, 100*Mathf.Sin(rotationValueByTime));		
		//transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 15);
		//GameObject.Find("resultBoard").GetComponent<resultBoard>().ChangeTexture(currentBoardPosition%12);
		
		//if(currentBoardPosition >= 24)
		//	currentBoardPosition = 0;
	}
	
	public void GoNextByIntersection() {
		if( flyingStep == 0 ) {
			return;
		} else if( flyingStep == 1 ) {
			if(++(playerInfoList[currentPlayerNumber].currentBoardPosition) >= 24) {
				playerInfoList[currentPlayerNumber].currentBoardPosition = 0;
			}
			GameObject.Find("resultBoard").GetComponent<resultBoard>().ChangeTexture(playerInfoList[currentPlayerNumber].currentBoardPosition%12+1);
			transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), 15);
			GameObject.Find("mainCamera").GetComponent<CameraWork>().ChangeCamera(currentPlayerNumber+1);
			interpolationTime = 0.0f;
			flyingStep++;
		} else if( flyingStep == 2 ) {
			interpolationTime += 2.0f*Time.deltaTime;
			float rotationValueByTime = 3.14f/12*(6-playerInfoList[currentPlayerNumber].currentBoardPosition%24);
			transform.position = Vector3.Slerp( transform.position, new Vector3(10*Mathf.Cos(rotationValueByTime), 0.0f, 10*Mathf.Sin(rotationValueByTime)), interpolationTime);
			transform.position = new Vector3( transform.position.x, Mathf.Sin(3.14f*interpolationTime), transform.position.z);
			
			// Camera Position Setting
			Vector3 cameraPosition = GameObject.Find("tracingCamera"+(currentPlayerNumber+1)).GetComponent<Camera>().transform.position;
			GameObject.Find("tracingCamera"+(currentPlayerNumber+1)).GetComponent<Camera>().transform.position = new Vector3( cameraPosition.x, 20, cameraPosition.z);
			if( interpolationTime > 1.0f ) {
				flyingStep = 0;
				interpolationTime = 0.0f;
			}
		}
			
	}
	
	public override IEnumerator WaitForRequest(WWW www) {
        yield return www;
		
        // check for errors
		Debug.Log("WWW Ok!: " + www.text);
		if (www.error == null) {
			var Reader = new LitJson.JsonReader(www.text);
			bool needPrintMessage = true;
			while(Reader.Read()) {
				string type = Reader.Value != null ? Reader.Value.GetType().ToString() : "";
				string valueString = type != "" ? Reader.Value.ToString() : "";
				
				// 1. check rss
				if( valueString.Equals("rss") ) {
					Reader.Read();
					type = Reader.Value != null ? Reader.Value.GetType().ToString() : "";
					valueString = type != "" ? Reader.Value.ToString() : "";
					if( rss < long.Parse(valueString) ){
						
						rss = long.Parse(valueString);
						Debug.Log("RSS: " + rss);
					}
				}
				
				// 2. check selected card number.
				if( valueString.Equals("value") ) {
					Reader.Read();
					type = Reader.Value != null ? Reader.Value.GetType().ToString() : "";
					valueString = type != "" ? Reader.Value.ToString() : "";
					positionValue = int.Parse(valueString);
				}
				
				// 3. check message
				if( valueString.Equals("messages") ) {
					Reader.Read();
					type = Reader.Value != null ? Reader.Value.GetType().ToString() : "";
					valueString = type != "" ? Reader.Value.ToString() : "";
					needPrintMessage = !valueString.Equals("");
				}
				
				// 4. check player
				if( valueString.Equals("player") ) {
					Reader.Read();
					type = Reader.Value != null ? Reader.Value.GetType().ToString() : "";
					valueString = type != "" ? Reader.Value.ToString() : "";
					latestIdentifier = valueString;
				}
				
				// 5. check key
				if( valueString.Equals("key") ) {
					Reader.Read();
					type = Reader.Value != null ? Reader.Value.GetType().ToString() : "";
					valueString = type != "" ? Reader.Value.ToString() : "";
					if( valueString.Equals("connect") && !latestIdentifier.Equals(identifier)) {
						if( numOfPlayer < maxPlayer ) {
							bool alreadyIn = false;
							foreach( playerInfo info in playerInfoList ) {
								if( info.identifier == latestIdentifier )
									alreadyIn = true;
							}
							if( !alreadyIn ) {
								playerInfoList[numOfPlayer++] = new playerInfo(-1, latestIdentifier);
								addPlayer();	
							}
						}
					}
				}
				
			}
			if( needPrintMessage ) {
				Debug.Log("WWW Ok!: " + www.text);
			}
        } else {
            Debug.Log("WWW Error: "+ www.error);
		}
	}
	
	private void addPlayer() {
		switch( numOfPlayer ) {
		case 1:
			playerInfoList[0].currentBoardPosition = 0;
			GameObject.Find("chiken_ccc__runner1").GetComponent<Runner>().transform.localPosition = new Vector3(0, 0, 0.5f);
			GameObject.Find("chiken_ccc__runner1").GetComponent<Runner>().transform.Rotate(0, 45, 0);
			break;
		case 2:
			playerInfoList[1].currentBoardPosition = 12;
			GameObject.Find("chiken_ccc__runner2").GetComponent<Runner>().transform.localPosition = new Vector3(0, 0, -0.5f);
			GameObject.Find("chiken_ccc__runner2").GetComponent<Runner>().transform.Rotate(0, 0, 0);
			break;
		case 3:
			playerInfoList[1].currentBoardPosition = 8;
			playerInfoList[2].currentBoardPosition = 16;
			GameObject.Find("chiken_ccc__runner2").GetComponent<Runner>().transform.localPosition = new Vector3(0.433f, 0, -0.25f);
			GameObject.Find("chiken_ccc__runner2").GetComponent<Runner>().transform.Rotate(0, 75, 0);
			GameObject.Find("chiken_ccc__runner3").GetComponent<Runner>().transform.localPosition = new Vector3(-0.433f, 0, -0.25f);
			GameObject.Find("chiken_ccc__runner3").GetComponent<Runner>().transform.Rotate(0, 15, 0);
			break;
		case 4:
			playerInfoList[1].currentBoardPosition = 6;
			playerInfoList[2].currentBoardPosition = 12;
			playerInfoList[3].currentBoardPosition = 18;
			GameObject.Find("chiken_ccc__runner2").GetComponent<Runner>().transform.localPosition = new Vector3(0.5f, 0, 0);
			GameObject.Find("chiken_ccc__runner2").GetComponent<Runner>().transform.Rotate(0, 82.5f, 0);
			GameObject.Find("chiken_ccc__runner3").GetComponent<Runner>().transform.localPosition = new Vector3(0, 0, -0.5f);
			GameObject.Find("chiken_ccc__runner3").GetComponent<Runner>().transform.Rotate(0, 75, 0);
			GameObject.Find("chiken_ccc__runner4").GetComponent<Runner>().transform.localPosition = new Vector3(-0.5f, 0, 0);
			GameObject.Find("chiken_ccc__runner4").GetComponent<Runner>().transform.Rotate(0, 22.5f, 0);
			break;
		}
	}
}
