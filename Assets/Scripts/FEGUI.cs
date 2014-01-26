using UnityEngine;
using System.Collections;
using System.Threading;
public class FEGUI : MonoBehaviour {

    public Player player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {

        float leftMargin = Screen.width * 0.25f;

        GUIContent label = new GUIContent("Enter Name: ");


        GUIStyle style = new GUIStyle();
        Vector2 size = style.CalcSize(label);
        size.x += 50;
        size.y *= 3;
        float topMargin = (Screen.height * 0.5f) - (size.y * 0.5f);

        GUI.TextArea(new Rect(leftMargin, topMargin, size.x, size.y), "Enter Name: ");
        player.playerName = GUI.TextField(new Rect(leftMargin + size.x + 50, topMargin, 3 * size.x, size.y), player.playerName, 36);

        if (GUI.Button(new Rect(leftMargin + (4 * size.x) + 100.0f, topMargin, size.x, size.y), "Play"))
        {
            Application.LoadLevel("Main");

        }
    }
}
