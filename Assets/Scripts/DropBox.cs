using UnityEngine;
using System.Collections;

public interface IDropBox
{
    void DropCard(MoodCard card);
}

public class DropBox : MonoBehaviour, IDropBox {

    bool internalCanDrop;

    public Texture2D canDropTexture;
    public Texture2D cantDropTexture;

    public bool canDrop
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
                renderer.material.mainTexture = canDropTexture;
            }
            else
            {
                renderer.material.mainTexture = cantDropTexture;

            }
        }
    }

    public GameManager manager;

	// Use this for initialization
	void Start () {
        
        canDrop = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DropCard(MoodCard card)
    {
        manager.PlayCard(card.mood);
        GameObject.Destroy(card.gameObject);

        canDrop = false;
    }
}
