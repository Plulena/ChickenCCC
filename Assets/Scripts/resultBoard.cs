using UnityEngine;
using System.Collections;

public class resultBoard : MonoBehaviour {
	public AudioClip yeahSound;
	public AudioClip oopsSound;
	public AudioClip victorySound;
	int setWinner = -1;
	float quitTimer = 0.0f;
	
	// Use this for initialization
	void Start () {
		yeahSound = Resources.Load("Sounds/yeah") as AudioClip;
		oopsSound = Resources.Load("Sounds/oops") as AudioClip;
		victorySound = Resources.Load("Sounds/victory") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
		if( setWinner == -1 ) return;
		
		quitTimer += Time.deltaTime;
		if( quitTimer > 5.0f )
			Application.Quit();
	}
	
	// Correct Answer
	public void ChangeTexture(int number) {
		Texture selectedTexture = Resources.Load("chiken_ccc__board_"+number) as Texture;
		audio.PlayOneShot(yeahSound);
		renderer.material.mainTexture = selectedTexture;
	}
	
	// Wrong Answer
	public void ChangeWrongAnswer(int number) {
		Texture selectedTexture = Resources.Load("chiken_ccc__board_"+number) as Texture;
		audio.PlayOneShot(oopsSound);
		renderer.material.mainTexture = selectedTexture;
	}
	
	// Default Image. Same as initiation.
	public void ChangeDefault() {
		Texture defaultTexture = Resources.Load("main_chicken") as Texture;
		renderer.material.mainTexture = defaultTexture;
	}
	
	// Win the Game by currently playing member.
	public void WinTheGame(int playerNumber) {
		setWinner = playerNumber;
		audio.PlayOneShot(victorySound);
	}
	
	// See who is the winner!
	void OnGUI() {
		if( setWinner == -1 ) return;
		
		GUI.DrawTexture(new Rect( 0, 0, Screen.width, Screen.height), Resources.Load("winplayer" + (setWinner+1)) as Texture);
	}
}
