using UnityEngine;
using System.Collections;

public class PhoneUI : NetworkTool {
	
	int buttonNumber = 0;
	
	// Use this for initialization
	void Start () {
		connect();
	}
	
	void OnGUI() {
		
		if( GUI.Button(new Rect( 0.0f*Screen.width+0,  0.0f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_1") as Texture)) {
			buttonNumber = 1;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.25f*Screen.width+0, 0.0f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_2") as Texture)) {
			buttonNumber = 2;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.5f*Screen.width+0, 0.0f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_3") as Texture)) {
			buttonNumber = 3;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.75f*Screen.width+0, 0.0f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_4") as Texture)) {
			buttonNumber = 4;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.0f*Screen.width+0, 0.33f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_5") as Texture)) {
			buttonNumber = 5;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.25f*Screen.width+0, 0.33f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_6") as Texture)) {
			buttonNumber = 6;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.5f*Screen.width+0, 0.33f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_7") as Texture)) {
			buttonNumber = 7;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.75f*Screen.width+0, 0.33f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_8") as Texture)) {
			buttonNumber = 8;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.0f*Screen.width+0, 0.66f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_9") as Texture)) {
			buttonNumber = 9;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.25f*Screen.width+0, 0.66f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_10") as Texture)) {
			buttonNumber = 10;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.5f*Screen.width+0, 0.66f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_11") as Texture)) {
			buttonNumber = 11;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		} else if( GUI.Button(new Rect(0.75f*Screen.width+0, 0.66f*Screen.height+0, 0.25f*Screen.width, 0.33f*Screen.height), Resources.Load("chiken_ccc__board_12") as Texture)) {
			buttonNumber = 12;
			GET( getURL(channelPort, "send")+"?id="+identifier+"&key=SELECT_IMAGE&value="+buttonNumber );
		}
		
		GUI.TextField(new Rect(0,0,100,100), ""+rss);
	}
	
}
