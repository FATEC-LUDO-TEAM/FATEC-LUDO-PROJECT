using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq.Expressions;

public class GameScriptMultiplayer : MonoBehaviourPunCallbacks
{
    private int totalRedInHouse, totalGreenInHouse;

    public GameObject frameRed, frameGreen;

    public GameObject redPlayerI_Border, redPlayerII_Border, redPlayerIII_Border, redPlayerIV_Border;
    public GameObject greenPlayerI_Border, greenPlayerII_Border, greenPlayerIII_Border, greenPlayerIV_Border;

    public Vector3 redPlayerI_Pos, redPlayerII_Pos, redPlayerIII_Pos, redPlayerIV_Pos;
    public Vector3 greenPlayerI_Pos, greenPlayerII_Pos, greenPlayerIII_Pos, greenPlayerIV_Pos;

    public Button RedPlayerI_Button, RedPlayerII_Button, RedPlayerIII_Button, RedPlayerIV_Button;
    public Button GreenPlayerI_Button, GreenPlayerII_Button, GreenPlayerIII_Button, GreenPlayerIV_Button;

    public GameObject greenScreen, redScreen;
    public Text greenRankText, redRankText;

    private string playerTurn = "RED";
    public Transform diceRoll;
    public Button DiceRollButton;

	private int selectDiceNumAnimation;

    public Transform redDiceRollPos, greenDiceRollPos;

    private string currentPlayer = "none";
    private string currentPlayerName = "none";

	public GameObject dice1_Roll_Animation;
	public GameObject dice2_Roll_Animation;
	public GameObject dice3_Roll_Animation;
	public GameObject dice4_Roll_Animation;
	public GameObject dice5_Roll_Animation;
	public GameObject dice6_Roll_Animation;

	public List<GameObject> redMovementBlocks = new List<GameObject>();
	public List<GameObject> greenMovementBlocks = new List<GameObject>();
	
    public GameObject redPlayerI, redPlayerII, redPlayerIII, redPlayerIV;
    public GameObject greenPlayerI, greenPlayerII, greenPlayerIII, greenPlayerIV;

    private int redPlayerI_Steps, redPlayerII_Steps, redPlayerIII_Steps, redPlayerIV_Steps;
    private int greenPlayerI_Steps, greenPlayerII_Steps, greenPlayerIII_Steps, greenPlayerIV_Steps;

    private System.Random randomNo;
    public GameObject confirmScreen;
    public GameObject gameCompletedScreen;

	public void yesGameCompleted()
	{
		SoundManagerScript.buttonAudioSource.Play ();
		SceneManager.LoadScene ("Ludo");
	}

	public void noGameCompleted()
	{
		SoundManagerScript.buttonAudioSource.Play ();
		SceneManager.LoadScene ("Main Menu");
	}

	public void yesMethod()
	{

		SoundManagerScript.buttonAudioSource.Play ();
		SceneManager.LoadScene ("Main Menu");
	}

	public void noMethod()
	{
		SoundManagerScript.buttonAudioSource.Play ();
		confirmScreen.SetActive (false);
	}

	public void ExitMethod()
	{

		SoundManagerScript.buttonAudioSource.Play ();
		confirmScreen.SetActive (true);
	}
	// -============== GAME COMPLETED ROUTINE ==========================================================

	[PunRPC]
	void StartGameCompletedRoutine()
{
    StartCoroutine("GameCompletedRoutine");
}

	IEnumerator GameCompletedRoutine()
	{
		yield return new WaitForSeconds (1.5f);
		gameCompletedScreen.SetActive (true);
	}

    [PunRPC]
    void InitializeGame()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;

        randomNo = new System.Random();

        dice1_Roll_Animation.SetActive(false);
        dice2_Roll_Animation.SetActive(false);
        dice3_Roll_Animation.SetActive(false);
        dice4_Roll_Animation.SetActive(false);
        dice5_Roll_Animation.SetActive(false);
        dice6_Roll_Animation.SetActive(false);

        redPlayerI_Pos = redPlayerI.transform.position;
        redPlayerII_Pos = redPlayerII.transform.position;
        redPlayerIII_Pos = redPlayerIII.transform.position;
        redPlayerIV_Pos = redPlayerIV.transform.position;

        greenPlayerI_Pos = greenPlayerI.transform.position;
        greenPlayerII_Pos = greenPlayerII.transform.position;
        greenPlayerIII_Pos = greenPlayerIII.transform.position;
        greenPlayerIV_Pos = greenPlayerIV.transform.position;

        redPlayerI_Border.SetActive(false);
        redPlayerII_Border.SetActive(false);
        redPlayerIII_Border.SetActive(false);
        redPlayerIV_Border.SetActive(false);

        greenPlayerI_Border.SetActive(false);
        greenPlayerII_Border.SetActive(false);
        greenPlayerIII_Border.SetActive(false);
        greenPlayerIV_Border.SetActive(false);

        redScreen.SetActive(false);
        greenScreen.SetActive(false);

        playerTurn = "RED";

        photonView.RPC("SyncGameState", RpcTarget.OthersBuffered);
    }

    [PunRPC]
    void SyncGameState()
    {
        if (playerTurn == "RED")
        {
            frameRed.SetActive(true);
            frameGreen.SetActive(false);
        }
        else if (playerTurn == "GREEN")
        {
            frameRed.SetActive(false);
            frameGreen.SetActive(true);
        }
    }

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            photonView.RPC("InitializeGame", RpcTarget.AllBuffered);
        }
        else
    	{
        Debug.Log("Conexão com o servidor perdida. Tentando reconectar... Voltando ao Menu");
		//SceneManager.LoadScene("CoreMenu");
    	}
    }


//▒█▀▀▄ ░▀░ █▀▀ █▀▀ 　 ▒█▀▀█ █▀▀█ █░░ █░░ 
//▒█░▒█ ▀█▀ █░░ █▀▀ 　 ▒█▄▄▀ █░░█ █░░ █░░ 
//▒█▄▄▀ ▀▀▀ ▀▀▀ ▀▀▀ 　 ▒█░▒█ ▀▀▀▀ ▀▀▀ ▀▀▀ 

    
    public void DiceRoll()

    {
        if (playerTurn == "RED" && PhotonNetwork.IsMasterClient || playerTurn == "GREEN" && !PhotonNetwork.IsMasterClient)
        {
			Debug.Log("O jogo entendeu que é seu turno e vc pode rodar");
            selectDiceNumAnimation = randomNo.Next(1, 7);
			selectDiceNumAnimation = 6;
            photonView.RPC("DiceRollResult", RpcTarget.All, selectDiceNumAnimation);
            ExecutePlayersNotInitialized();
        }
        else
        {
            Debug.Log("Not your turn to roll the dice.");
        }
    }

    [PunRPC]
    void DiceRollResult(int result)
    {
        DiceRollButton.interactable = false;
		selectDiceNumAnimation = result;
        switch (selectDiceNumAnimation)
        {
            case 1:
                dice1_Roll_Animation.SetActive(true);
                dice2_Roll_Animation.SetActive (false);
				dice3_Roll_Animation.SetActive (false);
				dice4_Roll_Animation.SetActive (false);
				dice5_Roll_Animation.SetActive (false);
				dice6_Roll_Animation.SetActive (false);
                break;
            case 2:
                dice1_Roll_Animation.SetActive (false);
				dice2_Roll_Animation.SetActive (true);
				dice3_Roll_Animation.SetActive (false);
				dice4_Roll_Animation.SetActive (false);
				dice5_Roll_Animation.SetActive (false);
				dice6_Roll_Animation.SetActive (false);
                break;
            case 3:
                dice1_Roll_Animation.SetActive (false);
				dice2_Roll_Animation.SetActive (false);
				dice3_Roll_Animation.SetActive (true);
				dice4_Roll_Animation.SetActive (false);
				dice5_Roll_Animation.SetActive (false);
				dice6_Roll_Animation.SetActive (false);
                break;
            case 4:
                dice1_Roll_Animation.SetActive (false);
				dice2_Roll_Animation.SetActive (false);
				dice3_Roll_Animation.SetActive (false);
				dice4_Roll_Animation.SetActive (true);
				dice5_Roll_Animation.SetActive (false);
				dice6_Roll_Animation.SetActive (false);
                break;
            case 5:
                dice1_Roll_Animation.SetActive (false);
				dice2_Roll_Animation.SetActive (false);
				dice3_Roll_Animation.SetActive (false);
				dice4_Roll_Animation.SetActive (false);
				dice5_Roll_Animation.SetActive (true);
				dice6_Roll_Animation.SetActive (false);
                break;
            case 6:
                dice1_Roll_Animation.SetActive (false);
				dice2_Roll_Animation.SetActive (false);
				dice3_Roll_Animation.SetActive (false);
				dice4_Roll_Animation.SetActive (false);
				dice5_Roll_Animation.SetActive (false);
				dice6_Roll_Animation.SetActive (true);
                break;
        }
       
	   
    }
//▒█▀▀█ █░░ █▀▀█ █░░█ █▀▀ █▀▀█ █▀▀ 　 █▀▀▄ █▀▀█ ▀▀█▀▀ 　 
//▒█▄▄█ █░░ █▄▄█ █▄▄█ █▀▀ █▄▄▀ ▀▀█ 　 █░░█ █░░█ ░░█░░ 　 
//▒█░░░ ▀▀▀ ▀░░▀ ▄▄▄█ ▀▀▀ ▀░▀▀ ▀▀▀ 　 ▀░░▀ ▀▀▀▀ ░░▀░░ 　 

//▀█▀ █▀▀▄ ░▀░ ▀▀█▀▀ ░▀░ █▀▀█ █░░ ░▀░ ▀▀█ █▀▀ █▀▀▄ 
//▒█░ █░░█ ▀█▀ ░░█░░ ▀█▀ █▄▄█ █░░ ▀█▀ ▄▀░ █▀▀ █░░█ 
//▄█▄ ▀░░▀ ▀▀▀ ░░▀░░ ▀▀▀ ▀░░▀ ▀▀▀ ▀▀▀ ▀▀▀ ▀▀▀ ▀▀▀░ 


    void ExecutePlayersNotInitialized()
{
    AtivarBordasJogador();

    string turncolor = playerTurn;
    if (DeveMudarTurno())
    	{
		Debug.Log("DeveMudarTurno retornou TRUE: Mudando o turno.");
        playerTurn = (playerTurn == "RED") ? "GREEN" : "RED";
    	}
		else{
			Debug.Log("DeveMudarTurno retornou FALSE: Mantendo o turno atual.");
		}
		Debug.Log("RedPlayerI_Border ativo: " + redPlayerI_Border.activeInHierarchy + " | RedPlayerI_Button interagível: " + RedPlayerI_Button.interactable);
     photonView.RPC("SyncPlayersState", RpcTarget.Others,
        playerTurn, selectDiceNumAnimation,
        redPlayerI_Border.activeInHierarchy, RedPlayerI_Button.interactable,
        redPlayerII_Border.activeInHierarchy, RedPlayerII_Button.interactable,
        redPlayerIII_Border.activeInHierarchy, RedPlayerIII_Button.interactable,
        redPlayerIV_Border.activeInHierarchy, RedPlayerIV_Button.interactable,
        greenPlayerI_Border.activeInHierarchy, GreenPlayerI_Button.interactable,
        greenPlayerII_Border.activeInHierarchy, GreenPlayerII_Button.interactable,
        greenPlayerIII_Border.activeInHierarchy, GreenPlayerIII_Button.interactable,
        greenPlayerIV_Border.activeInHierarchy, GreenPlayerIV_Button.interactable);
     
     if (turncolor != playerTurn){
        photonView.RPC("SyncGameState", RpcTarget.All);
        InitializeDice();
     }
     Debug.Log("Player Turn" + playerTurn);
     
}
 


	private void AtivarBordasJogador()
{
    bool isPlayerRed = playerTurn == "RED";
    int stepsThreshold = selectDiceNumAnimation;
    
    // Se for RED, ativa as bordas dos jogadores vermelhos conforme os passos
    if (isPlayerRed)
    {
        RedPlayerI_Button.interactable = AtivarBordaSePossivel(redPlayerI_Steps, redPlayerI_Border, stepsThreshold, redMovementBlocks);
        RedPlayerII_Button.interactable = AtivarBordaSePossivel(redPlayerII_Steps, redPlayerII_Border, stepsThreshold, redMovementBlocks);
        RedPlayerIII_Button.interactable = AtivarBordaSePossivel(redPlayerIII_Steps, redPlayerIII_Border, stepsThreshold, redMovementBlocks);
        RedPlayerIV_Button.interactable = AtivarBordaSePossivel(redPlayerIV_Steps, redPlayerIV_Border, stepsThreshold, redMovementBlocks);
    }
    else // Se for GREEN, ativa as bordas dos jogadores verdes conforme os passos
    {
        GreenPlayerI_Button.interactable = AtivarBordaSePossivel(greenPlayerI_Steps, greenPlayerI_Border, stepsThreshold, greenMovementBlocks);
        GreenPlayerII_Button.interactable = AtivarBordaSePossivel(greenPlayerII_Steps, greenPlayerII_Border, stepsThreshold, greenMovementBlocks);
        GreenPlayerIII_Button.interactable = AtivarBordaSePossivel(greenPlayerIII_Steps, greenPlayerIII_Border, stepsThreshold, greenMovementBlocks);
        GreenPlayerIV_Button.interactable = AtivarBordaSePossivel(greenPlayerIV_Steps, greenPlayerIV_Border, stepsThreshold, greenMovementBlocks);
    }
}

private bool AtivarBordaSePossivel(int playerSteps, GameObject playerBorder, int stepsThreshold, List<GameObject> movementBlocks)
{
    if ((movementBlocks.Count - playerSteps) >= stepsThreshold && playerSteps > 0 && (movementBlocks.Count > playerSteps))
	{
    playerBorder.SetActive(true);
    return true; // O botão do jogador pode ser interagido
	}
	else if (stepsThreshold == 6 && playerSteps == 0)
	{
    playerBorder.SetActive(true);
    return true;
	}
	else
	{
    playerBorder.SetActive(false);
    return false;
	}	

}
   



private bool DeveMudarTurno()
{
    bool isRedTurn = playerTurn == "RED";

    // Verifica se todas as bordas dos jogadores estão desativadas para o turno atual
    if (isRedTurn)
    {
        return !redPlayerI_Border.activeInHierarchy &&
               !redPlayerII_Border.activeInHierarchy &&
               !redPlayerIII_Border.activeInHierarchy &&
               !redPlayerIV_Border.activeInHierarchy;
    }
    else
    {
        return !greenPlayerI_Border.activeInHierarchy &&
               !greenPlayerII_Border.activeInHierarchy &&
               !greenPlayerIII_Border.activeInHierarchy &&
               !greenPlayerIV_Border.activeInHierarchy;
    }
} 

[PunRPC]
void SyncPlayersState(string turn, int diceValue,
    bool redPlayerIBorderActive, bool redPlayerIButtonInteractable,
    bool redPlayerIIBorderActive, bool redPlayerIIButtonInteractable,
    bool redPlayerIIIBorderActive, bool redPlayerIIIButtonInteractable,
    bool redPlayerIVBorderActive, bool redPlayerIVButtonInteractable,
    bool greenPlayerIBorderActive, bool greenPlayerIButtonInteractable,
    bool greenPlayerIIBorderActive, bool greenPlayerIIButtonInteractable,
    bool greenPlayerIIIBorderActive, bool greenPlayerIIIButtonInteractable,
    bool greenPlayerIVBorderActive, bool greenPlayerIVButtonInteractable)
{
    playerTurn = turn;
    selectDiceNumAnimation = diceValue;
    Debug.Log("    selectDiceNumAnimation =  "  + selectDiceNumAnimation.ToString());

    // Sincronize os estados de borda e botão
    redPlayerI_Border.SetActive(redPlayerIBorderActive);
    RedPlayerI_Button.interactable = redPlayerIButtonInteractable;

    redPlayerII_Border.SetActive(redPlayerIIBorderActive);
    RedPlayerII_Button.interactable = redPlayerIIButtonInteractable;

     redPlayerIII_Border.SetActive(redPlayerIIIBorderActive);
    RedPlayerIII_Button.interactable = redPlayerIIIButtonInteractable;

    redPlayerIV_Border.SetActive(redPlayerIVBorderActive);
    RedPlayerIV_Button.interactable = redPlayerIVButtonInteractable;

    greenPlayerI_Border.SetActive(greenPlayerIBorderActive);
    GreenPlayerI_Button.interactable = greenPlayerIButtonInteractable;

    greenPlayerII_Border.SetActive(greenPlayerIIBorderActive);
    GreenPlayerII_Button.interactable = greenPlayerIIButtonInteractable;

    greenPlayerIII_Border.SetActive(greenPlayerIIIBorderActive);
    GreenPlayerIII_Button.interactable = greenPlayerIIIButtonInteractable;

    greenPlayerIV_Border.SetActive(greenPlayerIVBorderActive);
    GreenPlayerIV_Button.interactable = greenPlayerIVButtonInteractable;

   
}





	//▀█▀ █▀▀▄ ░▀░ ▀▀█▀▀ ░▀░ █▀▀█ █░░ ░▀░ ▀▀█ █▀▀ 　 ▒█▀▀▄ ░▀░ █▀▀ █▀▀ 
	//▒█░ █░░█ ▀█▀ ░░█░░ ▀█▀ █▄▄█ █░░ ▀█▀ ▄▀░ █▀▀ 　 ▒█░▒█ ▀█▀ █░░ █▀▀ 
	//▄█▄ ▀░░▀ ▀▀▀ ░░▀░░ ▀▀▀ ▀░░▀ ▀▀▀ ▀▀▀ ▀▀▀ ▀▀▀ 　 ▒█▄▄▀ ▀▀▀ ▀▀▀ ▀▀▀ 

void InitializeDice()
{
    DiceRollButton.interactable = true;
	DesativarInteracaoPecas(); 

	    

    ConfigurarPosicaoDado();

	VerificarUltrapassagem();

    // Sincroniza o estado entre as máquinas para garantir o frame correto em volta das casinhas

	bool vitoria = VerificarCondicaoVitoria();
    photonView.RPC("SyncDiceState", RpcTarget.Others, playerTurn, selectDiceNumAnimation, vitoria, 
    redPlayerI_Steps, redPlayerII_Steps, redPlayerIII_Steps, redPlayerIV_Steps, 
    greenPlayerI_Steps, greenPlayerII_Steps, greenPlayerIII_Steps, greenPlayerIV_Steps, 
    redPlayerI.transform.position, redPlayerII.transform.position, redPlayerIII.transform.position, redPlayerIV.transform.position,
    greenPlayerI.transform.position, greenPlayerII.transform.position, greenPlayerIII.transform.position, greenPlayerIV.transform.position);

    // Verifica se houve vitória antes de preparar o próximo turno
  
}

private void ConfigurarPosicaoDado()
{
    dice1_Roll_Animation.SetActive(false);
    dice2_Roll_Animation.SetActive(false);
    dice3_Roll_Animation.SetActive(false);
    dice4_Roll_Animation.SetActive(false);
    dice5_Roll_Animation.SetActive(false);
    dice6_Roll_Animation.SetActive(false);

//se playerturn é Red, redposition, se não green
    diceRoll.position = (playerTurn == "RED") ? redDiceRollPos.position : greenDiceRollPos.position;

	

}

private bool VerificarCondicaoVitoria()
{
    if (playerTurn == "RED" && totalRedInHouse > 3)
    {
        redScreen.SetActive(true);
        photonView.RPC("StartGameCompletedRoutine", RpcTarget.All);
        playerTurn = "NONE";
        return true;
    }
    else if (playerTurn == "GREEN" && totalGreenInHouse > 3)
    {
        greenScreen.SetActive(true);
        photonView.RPC("StartGameCompletedRoutine", RpcTarget.All);
        playerTurn = "NONE";
        return true;
    }
    return false;
}


private void VerificarUltrapassagem()
{
    if (currentPlayerName.Contains("RED PLAYER"))
    {
        // Verifica se o jogador vermelho ultrapassou alguma peça verde
        if (currentPlayer == GreenPlayerI_Script_Multiplayer.greenPlayerI_ColName && (currentPlayer != "Star" && GreenPlayerI_Script_Multiplayer.greenPlayerI_ColName != "Star"))
        {
            ProcessarUltrapassagem(greenPlayerI, ref greenPlayerI_Steps, greenPlayerI_Pos, "RED");
        }
        if (currentPlayer == GreenPlayerII_Script_Multiplayer.greenPlayerII_ColName && (currentPlayer != "Star" && GreenPlayerII_Script_Multiplayer.greenPlayerII_ColName != "Star"))
        {
            ProcessarUltrapassagem(greenPlayerII, ref greenPlayerII_Steps, greenPlayerII_Pos, "RED");
        }
        if (currentPlayer == GreenPlayerIII_Script_Multiplayer.greenPlayerIII_ColName && (currentPlayer != "Star" && GreenPlayerIII_Script_Multiplayer.greenPlayerIII_ColName != "Star"))
        {
            ProcessarUltrapassagem(greenPlayerIII, ref greenPlayerIII_Steps, greenPlayerIII_Pos, "RED");
        }
        if (currentPlayer == GreenPlayerIV_Script_Multiplayer.greenPlayerIV_ColName && (currentPlayer != "Star" && GreenPlayerIV_Script_Multiplayer.greenPlayerIV_ColName != "Star"))
        {
            ProcessarUltrapassagem(greenPlayerIV, ref greenPlayerIV_Steps, greenPlayerIV_Pos, "RED");
        }
    }
    else if (currentPlayerName.Contains("GREEN PLAYER"))
    {
        // Verifica se o jogador verde ultrapassou alguma peça vermelha
        if (currentPlayer == RedPlayerI_Script_Multiplayer.redPlayerI_ColName && (currentPlayer != "Star" && RedPlayerI_Script_Multiplayer.redPlayerI_ColName != "Star"))
        {
            ProcessarUltrapassagem(redPlayerI, ref redPlayerI_Steps, redPlayerI_Pos, "GREEN");
        }
        if (currentPlayer == RedPlayerII_Script_Multiplayer.redPlayerII_ColName && (currentPlayer != "Star" && RedPlayerII_Script_Multiplayer.redPlayerII_ColName != "Star"))
        {
            ProcessarUltrapassagem(redPlayerII, ref redPlayerII_Steps, redPlayerII_Pos, "GREEN");
        }
        if (currentPlayer == RedPlayerIII_Script_Multiplayer.redPlayerIII_ColName && (currentPlayer != "Star" && RedPlayerIII_Script_Multiplayer.redPlayerIII_ColName != "Star"))
        {
            ProcessarUltrapassagem(redPlayerIII, ref redPlayerIII_Steps, redPlayerIII_Pos, "GREEN");
        }
        if (currentPlayer == RedPlayerIV_Script_Multiplayer.redPlayerIV_ColName && (currentPlayer != "Star" && RedPlayerIV_Script_Multiplayer.redPlayerIV_ColName != "Star"))
        {
            ProcessarUltrapassagem(redPlayerIV, ref redPlayerIV_Steps, redPlayerIV_Pos, "GREEN");
        }
    }
}

private void ProcessarUltrapassagem(GameObject player, ref int playerSteps, Vector3 playerPosInicial, string playerColor)
{
    SoundManagerScript.dismissalAudioSource.Play();
    player.transform.position = playerPosInicial;
    playerSteps = 0;
    playerTurn = playerColor;

}

[PunRPC]
void SyncDiceState(string newTurn, int diceValue, bool vitoria, 
    int redPlayerI_Steps, int redPlayerII_Steps, int redPlayerIII_Steps, int redPlayerIV_Steps,
    int greenPlayerI_Steps, int greenPlayerII_Steps, int greenPlayerIII_Steps, int greenPlayerIV_Steps,
    Vector3 redPlayer1_Pos, Vector3 redPlayer2_Pos, Vector3 redPlayer3_Pos, Vector3 redPlayer4_Pos,
    Vector3 greenPlayer1_Pos, Vector3 greenPlayer2_Pos, Vector3 greenPlayer3_Pos, Vector3 greenPlayer4_Pos)
{
    // Atualiza o turno, valor do dado e os passos das peças
    playerTurn = newTurn;
    selectDiceNumAnimation = diceValue;
    this.redPlayerI_Steps = redPlayerI_Steps;
    this.redPlayerII_Steps = redPlayerII_Steps;
    this.redPlayerIII_Steps = redPlayerIII_Steps;
    this.redPlayerIV_Steps = redPlayerIV_Steps;
    this.greenPlayerI_Steps = greenPlayerI_Steps;
    this.greenPlayerII_Steps = greenPlayerII_Steps;
    this.greenPlayerIII_Steps = greenPlayerIII_Steps;
    this.greenPlayerIV_Steps = greenPlayerIV_Steps;

    // Atualiza as posições das peças
    redPlayerI.transform.position = redPlayer1_Pos;
    redPlayerII.transform.position = redPlayer2_Pos;
    redPlayerIII.transform.position = redPlayer3_Pos;
    redPlayerIV.transform.position = redPlayer4_Pos;
    
    greenPlayerI.transform.position = greenPlayer1_Pos;
    greenPlayerII.transform.position = greenPlayer2_Pos;
    greenPlayerIII.transform.position = greenPlayer3_Pos;
    greenPlayerIV.transform.position = greenPlayer4_Pos;

    if (vitoria)
    {
        // Lógica de final de jogo se alguém venceu
       SceneManager.LoadScene("CoreMenu");
    }
    else
    {
       Debug.Log("segura aí que não acabou ainda ");
    }
}


private void DesativarInteracaoPecas()
{
    GreenPlayerI_Button.interactable = false;
    GreenPlayerII_Button.interactable = false;
    GreenPlayerIII_Button.interactable = false;
    GreenPlayerIV_Button.interactable = false;
    greenPlayerI_Border.SetActive(false);
    greenPlayerII_Border.SetActive(false);
    greenPlayerIII_Border.SetActive(false);
    greenPlayerIV_Border.SetActive(false);

    RedPlayerI_Button.interactable = false;
    RedPlayerII_Button.interactable = false;
    RedPlayerIII_Button.interactable = false;
    RedPlayerIV_Button.interactable = false;
    redPlayerI_Border.SetActive(false);
    redPlayerII_Border.SetActive(false);
    redPlayerIII_Border.SetActive(false);
    redPlayerIV_Border.SetActive(false);
}


//▒█▀▄▀█ █▀▀█ ▀█░█▀ ░▀░ █▀▄▀█ █▀▀ █▀▀▄ ▀▀█▀▀ █▀▀█ █▀▀ 
//▒█▒█▒█ █░░█ ░█▄█░ ▀█▀ █░▀░█ █▀▀ █░░█ ░░█░░ █░░█ ▀▀█ 
//▒█░░▒█ ▀▀▀▀ ░░▀░░ ▀▀▀ ▀░░░▀ ▀▀▀ ▀░░▀ ░░▀░░ ▀▀▀▀ ▀▀▀ 

//█▀▀▄ █▀▀█ █▀▀ 　 █▀▀█ █▀▀ █▀▀ █▀▀█ █▀▀ 
//█░░█ █▄▄█ ▀▀█ 　 █░░█ █▀▀ █░░ █▄▄█ ▀▀█ 
//▀▀▀░ ▀░░▀ ▀▀▀ 　 █▀▀▀ ▀▀▀ ▀▀▀ ▀░░▀ ▀▀▀ 




public void redPlayerI_UI()
{
	Debug.Log("Chegou em redPlayerI_UI.");

    if (playerTurn  == "RED")
    {

        photonView.RPC("MoveRedPlayerI", RpcTarget.All);
    }
}
 
 public void redPlayerII_UI()
{
	Debug.Log("Chegou em redPlayerII_UI.");
    if (playerTurn  == "RED")
    {
        photonView.RPC("MoveRedPlayerII", RpcTarget.All);
    }
}

public void redPlayerIII_UI()
{
	Debug.Log("Chegou em redPlayerIII_UI.");
    if (playerTurn  == "RED")
    {
        photonView.RPC("MoveRedPlayerIII", RpcTarget.All);
    }
}

public void redPlayerIV_UI()
{	Debug.Log("Chegou em redPlayerIV_UI.");
    if (playerTurn  == "RED")
    {
        photonView.RPC("MoveRedPlayerIV", RpcTarget.All);
    }
}


public void greenPlayerI_UI()
{
    Debug.Log("Tentou clickar né safado");
    if (playerTurn  == "GREEN")
    {
        photonView.RPC("MoveGreenPlayerI", RpcTarget.All);
    }
}

public void greenPlayerII_UI()
{
    Debug.Log("Tentou clickar né safado");
    if (playerTurn  == "GREEN")
    {
        photonView.RPC("MoveGreenPlayerII", RpcTarget.All);
    }
}

public void greenPlayerIII_UI()
{
    Debug.Log("Tentou clickar né safado");
    if (playerTurn  == "GREEN")
    {
        photonView.RPC("MoveGreenPlayerIII", RpcTarget.All);
    }
}


public void greenPlayerIV_UI()
{
    Debug.Log("Tentou clickar né safado");
    if (playerTurn  == "GREEN")
    {
        photonView.RPC("MoveGreenPlayerIV", RpcTarget.All);
    }
}

[PunRPC]
void MoveRedPlayerI()
{
    currentPlayerName = "RED PLAYER I";
    if (playerTurn != "RED") return;
    // Desativa bordas e botões antes de iniciar o movimento
    DesativarInteracaoPecas();
	
    // Verifica se o movimento é possível para RedPlayerI com base nos passos e condições
    if (VerificarMovimentoPossivel(redPlayerI_Steps, selectDiceNumAnimation, redMovementBlocks))
    {
        // Executa o movimento

        StartCoroutine(MoverPeca(redPlayerI, redPlayerI_Steps, selectDiceNumAnimation, redMovementBlocks, RedPlayerI_Button, playerTurn, 
    	(updatedSteps, updatedTurn) => {
        redPlayerI_Steps = updatedSteps;
        playerTurn = updatedTurn;
    	}));
	}
    else
    {
        if(redPlayerII_Steps + redPlayerIII_Steps + redPlayerIV_Steps == 0 && selectDiceNumAnimation != 6){
            TrocarTurno();
            photonView.RPC("InitializeDice", RpcTarget.All);
        }
        else{
            return;
        }
    }
}



[PunRPC]
void MoveRedPlayerII()
{
    currentPlayerName = "RED PLAYER II";
    if (playerTurn != "RED") return;
    // Desativa bordas e botões antes de iniciar o movimento
    DesativarInteracaoPecas();

    // Verifica se o movimento é possível para RedPlayerI com base nos passos e condições
    if (VerificarMovimentoPossivel(redPlayerII_Steps, selectDiceNumAnimation, redMovementBlocks))
    {
        // Executa o movimento
	
        StartCoroutine(MoverPeca(redPlayerII, redPlayerII_Steps, selectDiceNumAnimation, redMovementBlocks, RedPlayerII_Button, playerTurn, 
    	(updatedSteps, updatedTurn) => {
        redPlayerII_Steps = updatedSteps;
        playerTurn = updatedTurn;
    	}));
    }
    else
    {
        if(redPlayerI_Steps + redPlayerIII_Steps + redPlayerIV_Steps == 0 && selectDiceNumAnimation != 6){
            TrocarTurno();
            photonView.RPC("InitializeDice", RpcTarget.All);
        }
        else{
            return;
        }
    }
}

[PunRPC]
void MoveRedPlayerIII()
{
    currentPlayerName = "RED PLAYER III";
    if (playerTurn != "RED") return;
    // Desativa bordas e botões antes de iniciar o movimento
    DesativarInteracaoPecas();

    // Verifica se o movimento é possível para RedPlayerI com base nos passos e condições
    if (VerificarMovimentoPossivel(redPlayerIII_Steps, selectDiceNumAnimation, redMovementBlocks))
    {
        // Executa o movimento
	
        StartCoroutine(MoverPeca(redPlayerIII, redPlayerIII_Steps, selectDiceNumAnimation, redMovementBlocks, RedPlayerIII_Button, playerTurn, 
    	(updatedSteps, updatedTurn) => {
        redPlayerIII_Steps = updatedSteps;
        playerTurn = updatedTurn;
    	}));

    }
    else
    {
        if(redPlayerI_Steps + redPlayerII_Steps + redPlayerIV_Steps == 0 && selectDiceNumAnimation != 6){
            TrocarTurno();
            photonView.RPC("InitializeDice", RpcTarget.All);
        }
        else{
            return;
        }
    }
}

[PunRPC]
void MoveRedPlayerIV()
{
    currentPlayerName = "RED PLAYER I";
    if (playerTurn != "RED") return;
    // Desativa bordas e botões antes de iniciar o movimento
    DesativarInteracaoPecas();

    // Verifica se o movimento é possível para RedPlayerI com base nos passos e condições
    if (VerificarMovimentoPossivel(redPlayerI_Steps, selectDiceNumAnimation, redMovementBlocks))
    {
        // Executa o movimento
		
         StartCoroutine(MoverPeca(redPlayerIV, redPlayerIV_Steps, selectDiceNumAnimation, redMovementBlocks, RedPlayerIV_Button, playerTurn, 
    	(updatedSteps, updatedTurn) => {
        redPlayerIV_Steps = updatedSteps;
        playerTurn = updatedTurn;
    	}));

    }
    else
    {
        if(redPlayerI_Steps + redPlayerII_Steps + redPlayerIII_Steps == 0 && selectDiceNumAnimation != 6){
            TrocarTurno();
            photonView.RPC("InitializeDice", RpcTarget.All);
        }
        else{
            return;
        }
    }
}



[PunRPC]
void MoveGreenPlayerI()
{
    currentPlayerName = "GREEN PLAYER I";
    if (playerTurn != "GREEN") return;
    // Desativa bordas e botões antes de iniciar o movimento
    DesativarInteracaoPecas();

    // Verifica se o movimento é possível para GreenPlayerI com base nos passos e condições
    if (VerificarMovimentoPossivel(greenPlayerI_Steps, selectDiceNumAnimation, greenMovementBlocks))
    {
        // Executa o movimento
        StartCoroutine(MoverPeca(greenPlayerI, greenPlayerI_Steps, selectDiceNumAnimation, greenMovementBlocks, GreenPlayerI_Button, playerTurn, 
    	(updatedSteps, updatedTurn) => {
        greenPlayerI_Steps = updatedSteps;
        playerTurn = updatedTurn;
    	}));
    }
    else
    {
        if(greenPlayerII_Steps + greenPlayerIII_Steps + greenPlayerIV_Steps == 0 && selectDiceNumAnimation != 6){
            TrocarTurno();
            photonView.RPC("InitializeDice", RpcTarget.All);
        }
        else{
            return;
        }
    }
}


[PunRPC]
void MoveGreenPlayerII()
{
    currentPlayerName = "GREEN PLAYER II";
    if (playerTurn != "GREEN") return;
    // Desativa bordas e botões antes de iniciar o movimento
    DesativarInteracaoPecas();

    // Verifica se o movimento é possível para GreenPlayerI com base nos passos e condições
    if (VerificarMovimentoPossivel(greenPlayerII_Steps, selectDiceNumAnimation, greenMovementBlocks))
    {
        // Executa o movimento
          StartCoroutine(MoverPeca(greenPlayerII, greenPlayerII_Steps, selectDiceNumAnimation, greenMovementBlocks, GreenPlayerII_Button, playerTurn, 
    	(updatedSteps, updatedTurn) => {
        greenPlayerII_Steps = updatedSteps;
        playerTurn = updatedTurn;
    	}));
		
    }
    else
    {
        if(greenPlayerI_Steps + greenPlayerIII_Steps + greenPlayerIV_Steps == 0 && selectDiceNumAnimation != 6){
            TrocarTurno();
            photonView.RPC("InitializeDice", RpcTarget.All);
        }
        else{
            return;
        }
    }
}

[PunRPC]
void MoveGreenPlayerIII()
{
    currentPlayerName = "GREEN PLAYER III";
    if (playerTurn != "GREEN") return;
    // Desativa bordas e botões antes de iniciar o movimento
    DesativarInteracaoPecas();

    // Verifica se o movimento é possível para GreenPlayerI com base nos passos e condições
    if (VerificarMovimentoPossivel(greenPlayerIII_Steps, selectDiceNumAnimation, greenMovementBlocks))
    {
        // Executa o movimento
          StartCoroutine(MoverPeca(greenPlayerIII, greenPlayerIII_Steps, selectDiceNumAnimation, greenMovementBlocks, GreenPlayerIII_Button, playerTurn, 
    	(updatedSteps, updatedTurn) => {
        greenPlayerIII_Steps = updatedSteps;
        playerTurn = updatedTurn;
    	}));
    }
    else
    {
        if(greenPlayerI_Steps + greenPlayerII_Steps + greenPlayerIV_Steps == 0 && selectDiceNumAnimation != 6){
            TrocarTurno();
            photonView.RPC("InitializeDice", RpcTarget.All);
        }
        else{
            return;
        }
    }
}

[PunRPC]
void MoveGreenPlayerIV()
{
    currentPlayerName = "GREEN PLAYER I";
    if (playerTurn != "GREEN") return;
    // Desativa bordas e botões antes de iniciar o movimento
    DesativarInteracaoPecas();

    // Verifica se o movimento é possível para GreenPlayerI com base nos passos e condições
    if (VerificarMovimentoPossivel(greenPlayerI_Steps, selectDiceNumAnimation, greenMovementBlocks))
    {
        // Executa o movimento
          StartCoroutine(MoverPeca(greenPlayerIV, greenPlayerIV_Steps, selectDiceNumAnimation, greenMovementBlocks, GreenPlayerIV_Button, playerTurn, 
    	(updatedSteps, updatedTurn) => {
        greenPlayerIV_Steps = updatedSteps;
        playerTurn = updatedTurn;
    	}));
	}
    else
    {
        if(greenPlayerI_Steps + greenPlayerII_Steps + greenPlayerIII_Steps == 0 && selectDiceNumAnimation != 6){
            TrocarTurno();
            photonView.RPC("InitializeDice", RpcTarget.All);
        }
        else{
            return;
        }
    }
}


// Verifica se a peça pode se mover com base na posição e passos
bool VerificarMovimentoPossivel(int playerSteps, int diceValue, List<GameObject> movementBlocks)
{
    if (playerSteps == 0 && diceValue != 6)
    {
        return false;
    }
    // Verifica se os passos restantes permitem o movimento com base no total de blocos e posição atual
    return (movementBlocks.Count - playerSteps) >= diceValue;
}

// Coroutine para mover a peça
IEnumerator MoverPeca(GameObject player, int playerSteps, int diceValue, List<GameObject> movementBlocks, Button playerButton, string color, System.Action<int, string> callback)
{
    int stepsToMove = diceValue;
    Vector3[] Player_Path = new Vector3[stepsToMove];

    if (playerSteps > 0)
    {
        for (int i = 0; i < stepsToMove; i++)
        {
            Player_Path[i] = movementBlocks[playerSteps + i].transform.position;
        }
        playerSteps += stepsToMove;

        if ((movementBlocks.Count - playerSteps) == 0)
        {
            playerTurn = "RED"; // Manter o turno no vermelho, pois a peça chegou na casa
            totalRedInHouse += 1;
            playerButton.interactable = false;
        }
        else if (diceValue != 6)
        {
            TrocarTurno();  // Troca de turno se não tirou 6 no dado
        }

        // Movimento com iTween
        if (Player_Path.Length > 1)
        {
            iTween.MoveTo(player, iTween.Hash("path", Player_Path, "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none"));
        }
        else
        {
            iTween.MoveTo(player, iTween.Hash("position", Player_Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none"));
        }
        yield return new WaitForSeconds(2.0f); // Espera o movimento completar
    }
    else
    {
        // Caso especial de sair da base com 6
        Player_Path[0] = movementBlocks[playerSteps].transform.position;
        playerSteps += 1;
        playerTurn = "RED";  // Manter o turno no vermelho
        iTween.MoveTo(player, iTween.Hash("position", Player_Path[0], "speed", 125, "time", 2.0f, "easetype", "elastic", "looptype", "none"));
        yield return new WaitForSeconds(2.0f);
    }
	callback(playerSteps, color);
    // Sincronização após movimento ou troca de turno
    photonView.RPC("InitializeDice", RpcTarget.All);
}

// Função de troca de turno
void TrocarTurno()
{
    playerTurn = (playerTurn == "RED") ? "GREEN" : "RED";
}



 void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detecta clique esquerdo do mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast hit object: " + hit.collider.gameObject.name);
            }

            // Para UI (caso seja UI)
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);

            foreach (var result in results)
            {
                Debug.Log("UI Raycast hit object: " + result.gameObject.name);
            }
        }
    }


}

