using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {
    /*
    public AudioClip jumpSound;
    public AudioClip translationSound;
    public AudioClip rotationSound;
    public AudioClip symetrieSound;

    public AudioClip rewindTranslationSound;
    public AudioClip rewindRotationSound;
    public AudioClip rewindSymetrieSound;*/

    public List<AudioClip> audioList;
    AudioSource[] sources;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        sources = GetComponents<AudioSource>();
    }

    void Update()
    {
        if (!sources[1].isPlaying)
        {
            sources[1].PlayOneShot(audioList[Random.Range(14, 22)], 1f);
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
