using UnityEngine;
using System.Collections;

public interface IDropBox
{
    void DropCard(MoodCard card);
}

public class DropBox : MonoBehaviour, IDropBox {

    public GameManager manager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DropCard(MoodCard card)
    {
        manager.PlayCard(card.mood);
        GameObject.Destroy(card.gameObject);
    }
}
