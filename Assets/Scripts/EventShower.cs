using UnityEngine;
using System.Collections;

public class EventShower : MonoBehaviour, IEventShower  {

	public TextMesh topText;
	public TextMesh bottomText;
	public int lineLength;

	private Event currentEvent;

    public Slide foreground;

	// Use this for initialization
	void Start () {
        Slide slide = GetComponent<Slide>();
        if (slide != null)
        {
            slide.SlideStateChanged += new System.Action<SlideState>(EventShower_SlideStateChanged);
        }
	}

    void EventShower_SlideStateChanged(SlideState obj)
    {
        if (obj == SlideState.Idle)
        {
            foreground.state = SlideState.Entering;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IEventShower implementation

	public void SetEvent (Event theEvent)
	{
		currentEvent = theEvent;

		topText.text = ResolveTextSize(currentEvent.text);
		bottomText.text = "";
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
		bottomText.text = ResolveTextSize(outcome.eventOutcomeText);
	}

	public void MoveIn ()
	{
		GetComponent<Slide> ().state = SlideState.Entering;
	}

	public void MoveOn ()
	{
		GetComponent<Slide> ().state = SlideState.Exiting;
	}

	// Wrap text by line height
	private string ResolveTextSize(string input)
	{	
		// Split string by char " "    
		string[] words = input.Split(' ');
		
		// Prepare result
		string result = "";
		
		// Temp line string
		string line = "";
		
		// for each all words     
		foreach(string s in words){
			// Append current word into line
			string temp = line + " " + s;
			
			// If line length is bigger than lineLength
			if(temp.Length > lineLength){
				
				// Append current line into result
				result += line + "\n";
				// Remain word append into new line
				line = s;
			}
			// Append current word into current line
			else {
				line = temp;
			}
		}
		
		// Append last line into result   
		result += line;
		
		// Remove first " " char
		return result.Substring(1,result.Length-1);
	}
}
