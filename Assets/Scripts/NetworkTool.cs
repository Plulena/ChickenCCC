using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkTool : MonoBehaviour {
	protected string host = "scaa.kr";
	protected int gamePort = 3000;
	protected int channelPort = 10001;
	protected long rss = -1;
	protected int recvCounter = 0;
	protected int positionValue = 0;
	protected string identifier;
	
	// make url dynamically.
	public string getURL(int port, string page) {
		return "http://" + host + ":" + port + "/" + page;	
	}
	
	public int connect() {
		identifier = SystemInfo.deviceUniqueIdentifier;
		Debug.Log("identifier:"+identifier);
		register();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("latitude", "1.0");
		dictionary.Add("longitude", "1.0");
		dictionary.Add("gameCode", "10010");
		dictionary.Add("action", "shakeUpDown");
		POST(getURL(gamePort, "setInstantPlay"), dictionary);
		GET (getURL(channelPort,"") + "?id="+identifier);
		
		return 0;
	}
	
	public void register() {
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("uid", identifier);
		dictionary.Add("name", identifier);
		dictionary.Add("device", identifier);
		POST(getURL(gamePort, "setUser"), dictionary);
	}
	
	// Http method for GET
	public WWW GET(string url) {
		WWW www = new WWW (url);
    	StartCoroutine (WaitForRequest (www));
    	return www; 
    }
	
	// Http method for POST
	public WWW POST(string url, Dictionary<string,string> post) {
    	WWWForm form = new WWWForm();
    	foreach(KeyValuePair<string,string> post_arg in post) {
			if( post_arg.Value.Contains(",") )
				form.AddField(post_arg.Key, int.Parse(post_arg.Value.Substring(2)));
			else
       			form.AddField(post_arg.Key, post_arg.Value);
    	}
        WWW www = new WWW(url, form);

        StartCoroutine(WaitForRequest(www));
    	return www; 
    }

	// Make information from received data.
    public virtual IEnumerator WaitForRequest(WWW www) {
        yield return www;
		
        // check for errors
		Debug.Log("WWW Ok!: " + www.text);
		if (www.error == null) {
			var Reader = new LitJson.JsonReader(www.text);
			bool needPrintMessage = true;
			string latestIdentifier = "";
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
			}
			if( needPrintMessage ) {
				Debug.Log("WWW Ok!: " + www.text);
			}
        } else {
            Debug.Log("WWW Error: "+ www.error);
		}
    }
}

