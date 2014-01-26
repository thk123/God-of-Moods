using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public string playerName = "Jammer";

	// Use this for initialization

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
