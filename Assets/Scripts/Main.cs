using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

    // adapted from https://processing.org/examples/flocking.html

    public int numberOfCows = 100;
    public float areaSize = 100;
    public Camera camera;
    public static float viewSize;

    Flock flock;

	// Use this for initialization
	void Start () {
        
        flock = new Flock();
        for ( int i = 0; i < numberOfCows; i++ ) {
            var cow = new Boid(areaSize/2,areaSize/2);
            var cowObject = Instantiate(Resources.Load("cow")) as GameObject;
            cowObject.GetComponent<MoveCow>().cowBoid = cow;
            flock.AddBoid(cow);
        }

	}
	
	// Update is called once per frame
    void Update () {
        camera.orthographicSize = areaSize/2;
        camera.transform.position = new Vector3 ( areaSize/2, 100, areaSize/2 );
        viewSize = areaSize;  // make static variable so all Boids can easily access from Borders()
        flock.Update();
	}

//    void mousePressed() {
//        flock.AddBoid(new Boid(Input.mousePosition.x,Input.mousePosition.y));
//    }

}