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
            foreach (eMood card in hand)
            {
                if (card != eMood.eMoodCount)
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
            foreach (eMood card in hand)
            {
                if (card == eMood.eMoodCount)
                {
                    return cardSpawnPoint;
                }
                else
                {
                    
                    cardSpawnPoint -= gapBetweenCards * Vector3.left;
                }
            }

            throw new UnityException("No valid card slots");
        }
    }

    eMood[] hand;

	// Use this for initialization
	void Start () {
        hand = new eMood[HandSize];

        for (int i = 0; i < HandSize; ++i)
        {
            hand[i] = eMood.eMoodCount;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DealCard(eMood card)
    {
        
        if (CurrentHandSize >= HandSize)
        {
            throw new UnityException("Too many cards in the hand");
        }
               

        print("Placing card: " + CurrentHandSize + "/" + HandSize);
        GameObject newCard;
        switch (card)
        {
            case eMood.Optimisism:
                newCard = (GameObject)Instantiate(optimismCard, NextCardSpawnPoint, Quaternion.AngleAxis(90.0f, Vector3.left) * Quaternion.AngleAxis(180.0f, Vector3.up));
                break;
            case eMood.Pessismism:
                newCard = (GameObject)Instantiate(pesimismCard, NextCardSpawnPoint, Quaternion.AngleAxis(90.0f, Vector3.left) * Quaternion.AngleAxis(180.0f, Vector3.up));
                break;
            case eMood.Anger:
                newCard = (GameObject)Instantiate(angerCard, NextCardSpawnPoint, Quaternion.AngleAxis(90.0f, Vector3.left) * Quaternion.AngleAxis(180.0f, Vector3.up));
                break;
            case eMood.Chilled:
                newCard = (GameObject)Instantiate(chillCard, NextCardSpawnPoint, Quaternion.AngleAxis(90.0f, Vector3.left) * Quaternion.AngleAxis(180.0f, Vector3.up));
                break;
            case eMood.eMoodCount:
                throw new UnityException("Unknown mood type");
            default:
                throw new UnityException("Unknown mood type");
        }

        hand[CurrentHandSize] = card;

        newCard.GetComponent<MoodCard>().mood = card;

        newCard.transform.parent = transform;
    }
}
