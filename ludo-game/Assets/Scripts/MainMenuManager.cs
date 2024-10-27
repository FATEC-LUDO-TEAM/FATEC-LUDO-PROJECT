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

public class MainMenuManager : MonoBehaviour{

    
public static int howManyPlayers;

void Start(){
    //Posicao Inicial do Jogo no primeiro Frame_.
        //Sincronizar o jogo com a tela
        QualitySettings.vSyncCount = 1;
        //Estamos travando a 30 Frame_s por segundo
        Application.targetFrameRate = 30;

}

//Posicao Alterada a cada Frame_. 30 fps, 30 Frame_s por segundo. Nós podemos alterar esse comportamento.
void Update(){        

        
}

public void two_player()
	{
		SoundManager.buttonAudioSource.Play ();
		howManyPlayers = 2;
		SceneManager.LoadScene ("Ludo");
	}

	public void three_player()
	{
		SoundManager.buttonAudioSource.Play ();
		howManyPlayers = 3;
		SceneManager.LoadScene ("Ludo");
	}

	public void four_player()
	{
		SoundManager.buttonAudioSource.Play ();
		howManyPlayers = 4;
		SceneManager.LoadScene ("Ludo");
	}

	public void quit()
	{
		SoundManager.buttonAudioSource.Play ();
		Application.Quit ();
	}
}

