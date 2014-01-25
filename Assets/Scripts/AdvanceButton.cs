using UnityEngine;
using System.Collections;

public class AdvanceButton : MonoBehaviour {

    public Texture2D avaliableButton;
    public Texture2D pressedButton;
    public Texture2D disabledButton;

    public GameManager gameManager;

    bool isEnabled;
    public bool Avaliable
    {
        get
        {
            return isEnabled;
        }
        set
        {
            isEnabled= value;
            if (value)
            {
                renderer.material.mainTexture = avaliableButton;
            }
            else
            {
                renderer.material.mainTexture = disabledButton;
            }
        }
    }

    bool isPressed;
    public bool Pressed
    {
        get
        {
            return isPressed;
        }
        set
        {
            isPressed = value;
            if(enabled)
            {
                if (isPressed)
                {
                    renderer.material.mainTexture = pressedButton;
                }
                else
                {
                    renderer.material.mainTexture = avaliableButton;

                }
            }
        }
    }

	// Use this for initialization
	void Start () {
        Avaliable = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Avaliable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if(collider2D.OverlapPoint(pos))
                {
                    Pressed = true;
                }
            }
            else if (Input.GetMouseButtonUp(0) && Pressed)
            {
                Pressed = false;
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (collider2D.OverlapPoint(pos))
                {
                    gameManager.Advance();
                }
                
            }
        }
	}
}
