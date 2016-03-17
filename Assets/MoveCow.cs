using UnityEngine;
using System.Collections;

public class MoveCow : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        transform.position = new Vector3( 50*Mathf.Sin(Time.time), 0, 50*Mathf.Cos(Time.time) );

    }
}
