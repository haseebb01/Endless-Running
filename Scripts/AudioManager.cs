using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name; // Name of the sound
        public AudioClip clip; // The actual audio clip
        [HideInInspector]
        public AudioSource source; // The AudioSource for playing the clip
        public bool loop; // Whether the sound should loop
    }

    public Sound[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

        }

        playsound("MainTheme");

    }
    public void playsound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.Play();
        }
    }
}
