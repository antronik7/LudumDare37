using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {
    public List<AudioClip> audioList;
    public List<AudioClip> audioListMusic;
    AudioSource[] sources;
    public bool juanTest;

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
                sources[1].volume = 0f;
            }
            else
            {
                sources[1].volume = 1f;
            }
        }
        if (!sources[1].isPlaying)
        {
            sources[1].PlayOneShot(audioListMusic[Random.Range(0, 8)], 1f);
        }
    }

    public void playClip(int audioIndex)
    {
        sources[0].PlayOneShot(audioList[audioIndex], 1f);
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
