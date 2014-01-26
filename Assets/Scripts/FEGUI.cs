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

        float leftMargin = Screen.width * 0.5f;

        GUIContent label = new GUIContent("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");


        GUIStyle style = new GUIStyle();
        Vector2 size = style.CalcSize(label);
        size.x += 50;
        size.y *= 3;
        float topMargin = (Screen.height * 0.6f) - (size.y * 0.5f);

        
        player.playerName = GUI.TextField(new Rect(leftMargin - (0.5f * size.x), topMargin, size.x, size.y), player.playerName, 36);

        if (GUI.Button(new Rect(leftMargin - (0.5f * size.x), topMargin + (size.y), size.x, size.y), "Play"))
        {
            Application.LoadLevel("Main");

        }
    }
}
