using UnityEngine;
using System.Collections;
using System;
public class FlippableCard : MonoBehaviour {

    public Vector3 startPosition;
    public Vector3 finishPosition;

    public float maxScale;

    public eMood mood;

    public Texture2D[] cardFaces = new Texture2D[(int)eMood.eMoodCount];

    public GameObject frontFace;

    public GameObject rotateMe;

    public float timeToTake;

    float startingAngle;

    public event Action<Vector3> OnArrive;

	// Use this for initialization
	void Start () {
	    frontFace.renderer.material.mainTexture = cardFaces[(int)mood];
        startingAngle = transform.rotation.eulerAngles.y;
        StartCoroutine("Flip");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    IEnumerator Flip()
    {
        if(timeToTake > 0.0f)
        {
            float time = 0.0f;

            while (time <= timeToTake)
            {
                float normalisedValue = Mathf.Sin((Mathf.PI * time) / (2 * timeToTake));
                rotateMe.transform.localRotation = Quaternion.AngleAxis(Mathf.Lerp(startingAngle, startingAngle + 180.0f, normalisedValue), Vector3.up);
                transform.position = Vector3.Lerp(startPosition, finishPosition, normalisedValue);
                yield return null;
                float scaleNormalisedValue = Mathf.Sin((Mathf.PI * time) / (timeToTake));
                transform.localScale = Matrix4x4.Scale((1 + (scaleNormalisedValue * maxScale)) * Vector3.one) * Vector3.one;

                time += Time.deltaTime;
            }

            if (OnArrive != null)
            {
                OnArrive(finishPosition);
            }

            Destroy(gameObject);
        }
    }
}
