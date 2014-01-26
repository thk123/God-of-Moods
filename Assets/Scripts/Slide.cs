using UnityEngine;
using System.Collections;
using System;
public enum SlideState
{
    Paused,
    Entering,
    Idle,
    Exiting,
    Done
};

public class Slide : MonoBehaviour {

    public Vector3 startPosition;
	public Vector3 idlePosition;
    public Vector3 endPosition;

    public event Action<SlideState> SlideStateChanged;

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
            if (SlideStateChanged != null)
            {
                SlideStateChanged(value);
            }
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
                transform.localPosition = Vector3.Lerp(startPosition, idlePosition, timePassed / timeToTake);

				if(timePassed >= timeToTake)
				{
					state = SlideState.Idle;
				}
				break;
			}
			case SlideState.Idle: 
			{
				//transform.localPosition = idlePosition;
				timePassed = 0.0f;
				break;
			}
			case SlideState.Exiting:
			{
				timePassed += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(idlePosition, endPosition, timePassed / timeToTake);
                if (timePassed >= timeToTake)
                {
                    state = SlideState.Done;
                }
				break;
			}

            case SlideState.Done:
            {
                break;
            }
		}
	}
}
