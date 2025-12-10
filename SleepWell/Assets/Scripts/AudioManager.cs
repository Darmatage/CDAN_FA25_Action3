using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioInterrupt : MonoBehaviour {

        public AudioSource dayMusic;
		public AudioSource nightMusic;
		public AudioSource menuMusic;
		private AudioSource audioSource;
        private float stopTimestamp = 12.5f;
       
	void Start()
	{
		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "MainMenu")
		{
			audioSource = menuMusic;
			PlayMusicAtBegin();
		} 
		
	}

        void Update(){
			/*
                if (Input.GetKeyDown("i")) {
                        PlayMusicAtBegin();
                }
                if (Input.GetKeyDown("o")) {
                        StopMusic();
                }
                if (Input.GetKeyDown("p")) {
                        PlayMusicAtTime(stopTimestamp);
                }
				*/
        }

	public void PlayDayMusic()
	{
		StopMusic();
		audioSource = dayMusic;
		PlayMusicAtTime(stopTimestamp);
	}

	public void PlayNightMusic()
	{
		StopMusic();
		audioSource = nightMusic;
		PlayMusicAtTime(stopTimestamp);
	}


        public void PlayMusicAtBegin(){
                audioSource.time = 0.0f;
                audioSource.Play();
        }

        public void StopMusic(){
                stopTimestamp = audioSource.time;
                Debug.Log("Stopped audio at: " + stopTimestamp);
                audioSource.Stop();
        }

        public void PlayMusicAtTime(float timeStamp){
                if (timeStamp > audioSource.clip.length){
                        return;
                } else {
                        audioSource.time = timeStamp;
                        audioSource.Play();
                }
        }
} 