using UnityEngine;
using System.Collections;

public class EventShower : MonoBehaviour, IEventShower  {

	public TextMesh topText;
	public TextMesh bottomText;

	private Event currentEvent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IEventShower implementation

	public void SetEvent (Event theEvent)
	{
		currentEvent = theEvent;

		topText.text = currentEvent.text;

	}

	public void SetOutcome (eMood mood)
	{
		switch (mood) 
		{
			case eMood.Optimisism: ApplyOutcome(currentEvent.optimismOutcome); break;
			case eMood.Pessismism: ApplyOutcome(currentEvent.pessimismOutcome); break;
			case eMood.Anger: ApplyOutcome(currentEvent.angerOutcome); break;
			case eMood.Chilled: ApplyOutcome(currentEvent.chilledOutcome); break;
		}
	}

	#endregion

	void ApplyOutcome(EventOutcome outcome)
	{
		bottomText.text = outcome.eventOutcomeText;
	}
}
