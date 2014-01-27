using UnityEngine;
using System.Collections;

public class FadeSound : MonoBehaviour {

    public enum FadeState
    {
        Idle,
        FadeIn,
        On,
        FadeOut
    }

    public FadeState CurrentState;

    public AudioSource source;

    public float FadeDuration;

    float fadeTime;

	// Use this for initialization
	void Start () {
        fadeTime = 0.0f;
        source.volume = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        switch (CurrentState)
        {
            case FadeState.Idle:
                fadeTime = 0.0f;
				if(!source.isPlaying)
				{
					source.Play();
				}
                break;

            case FadeState.On:
                fadeTime = 0.0f;
                break;
            case FadeState.FadeIn:
                source.volume = Mathf.Lerp(0.0f, 1.0f, fadeTime / FadeDuration);
                fadeTime += Time.deltaTime;
                if (fadeTime >= FadeDuration)
                {
                    CurrentState = FadeState.On;
                }
                break;
            case FadeState.FadeOut:
                source.volume = Mathf.Lerp(1.0f, 0.0f, fadeTime / FadeDuration);
                fadeTime += Time.deltaTime;
                if (fadeTime >= FadeDuration)
                {
                    CurrentState = FadeState.Idle;
                }
                break;
            default:
                break;
        }
	}

    public void FadeIn()
    {
        switch (CurrentState)
        {
            case FadeState.Idle:
                CurrentState = FadeState.FadeIn;
                break;
            case FadeState.FadeIn:
                break;
            case FadeState.On:

                break;
            case FadeState.FadeOut:
                fadeTime = 1.0f - fadeTime;
                CurrentState = FadeState.FadeIn;
                break;
            default:
                break;
        }
    }

    public void FadeOut()
    {
        switch (CurrentState)
        {
            case FadeState.Idle:
                break;
            case FadeState.FadeIn:
                CurrentState = FadeState.FadeOut;
                fadeTime = 1.0f - fadeTime;
                break;
            case FadeState.On:
                CurrentState = FadeState.FadeOut;
                break;
            case FadeState.FadeOut:
                break;
            default:
                break;
        }
    }
}
