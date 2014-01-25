using UnityEngine;
using System.Collections;

public interface IEventShower
{
    void SetEvent(Event theEvent);

    void SetOutcome(eMood mood);
}

public class GameManager : MonoBehaviour {

    enum GameState
    {
        Begin,
        WaitingOnCardChoose,
        WaitingOnConfirmation,

    }

    

    GameState gameState;

    Events optimismEvents;
    Events pessimismEvents;
    Events angerEvents;
    Events chilledEvents;

    Deck cardDeck;

    Hand hand;


    IEventShower shower;

    LifeSegmentManager lifeSegmentBar;

    DropBox dropBox;

    eMood lastMood;

    AdvanceButton button;

	// Use this for initialization
	void Start () {
        Events[] eventDecks = GameObject.FindObjectsOfType<Events>();

        cardDeck = GameObject.FindObjectOfType<Deck>();

        hand = GameObject.FindObjectOfType<Hand>();

		shower = FindObjectOfType<ContentController>();

        dropBox = FindObjectOfType<DropBox>();

        button = FindObjectOfType<AdvanceButton>();

        lifeSegmentBar = FindObjectOfType<LifeSegmentManager>();

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
        lastMood = (eMood)Random.Range(0, (int)eMood.eMoodCount);
    }

    void TakeEvent()
    {
        Events selecteDeck;

        switch (lastMood)
        {
            case eMood.Optimisism:
                selecteDeck = optimismEvents;
                break;
            case eMood.Pessismism:
                selecteDeck = pessimismEvents;
                break;
            case eMood.Anger:
                selecteDeck = angerEvents;
                break;
            case eMood.Chilled:
                selecteDeck = chilledEvents;
                break;
            case eMood.eMoodCount:
                throw new UnityException("Invalid last mood");
            default:
                throw new UnityException("Invalid last mood");
        }

        Event selectedEvent = selecteDeck.DrawEvent();

        shower.SetEvent(selectedEvent);
    }

	// Update is called once per frame
	void Update () {
        switch (gameState)
        {
            case GameState.Begin:
                StartGo();
                gameState = GameState.WaitingOnCardChoose;
                break;
            case GameState.WaitingOnCardChoose:
                break;

            case GameState.WaitingOnConfirmation:

                // TODO
                break;
            default:
                break;
        }
	}

    void StartGo()
    {
        print("Starting go: " + hand.HandSize + "/" + hand.CurrentHandSize);
        while (hand.CurrentHandSize < hand.HandSize)
        {            
            hand.DealCard(cardDeck.DrawCard());
        }

        TakeEvent();

        dropBox.canDrop = true;

        button.Avaliable = false;
    }

    public void PlayCard(eMood card)
    {
        if (gameState != GameState.WaitingOnCardChoose)
        {
            throw new UnityException("Invalid state to be playing a card");
        }

        lastMood = card;

        shower.SetOutcome(card);

        lifeSegmentBar.SetSegment(card);

        button.Avaliable = true;

        gameState = GameState.WaitingOnConfirmation;
    }

    public void Advance()
    {
        if (gameState != GameState.WaitingOnConfirmation)
        {
            throw new UnityException("Invalid state to advance from");
        }

        gameState = GameState.Begin;
    }
}
