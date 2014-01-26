using UnityEngine;
using System;
using System.Collections;

public class Draggable : MonoBehaviour {

    

    bool beingDragged;

    bool canDrag;

    Vector3 startingPosition;

    IDropBox onDropBox;

    Matrix4x4 scale;
    Matrix4x4 inverseScale;

    public float scaleFactor;

    public event EventHandler OnPickedUp;
    public event EventHandler OnSpent;

    public AudioSource cardPickedUpSound;
    public AudioSource cardResetSound;
    public AudioSource cardPlayedSound;


	// Use this for initialization
	void Start () {
        beingDragged = false;

        scale = Matrix4x4.Scale(scaleFactor * Vector3.one);
        inverseScale = scale.inverse;
        canDrag = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (canDrag)
        {
            if (beingDragged)
            {
                Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 pos = clickRay.GetPoint(8.0f);
                transform.position = pos;

                if (Input.GetMouseButtonUp(0))
                {
                    beingDragged = false;

                    if (onDropBox != null && onDropBox.CanDrop)
                    {
                        onDropBox.DropCard(GetComponent<MoodCard>());

                        if (OnSpent != null)
                        {
                            OnSpent(this, new EventArgs());
                        }

                        cardPlayedSound.Play();

                        canDrag = false;

                    }
                    else
                    {
                        cardResetSound.Play();
                        transform.position = startingPosition;
                    }

                    transform.localScale = inverseScale * transform.localScale;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    onDropBox = null;


                    if (AttemptClickOrtho())
                    {
                        beingDragged = true;
                        startingPosition = transform.position;

                        cardPickedUpSound.Play();

                        transform.localScale = scale * transform.localScale;

                        if (OnPickedUp != null)
                        {
                            OnPickedUp(this, new EventArgs());
                        }
                    }
                }
            }
        }
	}

    bool AttemptClickOrtho()
    {
        
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return collider2D.OverlapPoint(pos);
        
    }

    bool AttemptClickPersp()
    {
        Ray clickRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20.0f));

        RaycastHit info;
        if (Physics.Raycast(clickRay, out info))
        {
            if (info.collider.gameObject.transform.parent.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IDropBox dropBox = (IDropBox)collision.gameObject.GetComponent(typeof(IDropBox));

        if (dropBox != null)
        {
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
