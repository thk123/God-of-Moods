using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

	public int[] cardDistributions = new int[(int)MoodCard.eMood.eMoodCount];

    List<MoodCard> CurrentDeck;

    public int CardsRemaining
    {
        get
        {
            return CurrentDeck.Count;
        }
    }

	// Use this for initialization
    void Start()
    {
        if (cardDistributions.Length != (int)MoodCard.eMood.eMoodCount)
        {
            throw new UnityException("Invalid card distribution");
        }

        CurrentDeck = new List<MoodCard>();

        foreach (MoodCard.eMood moodId in (MoodCard.eMood[])Enum.GetValues(typeof(MoodCard.eMood)))
        {
            CurrentDeck.Add(new MoodCard(moodId));
        }

        CurrentDeck.Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
	
	}

    public MoodCard DrawCard()
    {
        MoodCard topCard = CurrentDeck[0];
        CurrentDeck.RemoveAt(0);

        return topCard;
    }
}

public class MoodCard
{
	public enum eMood
	{
		Optimisism,
		Pessismism,
		Anger,
		Chilled,

		eMoodCount,
	}

	eMood mood;

    public MoodCard(eMood mood)
    {
        this.mood = mood;
    }
}

public static class Util
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}