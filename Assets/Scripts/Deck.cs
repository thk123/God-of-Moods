using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

	public int[] cardDistributions = new int[(int)eMood.eMoodCount];

    List<eMood> CurrentDeck;

    public AudioSource newCardDrawn;


    public int CardsRemaining
    {
        get
        {
            return CurrentDeck.Count;
        }
    }

    float startingHeight;

	// Use this for initialization
    void Start()
    {
        if (cardDistributions.Length != (int)eMood.eMoodCount)
        {
            throw new UnityException("Invalid card distribution");
        }

        CurrentDeck = new List<eMood>();

        foreach (eMood moodId in (eMood[])Enum.GetValues(typeof(eMood)))
        {
            if (moodId != eMood.eMoodCount)
            {
                for (int i = 0; i < cardDistributions[(int)moodId]; ++i)
                {
                    CurrentDeck.Add(moodId);
                }
            }
        }

        CurrentDeck.Shuffle();

        startingHeight = transform.localScale.y;
            
    }

    // Update is called once per frame
    void Update()
    {
	
	}

    public eMood DrawCard()
    {
        eMood topCard = CurrentDeck[0];
        CurrentDeck.RemoveAt(0);

        //transform.localScale.y -= cardHeight;

        newCardDrawn.Play();

        
        return topCard;
    }
}

public enum eMood
{
    Optimisism,
    Pessismism,
    Anger,
    Chilled,

    eMoodCount,
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