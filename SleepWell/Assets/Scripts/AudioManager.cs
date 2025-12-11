using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

        public AudioSource dayMusic;
		public AudioSource nightMusic;
		public AudioSource menuMusic;
		public AudioSource audioSource;
        public static float stopTimestamp = 0f;
       //12.5f;

	void Start()
	{
		string sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "MainMenu" || sceneName == "Credits" || sceneName == "EndWin")
		{
			audioSource = menuMusic;
			PlayMusicAtTime(stopTimestamp);
		} 

		else if (sceneName == "EndLose" || sceneName == "EndLose_NoStamina")
		{
			audioSource = nightMusic;
			PlayMusicAtTime(stopTimestamp);
		} 

		else if (GameHandler_CycleStamina.isNight == false)
		{
			audioSource = dayMusic;
			PlayMusicAtTime(stopTimestamp);
		} 
		else
		{
			audioSource = nightMusic;
			PlayMusicAtTime(stopTimestamp);
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

		public void GetTimeStamp()
	{
		stopTimestamp = audioSource.time;
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