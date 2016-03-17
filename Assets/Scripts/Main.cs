using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    public int numberOfCows = 100;

	// Use this for initialization
	void Start () {

        for ( int i = 0; i < numberOfCows; i++ ) {
            var cow = Instantiate(Resources.Load("cow")) as GameObject;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
