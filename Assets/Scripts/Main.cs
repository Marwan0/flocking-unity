using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var cow = Instantiate(Resources.Load("cow")) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
