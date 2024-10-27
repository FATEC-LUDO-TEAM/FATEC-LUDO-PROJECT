using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
Disclaimer: 
Por enquanto, estou trabalhando com essa quantidade gigantesca de objetos sem ser em array, o que pode complicar o projeto no futuro e não ideal para trabalhar.
Primeiro vou deixar de pé, depois quero ver se consigo consertar isso:

Não é fácil, pois temos que descobrir um jeito de assinalar X a 1, 2, 3, 4 de alguma forma.

*/

public class SoundManager : MonoBehaviour {

	public AudioClip buttonAudioClip;
	public AudioClip dismissalAudioClip;
	public AudioClip diceAudioClip;
	public AudioClip winAudioClip;
	public AudioClip safeHouseAudioClip;
	public AudioClip playerAudioClip;

	public static AudioSource buttonAudioSource;
	public static AudioSource dismissalAudioSource;
	public static AudioSource diceAudioSource;
	public static AudioSource winAudioSource;
	public static AudioSource safeHouseAudioSource;
	public static AudioSource playerAudioSource;

	AudioSource AddAudio(AudioClip clip, bool playOnAwake, bool loop, float  volume)
	{
		AudioSource audioSource = gameObject.AddComponent<AudioSource> ();
		audioSource.clip = clip;
		audioSource.playOnAwake = playOnAwake;
		audioSource.loop = loop;
		audioSource.volume = volume;
		return audioSource;
	}

	void Start () 
	{
		buttonAudioSource = AddAudio (buttonAudioClip,false, false, 1.0f);
		dismissalAudioSource = AddAudio (dismissalAudioClip,false, false, 1.0f);
		diceAudioSource = AddAudio (diceAudioClip,false, false, 1.0f);
		winAudioSource = AddAudio (winAudioClip,false, false, 1.0f);
		safeHouseAudioSource = AddAudio (safeHouseAudioClip,false, false, 1.0f);
		playerAudioSource = AddAudio (playerAudioClip,false, false, 1.0f);
	}	
}