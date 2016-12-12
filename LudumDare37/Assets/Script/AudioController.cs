using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {
    public List<AudioClip> audioList;
    public List<float> audioListVolume;
    public List<bool> audioListMuteMusic;

    public List<AudioClip> audioListMusic;
    public List<float> audioListMusicVolume;
    AudioSource[] sources;
    public bool juanTest;

	private int nextSong = 0;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        sources = GetComponents<AudioSource>();
    }

    void Update()
    {
        if (juanTest)
        {
            if (sources[0].isPlaying)
            {
                if(sources[1].volume > 0)
                {
                    sources[1].volume -= Time.deltaTime * 2f;
                }
            }
            else
            {
				if (sources[1].volume < audioListMusicVolume[nextSong])
                {
                    sources[1].volume += Time.deltaTime * 2f;
                }
            }
        }
        if (!sources[1].isPlaying)
        {
            nextSong = Random.Range(0, 8);
			print (audioListMusic [nextSong]);
			print (nextSong);
            sources[1].PlayOneShot(audioListMusic[nextSong], audioListMusicVolume[nextSong]);
        }
    }

    public void playClip(int audioIndex)
    {
        if (audioListMuteMusic[audioIndex])
        {
            sources[0].PlayOneShot(audioList[audioIndex], audioListVolume[audioIndex]);
        }
        else
        {
            sources[2].PlayOneShot(audioList[audioIndex], audioListVolume[audioIndex]);
        }
    }


    private static AudioController s_Instance = null;

    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static AudioController instance
    {
        get
        {
            if (s_Instance == null)
            {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first AManager object in the scene.
                s_Instance = FindObjectOfType(typeof(AudioController)) as AudioController;
            }

            // If it is still null, create a new instance
            if (s_Instance == null)
            {
                GameObject obj = Instantiate(Resources.Load("AudioController") as GameObject);
                s_Instance = obj.GetComponent<AudioController>();
            }
            return s_Instance;
        }
    }
}
