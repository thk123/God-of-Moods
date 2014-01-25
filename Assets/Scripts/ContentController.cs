﻿using UnityEngine;
using System.Collections;

public class ContentController : MonoBehaviour, IEventShower
{
	public GameObject viewer;

	EventShower currentEventShower;

    public AudioSource outcomeArrivalSound;

	void Start()
	{
		currentEventShower = null;
	}

	#region IEventShower implementation
	
	public void SetEvent (Event theEvent)
	{
		GameObject newEventShower;
		newEventShower = (GameObject)Instantiate (viewer);

		if (currentEventShower != null) 
		{
			currentEventShower.MoveOn();
		}

		currentEventShower = newEventShower.GetComponent<EventShower> ();
		currentEventShower.SetEvent (theEvent);
		currentEventShower.MoveIn ();
	}
	
	public void SetOutcome (eMood mood)
	{
		currentEventShower.SetOutcome (mood);
        outcomeArrivalSound.Play();
	}
	
	#endregion

}
