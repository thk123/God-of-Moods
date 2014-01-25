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
            foreach (GameObject card in hand)
            {
                if (card != null)
                {
                    ++numberOfCards;
                }
            }

            return numberOfCards;
        }
    }

    public int NextHandSlot
    {
        get
        {
            for (int i = 0; i < hand.Length; ++i)
            {
                if (hand[i] == null)
                {
                    return i;
                }
            }

            throw new UnityException("No slot");
        }
    }

    Vector3 NextCardSpawnPoint
    {
        get
        {
            Vector3 cardSpawnPoint = transform.position;
            foreach (GameObject card in hand)
            {
                if (card == null)
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

    GameObject[] hand;

	// Use this for initialization
	void Start () {
        hand = new GameObject[HandSize];

        for (int i = 0; i < HandSize; ++i)
        {
            hand[i] = null;
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

        hand[NextHandSlot] = newCard;

        newCard.GetComponent<MoodCard>().mood = card;

        newCard.GetComponent<Draggable>().OnSpent += new System.EventHandler(Hand_OnSpent);

        newCard.transform.parent = transform;
    }

    void Hand_OnSpent(object sender, System.EventArgs e)
    {
        Draggable card = sender as Draggable;

        if (card != null)
        {
            for (int i = 0; i < hand.Length; ++i)
            {
                if (hand[i] == card.gameObject)
                {
                    hand[i] = null;
                    return;
                }
            }
            throw new UnityException("Could not find card to remove from hand");
        }
        else
        {
            throw new UnityException("Was not a draggable that stopped being dragged??");   
        }

        
    }

}
