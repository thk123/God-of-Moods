using UnityEngine;
using System.Collections;

public interface IDropBox
{
    void DropCard(MoodCard card);

    bool CanDrop
    {
        get;
    }
}

public class DropBox : MonoBehaviour, IDropBox {

    bool internalCanDrop;

    public Texture2D canDropTexture;
    public Texture2D cantDropTexture;

    MoodCard cardOnBox;

    public bool CanDrop
    {
        get 
        {
            return internalCanDrop;
        }
        set
        {
            internalCanDrop = value;
            if (internalCanDrop)
            {
				renderer.material.color = Color.white;
                renderer.material.mainTexture = canDropTexture;
                if (cardOnBox != null)
                {
                    Destroy(cardOnBox.gameObject);
                }
            }
            else
            {
				switch (cardOnBox.mood) {
				case eMood.Optimisism:
					renderer.material.color = manager.OptimismColour;
					break;
				case eMood.Pessismism:
					renderer.material.color = manager.PessimismColour;
					break;
				case eMood.Anger:
					renderer.material.color = manager.AngerColour;
					break;
				case eMood.Chilled:
					renderer.material.color = manager.ChilledColour;
					break;
				}
				renderer.material.mainTexture = cantDropTexture;
            }
        }
    }

    public GameManager manager;

	// Use this for initialization
	void Start () {
        
        CanDrop = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DropCard(MoodCard card)
    {
        manager.PlayCard(card.mood);
        //GameObject.Destroy(card.gameObject);

        card.transform.position = transform.position;
        card.transform.Translate(Vector3.up);     
        cardOnBox = card;
        CanDrop = false;
    }
}
