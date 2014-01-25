using UnityEngine;
using System.Collections;

public enum SlideState
{
	Entering,
	Idle,
	Exiting
};

public class Slide : MonoBehaviour {

    public Vector3 startPosition;
	public Vector3 idlePosition;
    public Vector3 endPosition;

    public float timeToTake;

	public SlideState state 
	{
		get;
		set;
	}

    float timePassed;

	// Use this for initialization
	void Start () {
		state = SlideState.Exiting;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (state) 
		{
			case SlideState.Entering:
			{
				timePassed += Time.deltaTime;
				transform.position = Vector3.Lerp (startPosition, idlePosition, timePassed / timeToTake);
				break;
			}
			case SlideState.Idle: 
			{
				transform.position = idlePosition;
				break;
			}
			case SlideState.Exiting:
			{
				timePassed += Time.deltaTime;
				transform.position = Vector3.Lerp (idlePosition, endPosition, timePassed / timeToTake);
				break;
			}
		}
	}
}
