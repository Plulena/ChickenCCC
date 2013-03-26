using UnityEngine;
using System.Collections;

public class resultBoard : MonoBehaviour {
	public AudioClip yeahSound;
	public AudioClip oopsSound;
	int setWinner = -1;
	float quitTimer = 0.0f;
	
	// Use this for initialization
	void Start () {
		yeahSound = Resources.Load("Sounds/yeah") as AudioClip;
		oopsSound = Resources.Load("Sounds/oops") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
		if( setWinner == -1 ) return;
		
		quitTimer += Time.deltaTime;
		if( quitTimer > 5.0f )
			Application.Quit();
	}
	
	public void ChangeTexture(int number) {
		Texture selectedTexture = Resources.Load("chiken_ccc__board_"+number) as Texture;
		audio.PlayOneShot(yeahSound);
		renderer.material.mainTexture = selectedTexture;
	}
	
	public void ChangeWrongAnswer(int number) {
		Texture selectedTexture = Resources.Load("chiken_ccc__board_"+number) as Texture;
		audio.PlayOneShot(oopsSound);
		renderer.material.mainTexture = selectedTexture;
	}
	
	public void ChangeDefault() {
		Texture defaultTexture = Resources.Load("main_chicken") as Texture;
		renderer.material.mainTexture = defaultTexture;
	}
	
	public void WinTheGame(int playerNumber) {
		setWinner = playerNumber;
	}
	
	void OnGUI() {
		if( setWinner == -1 ) return;
		
		GUI.DrawTexture(new Rect( 0, 0, Screen.width, Screen.height), Resources.Load("winplayer" + (setWinner+1)) as Texture);
	}
}
