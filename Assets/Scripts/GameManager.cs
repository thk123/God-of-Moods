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
        EndGame,

    }

    public Renderer background;

    public Color32 DefaultColour;
    public Color32 OptimismColour;
    public Color32 PessimismColour;
    public Color32 AngerColour;
    public Color32 ChilledColour;

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

    public eMood MostUsedMood
    {
        get
        {

            
            eMood currentMood = lastMood;

            int maxValue = moodValues[(int)currentMood];

            for (int i = 0; i < (int)eMood.eMoodCount; ++i)
            {
                if (moodValues[i] > maxValue)
                {
                    currentMood = (eMood)i;
                }
            }
            return currentMood;
        }
    }

    public int[] moodValues = new int[(int)eMood.eMoodCount];

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
        //lastMood = (eMood)Random.Range(0, (int)eMood.eMoodCount);
        lastMood = eMood.Anger;

        moodValues = new int[(int)eMood.eMoodCount];

        for (int i = 0; i < moodValues.Length; ++i)
        {
            moodValues[i] = 0;
        }

        background.material.color = DefaultColour;

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

            case GameState.EndGame:

            default:
                break;
        }
	}

    void StartGo()
    {
        print("Starting go: " + hand.CurrentHandSize + "/" + hand.HandSize);
        while (hand.CurrentHandSize < hand.HandSize)
        {            
            hand.DealCard(cardDeck.DrawCard());
        }

        TakeEvent();

        dropBox.CanDrop = true;

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

        switch (card)
        {
            case eMood.Optimisism:
                
                background.material.color = OptimismColour;
                break;
            case eMood.Pessismism:
                
                background.material.color = PessimismColour;
                break;
            case eMood.Anger:
                
                background.material.color = AngerColour;
                break;
            case eMood.Chilled:
                
                background.material.color = ChilledColour;
                break;
            case eMood.eMoodCount:
                throw new UnityException("Invalid card");
            default:
                throw new UnityException("Invalid card");
        }

        ++moodValues[(int)card];

        button.Avaliable = true;

        gameState = GameState.WaitingOnConfirmation;
    }

    public void Advance()
    {
        if (gameState != GameState.WaitingOnConfirmation)
        {
            throw new UnityException("Invalid state to advance from");
        }

        if (lifeSegmentBar.IsFull)
        {
            gameState = GameState.EndGame;
            GetComponent<EndGame>().PopulateEndView();
        }
        else
        {
            gameState = GameState.Begin;
        }
    }
}
