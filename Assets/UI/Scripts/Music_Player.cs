using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music_Player : MonoBehaviour {
	[Tooltip("audioSource defines the Audio Source component in this scene.")]
	AudioSource audioSource;
	[Tooltip("audioTracks defines the audio clips to be played continuously through out the scene.")]
	public AudioClip audioTracks;
	[Space(20)]
	[HeaderAttribute("Music Player Options")]
	[Tooltip("playTracks acts as the Play/Stop function of the Music Player")]
	public bool playTracks;
	[Tooltip("Loops the current _audioTracks clip.")]
	public bool loopTrack;
	[Space(20)]
	[HeaderAttribute("Debugging/ReadOnly")]
	[Tooltip("_isMute returns the status of muting function in the Sound_Controller.")]
	public bool isMute = false;
	
	void Awake () {		
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = audioTracks;
        //Checks PlayerPrefs to see if the player has requested for the audio to be muted, and updates all the necessary variables 
        if (PlayerPrefs.HasKey("_Mute"))
        {
			int value = PlayerPrefs.GetInt("_Mute");
			if(value == 0){isMute = false;}
			if(value == 1){isMute = true;}
		}
        else
        {
			isMute = false;
			PlayerPrefs.SetInt("_Mute", 0);
		}
		if( isMute )
            audioSource.mute = true;
        else
        {
            audioSource.mute = false;
            audioSource.Play();
        }
	}
	
	void Update () {
		if(!playTracks)
            audioSource.Stop();
		if(playTracks && !audioSource.isPlaying)
            StartPlayer();		
		if(loopTrack)
            audioSource.loop = true;
        else
            audioSource.loop = false;
		
		//Checks PlayerPrefs to see if the audio has been muted.
		int value = PlayerPrefs.GetInt("_Mute");
		if(value == 0)
            isMute = false;
		if(value == 1)
            isMute = true;
        if ( isMute )
            audioSource.mute = true;
        else
            audioSource.mute = false;
	}
	
	public void StartPlayer(){

        audioSource.Play();
	}
}
