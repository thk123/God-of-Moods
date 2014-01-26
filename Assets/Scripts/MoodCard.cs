using UnityEngine;
using System.Collections;

public class MoodCard : MonoBehaviour
{
    public eMood mood;

    public Texture2D cardTexture;

    void Start()
    {
        renderer.material.mainTexture = cardTexture;
    }

    void Update()
    {


    }
}

