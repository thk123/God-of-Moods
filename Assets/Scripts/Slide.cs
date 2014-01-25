using UnityEngine;
using System.Collections;

public class Slide : MonoBehaviour {

    Vector3 startPosition;
    Vector3 endPosition;

    float timeToTake;

    float timePassed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(startPosition, endPosition, timePassed / timeToTake);
	}
}
