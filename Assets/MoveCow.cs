using UnityEngine;
using System.Collections;

public class MoveCow : MonoBehaviour {
    
    public float timeOffset = 0;
    public float radius = 50;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        transform.position = new Vector3( radius*Mathf.Sin(Time.time + timeOffset), 0, radius*Mathf.Cos(Time.time + timeOffset) );

    }
}
