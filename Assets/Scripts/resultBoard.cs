using UnityEngine;
using System.Collections;

public class resultBoard : MonoBehaviour {
	public AudioClip yeahSound;
	public AudioClip oopsSound;
	
	// Use this for initialization
	void Start () {
		yeahSound = Resources.Load("Sounds/yeah") as AudioClip;
		oopsSound = Resources.Load("Sounds/oops") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
