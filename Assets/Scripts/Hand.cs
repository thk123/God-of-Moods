using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {

    public GameObject optimismCard;
    public GameObject pesimismCard;
    public GameObject angerCard;
    public GameObject chillCard;

    public float gapBetweenCards;

    public int HandSize;

    public int CurrentHandSize
    {
        get
        {
            int numberOfCards = 0;
            foreach (MoodCard card in hand)
            {
                if (card != null)
                {
                    ++numberOfCards;
                }
            }

            return numberOfCards;
        }
    }

    Vector3 NextCardSpawnPoint
    {
        get
        {
            Vector3 cardSpawnPoint = transform.position;
            foreach (MoodCard card in hand)
            {
                if (card == null)
                {
                    return cardSpawnPoint;
                }
                else
                {
                    cardSpawnPoint -= gapBetweenCards * Vector3.up;
                }
            }

            throw new UnityException("No valid card slots");
        }
    }

    MoodCard[] hand;

	// Use this for initialization
	void Start () {
        hand = new MoodCard[HandSize];
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DealCard(MoodCard card)
    {
        
        if (CurrentHandSize >= HandSize - 1)
        {
            throw new UnityException("Too many cards in the hand");
        }

        hand[CurrentHandSize] = card;

        print("Placing card: " + CurrentHandSize + "/" + HandSize);

        switch (card.mood)
        {
            case eMood.Optimisism:
                Instantiate(optimismCard, NextCardSpawnPoint, Quaternion.AngleAxis(90.0f, Vector3.left) * Quaternion.AngleAxis(180.0f, Vector3.up));
                break;
            case eMood.Pessismism:
                Instantiate(pesimismCard, NextCardSpawnPoint, Quaternion.AngleAxis(90.0f, Vector3.left) * Quaternion.AngleAxis(180.0f, Vector3.up));
                break;
            case eMood.Anger:
                Instantiate(angerCard, NextCardSpawnPoint, Quaternion.AngleAxis(90.0f, Vector3.left) * Quaternion.AngleAxis(180.0f, Vector3.up));
                break;
            case eMood.Chilled:
                Instantiate(chillCard, NextCardSpawnPoint, Quaternion.AngleAxis(90.0f, Vector3.left) * Quaternion.AngleAxis(180.0f, Vector3.up));
                break;
            case eMood.eMoodCount:
                throw new UnityException("Unknown mood type");
            default:
                throw new UnityException("Unknown mood type");
        }
    }
}
