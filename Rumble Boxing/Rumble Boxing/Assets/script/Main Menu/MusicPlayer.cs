using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    private AudioSource audioscr;
    private float musicVol = 1;

    // Use this for initialization
    void Start () {
        audioscr = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
        audioscr.volume = musicVol;
        
    }

    public void SetVol(float vol)
    {
        musicVol = vol;
    }
}
