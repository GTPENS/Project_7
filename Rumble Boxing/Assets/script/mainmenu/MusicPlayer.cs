using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private static MusicPlayer instance = null;

    public static MusicPlayer Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        var NewMusic: AudioClip;

        if (instance != null && instance != this)
        {
            if (instance.audio.clip != audio.clip)
            {
                instance.audio.clip = audio.clip;
                instance.audio.volume = audio.volume;
                instance.audio.Play();
            }

            Destroy(this.gameObject);
            return;
        }
        instance = this;
        audio.Play();
        DontDestroyOnLoad(this.gameObject);
    }
}
