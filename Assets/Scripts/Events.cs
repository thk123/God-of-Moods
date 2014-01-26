using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Events : MonoBehaviour {

    public const string EventDeckTag = "EventDeck";

    public List<Event> events;

    public eMood DeckMood;

	// Use this for initialization
	void Start () {
        events.Shuffle();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Event DrawEvent()
    {
        Event topEvent = events[0];
        topEvent.eventCategory = DeckMood;
        events.RemoveAt(0);
        return topEvent;
    }
}

[Serializable]
public class Event
{
    public Texture2D eventPicture;
    public Material eventMaterial;

    public eMood eventCategory;

    public string text;

    public EventOutcome optimismOutcome;
    public EventOutcome pessimismOutcome;
    public EventOutcome angerOutcome;
    public EventOutcome chilledOutcome;
    
}

[Serializable]
public class EventOutcome
{
    public string eventOutcomeText;
	public Texture2D eventOutcomePicture;
}