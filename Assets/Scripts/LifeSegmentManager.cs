using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LifeSegmentManager : MonoBehaviour {

    public Color32 OptimismColour;
    public Color32 PessimismColour;
    public Color32 AngerColour;
    public Color32 ChilledColour;

    public int LifeSegments;

    public LifeSegment SegementPrefab;

    List<LifeSegment> segments;

    int currentLifeSeg;

    public float segmentGap;

	// Use this for initialization
	void Start () {
        segments = new List<LifeSegment>(LifeSegments);

        for (int i = 0; i < LifeSegments; ++i)
        {
            LifeSegment newSegment = (LifeSegment)Instantiate(SegementPrefab);
            segments.Add(newSegment);

            newSegment.transform.position = transform.position + (i * segmentGap * Vector3.right);

            newSegment.transform.parent = transform;
        }

        currentLifeSeg = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetSegment(eMood selectedMood)
    {
        Color32 segColour;
        switch (selectedMood)
        {
            case eMood.Optimisism:
                segColour = OptimismColour;
                break;
            case eMood.Pessismism:
                segColour = PessimismColour;
                break;
            case eMood.Anger:
                segColour = AngerColour;
                break;
            case eMood.Chilled:
                segColour = ChilledColour;
                break;
            case eMood.eMoodCount:
                throw new UnityException("Invalid card selected");
            default:
                throw new UnityException("Invalid card selected");
        }
        segments[currentLifeSeg].colour = segColour;

        ++currentLifeSeg;
    }
}
