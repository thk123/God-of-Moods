using UnityEngine;
using System.Collections;
using System;

public class BackgroundMusics : MonoBehaviour {

    class BGMusic
    {
        public AudioSource player
        {
            get;
            private set;
        }

        public FadeSound fader
        {
            get;
            private set;
        }

        public BGMusic(AudioClip clip, Transform parent)
        {
            GameObject playerObject = new GameObject();
            print(playerObject);
            playerObject.transform.parent = parent;
            player = playerObject.AddComponent<AudioSource>();
            player.loop = true;
            fader = playerObject.AddComponent<FadeSound>();


            if (clip != null)
            {
                playerObject.name = clip.name;
                player.clip = clip;
            }
            else
            {
                playerObject.name = "Bass";
            }
            
            
            fader.FadeDuration = 0.5f;
            fader.source = player;
            
        }
    }

    public AudioClip[] bassTracks = new AudioClip[(int)eMood.eMoodCount];
    public AudioClip[] level1Tracks = new AudioClip[(int)eMood.eMoodCount];
    public AudioClip[] level2Tracks = new AudioClip[(int)eMood.eMoodCount];

    BGMusic[] level1Players = new BGMusic[(int)eMood.eMoodCount];
    BGMusic[] level2Players = new BGMusic[(int)eMood.eMoodCount];

    public GameManager manager;

    BGMusic bassPlayer;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < (int)eMood.eMoodCount; ++i)
        {
            level1Players[i] = new BGMusic(level1Tracks[i], transform);
            level2Players[i] = new BGMusic(level2Tracks[i], transform);
        }

        bassPlayer = new BGMusic(null, transform);
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < (int)eMood.eMoodCount; ++i )
        {
            eMood mood = (eMood)i;
            ComputeSounds(mood, manager.moodValues[i]);

            eMood mostUsedMood = manager.MostUsedMood;
            int value = manager.moodValues[(int)mostUsedMood];

            if (value >= 4)
            {
                bassPlayer.player.clip = bassTracks[(int)mostUsedMood];
                bassPlayer.fader.FadeIn();
            }
        }
	}

    void ComputeSounds(eMood mood, int level)
    {
        if (level >= 1)
        {
            level1Players[(int)mood].fader.FadeIn();

            if (level >= 3)
            {
                level2Players[(int)mood].fader.FadeIn();
            }
            else
            {
                level2Players[(int)mood].fader.FadeOut();
            }
        }
        else
        {
            level1Players[(int)mood].fader.FadeOut();
        }
    }
}
