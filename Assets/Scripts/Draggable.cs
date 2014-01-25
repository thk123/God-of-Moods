using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour {

    bool beingDragged;

    Vector3 startingPosition;

    IDropBox onDropBox;

	// Use this for initialization
	void Start () {
        beingDragged = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (beingDragged)
        {
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            transform.position = clickRay.GetPoint(2.0f);

            if (Input.GetMouseButtonUp(0))
            {
                beingDragged = false;

                if (onDropBox != null)
                {
                    onDropBox.DropCard(GetComponent<MoodCard>());
                }
                else
                {
                    transform.position = startingPosition;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                onDropBox = null;
                Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                // THIS IS NOT WORKING -- BEFORE WE WERE USING 3D RAY CAST, NOW NEED TO MODIFY TO 2D so not even a fecking ray cast

                //RaycastHit info;
                if (collider2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    //if (info.collider.gameObject == gameObject)
                    {
                        beingDragged = true;
                        startingPosition = transform.position;
                    }
                }
            }
        }
        
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("OnTriggerEnter2D");
        IDropBox dropBox = (IDropBox)collision.gameObject.GetComponent(typeof(IDropBox));

        if (dropBox != null)
        {
            print("Found drop box");
            onDropBox = dropBox;
        }
        else
        {
            onDropBox = null;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        onDropBox = null;
    }
}
