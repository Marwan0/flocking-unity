using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    public int numberOfCows = 100;
    public float maxRadius = 50;
    public float maxSpeed = 3;

	// Use this for initialization
	void Start () {

        for ( int i = 0; i < numberOfCows; i++ ) {
            var cow = Instantiate(Resources.Load("cow")) as GameObject;
            cow.GetComponent<MoveCow>().timeOffset = Random.Range(-100,100);
            cow.GetComponent<MoveCow>().radius     = Random.Range(10,maxRadius);
            cow.GetComponent<MoveCow>().speed      = Random.Range(-maxSpeed,maxSpeed);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
