using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    enum GameState
    {
        Begin,
        WaitingOnInput
    }

    GameState gameState;

    Events optimismEvents;
    Events pessimismEvents;
    Events angerEvents;
    Events chilledEvents;

    Deck cardDeck;

    Hand hand;

    int optimismValue;
    int pessimismValue;
    int angerValue;
    int chillValue;

    public MeshRenderer eventRenderObject;

    int totalValue
    {
        get
        {
            return optimismValue + pessimismValue + angerValue + chillValue;
        }
    }

	// Use this for initialization
	void Start () {
        Events[] eventDecks = GameObject.FindObjectsOfType<Events>();

        cardDeck = GameObject.FindObjectOfType<Deck>();

        hand = GameObject.FindObjectOfType<Hand>();

        foreach (Events eventDeck in eventDecks)
        {
            switch (eventDeck.DeckMood)
            {
                case eMood.Optimisism:
                    optimismEvents = eventDeck;
                    break;
                case eMood.Pessismism:
                    pessimismEvents = eventDeck;
                    break;
                case eMood.Anger:
                    angerEvents = eventDeck;
                    break;
                case eMood.Chilled:
                    chilledEvents = eventDeck;
                    break;
                case eMood.eMoodCount:
                    throw new UnityException("Invalid deck type");
                default:
                    throw new UnityException("Invalid deck type");
            }
        }

        ResetValues();

        gameState = GameState.Begin;

        


	}

    void ResetValues()
    {
        optimismValue = 4;
        pessimismValue = 0;
        angerValue = 0;
        chillValue = 0;
    }

    void TakeEvent()
    {
        int value = Random.Range(0, totalValue);
        Events selecteDeck;
        if (value <= optimismValue)
        {
            selecteDeck = optimismEvents;
        }
        else if (value <= pessimismValue)
        {
            selecteDeck = pessimismEvents;
        }
        else if (value <= angerValue)
        {
            selecteDeck = angerEvents;
        }
        else
        {
            selecteDeck = chilledEvents;
        }

        Event selectedEvent = selecteDeck.DrawEvent();
        print(selectedEvent.text);
        eventRenderObject.material = selectedEvent.eventMaterial;
        //eventRenderObject
    }
	// Update is called once per frame
	void Update () {
        switch (gameState)
        {
            case GameState.Begin:
                StartGo();
                gameState = GameState.WaitingOnInput;
                break;
            case GameState.WaitingOnInput:
                break;
            default:
                break;
        }
	}

    void StartGo()
    {
        while (hand.CurrentHandSize < hand.HandSize - 1)
        {
            hand.DealCard(cardDeck.DrawCard());
        }

        TakeEvent();
    }
}
