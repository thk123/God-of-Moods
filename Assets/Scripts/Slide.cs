using UnityEngine;
using System.Collections;

public enum SlideState
{
	Paused,
	Entering,
	Idle,
	Exiting
};

public class Slide : MonoBehaviour {

    public Vector3 startPosition;
	public Vector3 idlePosition;
    public Vector3 endPosition;

    public float timeToTake;

	SlideState currentState;
	public SlideState state 
	{
		get
		{
			return currentState;
		}
		set
		{
			currentState = value;
		}
	}

    float timePassed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (state) 
		{
			case SlideState.Paused:
			transform.position = startPosition;
			break;
			case SlideState.Entering:
			{
				timePassed += Time.deltaTime;
				transform.position = Vector3.Lerp (startPosition, idlePosition, timePassed / timeToTake);

				if(timePassed >= timeToTake)
				{
					state = SlideState.Idle;
				}
				break;
			}
			case SlideState.Idle: 
			{
				transform.position = idlePosition;
				timePassed = 0.0f;
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
