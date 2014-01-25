using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class LifeSegment : MonoBehaviour {

    bool selected;

    MeshRenderer rendererer;

    public Color32 colour
    {
        set
        {
            renderer.material.color = value;
        }
    }

	// Use this for initialization
	void Start () {
        selected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
