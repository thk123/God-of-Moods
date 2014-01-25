using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    void Awake()
    {
        Application.LoadLevelAdditive("Deck");
        Application.LoadLevelAdditive("OptimisticEvents");
        Application.LoadLevelAdditive("AngerEvents");
        Application.LoadLevelAdditive("ChilledEvents");
        Application.LoadLevelAdditive("PessimisticEvents");
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
