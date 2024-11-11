using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
		SceneManager.LoadScene("CoreMenu");
    	}
    }



    
    public void DiceRoll()

    {
        if (playerTurn == "RED" && PhotonNetwork.IsMasterClient || playerTurn == "GREEN" && !PhotonNetwork.IsMasterClient)
        {
            selectDiceNumAnimation = randomNo.Next(1, 7);
            photonView.RPC("DiceRollResult", RpcTarget.All, selectDiceNumAnimation);
        }
        else
        {
            Debug.Log("Not your turn to roll the dice.");
        }
    }

    [PunRPC]
    void DiceRollResult(int selectDiceNumAnimation)
    {
        DiceRollButton.interactable = false;
		selectDiceNumAnimation = 6;
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
       

        photonView.RPC("PlayersNotInitialized", RpcTarget.All);
    }


    [PunRPC]
    IEnumerator PlayersNotInitialized()
    {
        yield return new WaitForSeconds(10.8f);

        dice1_Roll_Animation.SetActive(false);
        dice2_Roll_Animation.SetActive(false);
        dice3_Roll_Animation.SetActive(false);
        dice4_Roll_Animation.SetActive(false);
        dice5_Roll_Animation.SetActive(false);
        dice6_Roll_Animation.SetActive(false);
        
        if (playerTurn == "RED" && PhotonNetwork.IsMasterClient || playerTurn == "GREEN" && !PhotonNetwork.IsMasterClient)
        {
            DiceRollButton.interactable = true;
        }
        switch(playerTurn)
		{
		case "RED": 
	

			//==================== CONDITION FOR BORDER GLOW ========================
			if ((redMovementBlocks.Count - redPlayerI_Steps) >= selectDiceNumAnimation && redPlayerI_Steps > 0 && (redMovementBlocks.Count > redPlayerI_Steps)) {
				redPlayerI_Border.SetActive (true);
				RedPlayerI_Button.interactable = true;

			} else {
				redPlayerI_Border.SetActive (false);
				RedPlayerI_Button.interactable = false;
			}

			if ((redMovementBlocks.Count - redPlayerII_Steps) >= selectDiceNumAnimation && redPlayerII_Steps > 0 && (redMovementBlocks.Count > redPlayerII_Steps)) {
				redPlayerII_Border.SetActive (true);
				RedPlayerII_Button.interactable = true;
			} else {
				redPlayerII_Border.SetActive (false);
				RedPlayerII_Button.interactable = false;
			}

			if ((redMovementBlocks.Count - redPlayerIII_Steps) >= selectDiceNumAnimation && redPlayerIII_Steps > 0 && (redMovementBlocks.Count > redPlayerIII_Steps)) {
				redPlayerIII_Border.SetActive (true);
				RedPlayerIII_Button.interactable = true;
			} else {
				redPlayerIII_Border.SetActive (false);
				RedPlayerIII_Button.interactable = false;
			}

			if ((redMovementBlocks.Count - redPlayerIV_Steps) >= selectDiceNumAnimation && redPlayerIV_Steps > 0 && (redMovementBlocks.Count > redPlayerIV_Steps)) {
				redPlayerIV_Border.SetActive (true);
				RedPlayerIV_Button.interactable = true;
			} else {
				redPlayerIV_Border.SetActive (false);
				RedPlayerIV_Button.interactable = false;
			}
			//========================= PLAYERS BORDER GLOW WHEN OPENING ===========================================

			if (selectDiceNumAnimation == 6 && redPlayerI_Steps == 0) {
				redPlayerI_Border.SetActive (true);
				RedPlayerI_Button.interactable = true;
			}
			if (selectDiceNumAnimation == 6 && redPlayerII_Steps == 0) {
				redPlayerII_Border.SetActive (true);
				RedPlayerII_Button.interactable = true;
			}				
			if (selectDiceNumAnimation == 6 && redPlayerIII_Steps == 0) {
				redPlayerIII_Border.SetActive (true);
				RedPlayerIII_Button.interactable = true;
			}					
			if (selectDiceNumAnimation == 6 && redPlayerIV_Steps == 0) {
				redPlayerIV_Border.SetActive (true);
				RedPlayerIV_Button.interactable = true;
			}	
        break;

            case "GREEN":

			//==================== CONDITION FOR BORDER GLOW ========================
			if ((greenMovementBlocks.Count - greenPlayerI_Steps) >= selectDiceNumAnimation && greenPlayerI_Steps > 0 && (greenMovementBlocks.Count > greenPlayerI_Steps)) 
			{
				greenPlayerI_Border.SetActive (true);
				GreenPlayerI_Button.interactable = true;
				Debug.Log ("Vc nao é o master porra !!");
			} 
			else 
			{
				greenPlayerI_Border.SetActive (false);
				GreenPlayerI_Button.interactable = false;
			}

			if ((greenMovementBlocks.Count - greenPlayerII_Steps) >= selectDiceNumAnimation && greenPlayerII_Steps > 0 && (greenMovementBlocks.Count > greenPlayerII_Steps)) 
			{
				greenPlayerII_Border.SetActive (true);
				GreenPlayerII_Button.interactable = true;
				Debug.Log ("vc nao é o master porra");
			} 
			else 
			{
				greenPlayerII_Border.SetActive (false);
				GreenPlayerII_Button.interactable = false;
			}

			if ((greenMovementBlocks.Count - greenPlayerIII_Steps) >= selectDiceNumAnimation && greenPlayerIII_Steps > 0 && (greenMovementBlocks.Count > greenPlayerIII_Steps)) 
			{
				greenPlayerIII_Border.SetActive (true);
				GreenPlayerIII_Button.interactable = true;
				Debug.Log ("vc nao é o master porra");
			} 
			else 
			{
				greenPlayerIII_Border.SetActive (false);
				GreenPlayerIII_Button.interactable = false;
			}

			if ((greenMovementBlocks.Count - greenPlayerIV_Steps) >= selectDiceNumAnimation && greenPlayerIV_Steps > 0 && (greenMovementBlocks.Count > greenPlayerIV_Steps)) 
			{
				greenPlayerIV_Border.SetActive (true);
				GreenPlayerIV_Button.interactable = true;
				Debug.Log ("vc nao é o master porra");
			} 
			else 
			{
				greenPlayerIV_Border.SetActive (false);
				GreenPlayerIV_Button.interactable = false;
			}
			//=======================================================================================================

			if (selectDiceNumAnimation == 6 && greenPlayerI_Steps == 0) 
			{
				greenPlayerI_Border.SetActive (true);
				GreenPlayerI_Button.interactable = true;
				Debug.Log ("vc nao é o master porra");
			}
			if (selectDiceNumAnimation == 6 && greenPlayerII_Steps == 0) 
			{
				greenPlayerII_Border.SetActive (true);
				GreenPlayerII_Button.interactable = true;
				Debug.Log ("vc nao é o master porra");
			}				
			if (selectDiceNumAnimation == 6 && greenPlayerIII_Steps == 0) 
			{
				greenPlayerIII_Border.SetActive (true);
				GreenPlayerIII_Button.interactable = true;
				Debug.Log ("vc nao é o master porra");
			}					
			if (selectDiceNumAnimation == 6 && greenPlayerIV_Steps == 0) 
			{
				greenPlayerIV_Border.SetActive (true);
				GreenPlayerIV_Button.interactable = true;
				Debug.Log ("vc nao é o master porra");
			}
            break;
        }
        bool redHasMoves = CheckPlayerMoves("RED");
        bool greenHasMoves = CheckPlayerMoves("GREEN");

        if (playerTurn == "RED" && !redHasMoves)
        {
            playerTurn = "GREEN";
			photonView.RPC("SyncGameState", RpcTarget.All);
            photonView.RPC("InitializeDice", RpcTarget.All);
     
        }
        else if (playerTurn == "GREEN" && !greenHasMoves)
        {
            playerTurn = "RED";
			photonView.RPC("SyncGameState", RpcTarget.All);
            photonView.RPC("InitializeDice", RpcTarget.All);
            
        }

        DiceRollButton.interactable = true;
    }

    bool CheckPlayerMoves(string color)
    {
        if (playerTurn == "RED" && PhotonNetwork.IsMasterClient)
		{
			return redPlayerI_Border.activeInHierarchy || redPlayerII_Border.activeInHierarchy ||
            redPlayerIII_Border.activeInHierarchy || redPlayerIV_Border.activeInHierarchy;
        }

        else if(playerTurn == "GREEN" && !PhotonNetwork.IsMasterClient)
		{
            return greenPlayerI_Border.activeInHierarchy || greenPlayerII_Border.activeInHierarchy ||
        	greenPlayerIII_Border.activeInHierarchy || greenPlayerIV_Border.activeInHierarchy;

        }
        else {
            return false;
        }
    }

    [PunRPC]
    void InitializeDice()
    {
        DiceRollButton.interactable = true;

        dice1_Roll_Animation.SetActive(false);
        dice2_Roll_Animation.SetActive(false);
        dice3_Roll_Animation.SetActive(false);
        dice4_Roll_Animation.SetActive(false);
        dice5_Roll_Animation.SetActive(false);
        dice6_Roll_Animation.SetActive(false);

        if (playerTurn == "RED")
        {
            diceRoll.position = redDiceRollPos.position;
            frameRed.SetActive(true);
            frameGreen.SetActive(false);
              if (totalRedInHouse > 3) 
				{
					SoundManagerScript.winAudioSource.Play ();
					redScreen.SetActive (true);		
					photonView.RPC("StartGameCompletedRoutine", RpcTarget.All);			
					//StartCoroutine ("GameCompletedRoutine");
					playerTurn = "NONE";
				}
              

				
        }
        else if (playerTurn == "GREEN")
        {
            diceRoll.position = greenDiceRollPos.position;
            frameRed.SetActive(false);
            frameGreen.SetActive(true);
            if (totalGreenInHouse > 3) 
				{
					SoundManagerScript.winAudioSource.Play ();
					greenScreen.SetActive (true);
					photonView.RPC("StartGameCompletedRoutine", RpcTarget.All);
					//StartCoroutine ("GameCompletedRoutine");
					playerTurn = "NONE";
				}

				
        }
        if (currentPlayerName != "none") {
			switch (playerTurn) {
                case "RED":
				if (currentPlayerName.Contains ("RED PLAYER")) {
					if (currentPlayer == GreenPlayerI_Script_Multiplayer.greenPlayerI_ColName && (currentPlayer != "Star" && GreenPlayerI_Script_Multiplayer.greenPlayerI_ColName != "Star")) {
						SoundManagerScript.dismissalAudioSource.Play ();
						greenPlayerI.transform.position = greenPlayerI_Pos;
						GreenPlayerI_Script_Multiplayer.greenPlayerI_ColName = "none";
						greenPlayerI_Steps = 0;
						playerTurn = "RED";
					}
					if (currentPlayer == GreenPlayerII_Script_Multiplayer.greenPlayerII_ColName && (currentPlayer != "Star" && GreenPlayerII_Script_Multiplayer.greenPlayerII_ColName != "Star")) {
						SoundManagerScript.dismissalAudioSource.Play ();
						greenPlayerII.transform.position = greenPlayerII_Pos;
						GreenPlayerII_Script_Multiplayer.greenPlayerII_ColName = "none";
						greenPlayerII_Steps = 0;
						playerTurn = "RED";
					}
					if (currentPlayer == GreenPlayerIII_Script_Multiplayer.greenPlayerIII_ColName && (currentPlayer != "Star" && GreenPlayerIII_Script_Multiplayer.greenPlayerIII_ColName != "Star")) {
						SoundManagerScript.dismissalAudioSource.Play ();
						greenPlayerIII.transform.position = greenPlayerIII_Pos;
						GreenPlayerIII_Script_Multiplayer.greenPlayerIII_ColName = "none";
						greenPlayerIII_Steps = 0;
						playerTurn = "RED";
					}
					if (currentPlayer == GreenPlayerIV_Script_Multiplayer.greenPlayerIV_ColName && (currentPlayer != "Star" && GreenPlayerIV_Script_Multiplayer.greenPlayerIV_ColName != "Star")) {
						SoundManagerScript.dismissalAudioSource.Play ();
						greenPlayerIV.transform.position = greenPlayerIV_Pos;
						GreenPlayerIV_Script_Multiplayer.greenPlayerIV_ColName = "none";
						greenPlayerIV_Steps = 0;
						playerTurn = "RED";
					}
                } 
                break; 
                case "GREEN":
                if (currentPlayerName.Contains ("GREEN PLAYER")) {
					if (currentPlayer == RedPlayerI_Script_Multiplayer.redPlayerI_ColName && (currentPlayer != "Star" && RedPlayerI_Script_Multiplayer.redPlayerI_ColName != "Star")) {
						SoundManagerScript.dismissalAudioSource.Play ();
						redPlayerI.transform.position = redPlayerI_Pos;
						RedPlayerI_Script_Multiplayer.redPlayerI_ColName = "none";
						redPlayerI_Steps = 0;
						playerTurn = "GREEN";
					}
					if (currentPlayer == RedPlayerII_Script_Multiplayer.redPlayerII_ColName && (currentPlayer != "Star" && RedPlayerII_Script_Multiplayer.redPlayerII_ColName != "Star")) {
						SoundManagerScript.dismissalAudioSource.Play ();
						redPlayerII.transform.position = redPlayerII_Pos;
						RedPlayerII_Script_Multiplayer.redPlayerII_ColName = "none";
						redPlayerII_Steps = 0;
						playerTurn = "GREEN";
					}
					if (currentPlayer == RedPlayerIII_Script_Multiplayer.redPlayerIII_ColName && (currentPlayer != "Star" && RedPlayerIII_Script_Multiplayer.redPlayerIII_ColName != "Star")) {
						SoundManagerScript.dismissalAudioSource.Play ();
						redPlayerIII.transform.position = redPlayerIII_Pos;
						RedPlayerIII_Script_Multiplayer.redPlayerIII_ColName = "none";
						redPlayerIII_Steps = 0;
						playerTurn = "GREEN";
					}
					if (currentPlayer == RedPlayerIV_Script_Multiplayer.redPlayerIV_ColName && (currentPlayer != "Star" && RedPlayerIV_Script_Multiplayer.redPlayerIV_ColName != "Star")) {
						SoundManagerScript.dismissalAudioSource.Play ();
						redPlayerIV.transform.position = redPlayerIV_Pos;
						RedPlayerIV_Script_Multiplayer.redPlayerIV_ColName = "none";
						redPlayerIV_Steps = 0;
						playerTurn = "GREEN";
					}
				}
				break; 
            }
        }

        
                GreenPlayerI_Button.interactable = false;
				GreenPlayerII_Button.interactable = false;
				GreenPlayerIII_Button.interactable = false;
				GreenPlayerIV_Button.interactable = false;
                greenPlayerI_Border.SetActive (false);
				greenPlayerII_Border.SetActive (false);
				greenPlayerIII_Border.SetActive (false);
				greenPlayerIV_Border.SetActive (false);
                RedPlayerI_Button.interactable = false;
				RedPlayerII_Button.interactable = false;
				RedPlayerIII_Button.interactable = false;
				RedPlayerIV_Button.interactable = false;
                redPlayerI_Border.SetActive (false);
				redPlayerII_Border.SetActive (false);
				redPlayerIII_Border.SetActive (false);
				redPlayerIV_Border.SetActive (false);

        photonView.RPC("SyncGameState", RpcTarget.All);
    }
void OnMoveComplete()
{
    // Chama o método InitializeDice em todos os clientes
    photonView.RPC("InitializeDice", RpcTarget.All);
}

public void redPlayerI_UI()
{
    if (playerTurn  == "RED")
    {
        photonView.RPC("MoveRedPlayerI", RpcTarget.All);
    }
}



[PunRPC]
void MoveRedPlayerI()
{
    SoundManagerScript.playerAudioSource.Play();

    // Desativar bordas e botões para evitar conflitos de interação
    redPlayerI_Border.SetActive(false);
    redPlayerII_Border.SetActive(false);
    redPlayerIII_Border.SetActive(false);
    redPlayerIV_Border.SetActive(false);
    
    RedPlayerI_Button.interactable = false;
    RedPlayerII_Button.interactable = false;
    RedPlayerIII_Button.interactable = false;
    RedPlayerIV_Button.interactable = false;



    if (playerTurn == "RED" && (redMovementBlocks.Count - redPlayerI_Steps) > selectDiceNumAnimation)
    {
        if (redPlayerI_Steps > 0) 
			{
				Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					redPlayer_Path [i] = redMovementBlocks [redPlayerI_Steps + i].transform.position;
				}

				redPlayerI_Steps += selectDiceNumAnimation;			

				if (selectDiceNumAnimation == 6) 
				{
					playerTurn = "RED";
                    photonView.RPC("SyncGameState", RpcTarget.All);
				} 
				else 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
                    photonView.RPC("InitializeDice", RpcTarget.All);
				}
			

				//currentPlayer = RedPlayerI_Script_Multiplayer.redPlayerI_ColName;
				currentPlayerName = "RED PLAYER I";

				//if(redPlayerI_Steps + selectDiceNumAnimation == redMovementBlocks.Count)
				if (redPlayer_Path.Length > 1) 
				{
					//redPlayerI.transform.DOPath (redPlayer_Path, 2.0f, PathType.Linear, PathMode.Full3D, 10, Color.red);
					iTween.MoveTo (redPlayerI, iTween.Hash ("path", redPlayer_Path, 
															"speed", 125,"time",2.0f, 
															"easetype", "elastic", "looptype", "none", 
															"oncomplete", "OnMoveComplete", 
															"oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (redPlayerI, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			} 
        else 
			{
				if (selectDiceNumAnimation == 6 && redPlayerI_Steps == 0) 
				{
					Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];
					redPlayer_Path [0] = redMovementBlocks [redPlayerI_Steps].transform.position;
					redPlayerI_Steps += 1;
					playerTurn = "RED";
                    photonView.RPC("SyncGameState", RpcTarget.All);
					//currentPlayer = RedPlayerI_Script_Multiplayer.redPlayerI_ColName;
					currentPlayerName = "RED PLAYER I";
					iTween.MoveTo (redPlayerI, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			}
    }
    else
	{
			// Condition when Player Coin is reached successfully in House....(Actual Number of required moves to get into the House)
			if (playerTurn == "RED" && (redMovementBlocks.Count - redPlayerI_Steps) == selectDiceNumAnimation) 
			{
				Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					redPlayer_Path [i] = redMovementBlocks [redPlayerI_Steps + i].transform.position;
				}

				redPlayerI_Steps += selectDiceNumAnimation;

				playerTurn = "RED";
                photonView.RPC("SyncGameState", RpcTarget.All);

				//redPlayerI_Steps = 0;

				if (redPlayer_Path.Length > 1) 
				{
					iTween.MoveTo (redPlayerI, iTween.Hash ("path", redPlayer_Path, "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (redPlayerI, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
				totalRedInHouse += 1;
				Debug.Log ("Cool !!");
				RedPlayerI_Button.enabled = false;
			}
			else
			{
				Debug.Log ("You need "+  (redMovementBlocks.Count - redPlayerI_Steps).ToString() + " to enter into the house.");
                

				if(redPlayerII_Steps + redPlayerIII_Steps + redPlayerIV_Steps == 0 && selectDiceNumAnimation != 6)
				{   playerTurn = "GREEN";
					photonView.RPC("InitializeDice", RpcTarget.All);
                    photonView.RPC("SyncGameState", RpcTarget.All);
				}
				
			}
	}
    
}
 
 public void redPlayerII_UI()
{
    if (playerTurn  == "RED")
    {
        photonView.RPC("MoveRedPlayerII", RpcTarget.All);
    }
}



[PunRPC]
void MoveRedPlayerII()
{
    SoundManagerScript.playerAudioSource.Play();

    // Desativar bordas e botões para evitar conflitos de interação
    redPlayerI_Border.SetActive(false);
    redPlayerII_Border.SetActive(false);
    redPlayerIII_Border.SetActive(false);
    redPlayerIV_Border.SetActive(false);
    
    RedPlayerI_Button.interactable = false;
    RedPlayerII_Button.interactable = false;
    RedPlayerIII_Button.interactable = false;
    RedPlayerIV_Button.interactable = false;



    if (playerTurn == "RED" && (redMovementBlocks.Count - redPlayerII_Steps) > selectDiceNumAnimation)
    {
        if (redPlayerII_Steps > 0) 
			{
				Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					redPlayer_Path [i] = redMovementBlocks [redPlayerII_Steps + i].transform.position;
				}

				redPlayerII_Steps += selectDiceNumAnimation;			

				if (selectDiceNumAnimation == 6) 
				{
					playerTurn = "RED";
                    photonView.RPC("SyncGameState", RpcTarget.All);
				} 
				else 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
                    photonView.RPC("InitializeDice", RpcTarget.All);
				}
			

				//currentPlayer = RedPlayerII_Script_Multiplayer.redPlayerII_ColName;
				currentPlayerName = "RED PLAYER II";

				//if(redPlayerII_Steps + selectDiceNumAnimation == redMovementBlocks.Count)
				if (redPlayer_Path.Length > 1) 
				{
					//redPlayerII.transform.DOPath (redPlayer_Path, 2.0f, PathType.Linear, PathMode.Full3D, 10, Color.red);
					iTween.MoveTo (redPlayerII, iTween.Hash ("path", redPlayer_Path, 
															"speed", 125,"time",2.0f, 
															"easetype", "elastic", "looptype", "none", 
															"oncomplete", "OnMoveComplete", 
															"oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (redPlayerII, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			} 
        else 
			{
				if (selectDiceNumAnimation == 6 && redPlayerII_Steps == 0) 
				{
					Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];
					redPlayer_Path [0] = redMovementBlocks [redPlayerII_Steps].transform.position;
					redPlayerII_Steps += 1;
					playerTurn = "RED";
                    photonView.RPC("SyncGameState", RpcTarget.All);
					//currentPlayer = RedPlayerII_Script_Multiplayer.redPlayerII_ColName;
					currentPlayerName = "RED PLAYER II";
					iTween.MoveTo (redPlayerII, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			}
    }
    else
	{
			// Condition when Player Coin is reached successfully in House....(Actual Number of required moves to get into the House)
			if (playerTurn == "RED" && (redMovementBlocks.Count - redPlayerII_Steps) == selectDiceNumAnimation) 
			{
				Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					redPlayer_Path [i] = redMovementBlocks [redPlayerII_Steps + i].transform.position;
				}

				redPlayerII_Steps += selectDiceNumAnimation;

				playerTurn = "RED";
                photonView.RPC("SyncGameState", RpcTarget.All);

				//redPlayerII_Steps = 0;

				if (redPlayer_Path.Length > 1) 
				{
					iTween.MoveTo (redPlayerII, iTween.Hash ("path", redPlayer_Path, "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (redPlayerII, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
				totalRedInHouse += 1;
				Debug.Log ("Cool !!");
				RedPlayerII_Button.enabled = false;
			}
			else
			{
				Debug.Log ("You need "+  (redMovementBlocks.Count - redPlayerII_Steps).ToString() + " to enter into the house.");
                

				if(redPlayerI_Steps + redPlayerIII_Steps + redPlayerIV_Steps == 0 && selectDiceNumAnimation != 6)
				{   playerTurn = "GREEN";
					photonView.RPC("InitializeDice", RpcTarget.All);
                    photonView.RPC("SyncGameState", RpcTarget.All);
				}
			}
	}
    
}

public void redPlayerIII_UI()
{
    if (playerTurn  == "RED")
    {
        photonView.RPC("MoveRedPlayerIII", RpcTarget.All);
    }
}



[PunRPC]
void MoveRedPlayerIII()
{
    SoundManagerScript.playerAudioSource.Play();

    // Desativar bordas e botões para evitar conflitos de interação
    redPlayerI_Border.SetActive(false);
    redPlayerII_Border.SetActive(false);
    redPlayerIII_Border.SetActive(false);
    redPlayerIV_Border.SetActive(false);
    
    RedPlayerI_Button.interactable = false;
    RedPlayerII_Button.interactable = false;
    RedPlayerIII_Button.interactable = false;
    RedPlayerIV_Button.interactable = false;



    if (playerTurn == "RED" && (redMovementBlocks.Count - redPlayerIII_Steps) > selectDiceNumAnimation)
    {
        if (redPlayerIII_Steps > 0) 
			{
				Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					redPlayer_Path [i] = redMovementBlocks [redPlayerIII_Steps + i].transform.position;
				}

				redPlayerIII_Steps += selectDiceNumAnimation;			

				if (selectDiceNumAnimation == 6) 
				{
					playerTurn = "RED";
                    photonView.RPC("SyncGameState", RpcTarget.All);
				} 
				else 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
                    photonView.RPC("InitializeDice", RpcTarget.All);
				}
			

				//currentPlayer = RedPlayerIII_Script_Multiplayer.redPlayerI_ColName;
				currentPlayerName = "RED PLAYER III";

				//if(redPlayerIII_Steps + selectDiceNumAnimation == redMovementBlocks.Count)
				if (redPlayer_Path.Length > 1) 
				{
					//redPlayerIII.transform.DOPath (redPlayer_Path, 2.0f, PathType.Linear, PathMode.Full3D, 10, Color.red);
					iTween.MoveTo (redPlayerIII, iTween.Hash ("path", redPlayer_Path, 
															"speed", 125,"time",2.0f, 
															"easetype", "elastic", "looptype", "none", 
															"oncomplete", "OnMoveComplete", 
															"oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (redPlayerIII, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			} 
        else 
			{
				if (selectDiceNumAnimation == 6 && redPlayerIII_Steps == 0) 
				{
					Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];
					redPlayer_Path [0] = redMovementBlocks [redPlayerIII_Steps].transform.position;
					redPlayerIII_Steps += 1;
					playerTurn = "RED";
                    photonView.RPC("SyncGameState", RpcTarget.All);
					//currentPlayer = RedPlayerIII_Script_Multiplayer.redPlayerIII_ColName;
					currentPlayerName = "RED PLAYER III";
					iTween.MoveTo (redPlayerIII, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			}
    }
    else
	{
			// Condition when Player Coin is reached successfully in House....(Actual Number of required moves to get into the House)
			if (playerTurn == "RED" && (redMovementBlocks.Count - redPlayerIII_Steps) == selectDiceNumAnimation) 
			{
				Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					redPlayer_Path [i] = redMovementBlocks [redPlayerIII_Steps + i].transform.position;
				}

				redPlayerIII_Steps += selectDiceNumAnimation;

				playerTurn = "RED";
                photonView.RPC("SyncGameState", RpcTarget.All);

				//redPlayerIII_Steps = 0;

				if (redPlayer_Path.Length > 1) 
				{
					iTween.MoveTo (redPlayerIII, iTween.Hash ("path", redPlayer_Path, "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (redPlayerIII, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
				totalRedInHouse += 1;
				Debug.Log ("Cool !!");
				RedPlayerIII_Button.enabled = false;
			}
			else
			{
				Debug.Log ("You need "+  (redMovementBlocks.Count - redPlayerIII_Steps).ToString() + " to enter into the house.");
                

				if(redPlayerI_Steps + redPlayerII_Steps + redPlayerIV_Steps == 0 && selectDiceNumAnimation != 6)
				{   playerTurn = "GREEN";
					photonView.RPC("InitializeDice", RpcTarget.All);
                    photonView.RPC("SyncGameState", RpcTarget.All);
				}
			}
	}
    
}


public void redPlayerIV_UI()
{
    if (playerTurn  == "RED")
    {
        photonView.RPC("MoveRedPlayerIV", RpcTarget.All);
    }
}



[PunRPC]
void MoveRedPlayerIV()
{
    SoundManagerScript.playerAudioSource.Play();

    // Desativar bordas e botões para evitar conflitos de interação
    redPlayerI_Border.SetActive(false);
    redPlayerII_Border.SetActive(false);
    redPlayerIII_Border.SetActive(false);
    redPlayerIV_Border.SetActive(false);
    
    RedPlayerI_Button.interactable = false;
    RedPlayerII_Button.interactable = false;
    RedPlayerIII_Button.interactable = false;
    RedPlayerIV_Button.interactable = false;



    if (playerTurn == "RED" && (redMovementBlocks.Count - redPlayerIV_Steps) > selectDiceNumAnimation)
    {
        if (redPlayerIV_Steps > 0) 
			{
				Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					redPlayer_Path [i] = redMovementBlocks [redPlayerIV_Steps + i].transform.position;
				}

				redPlayerIV_Steps += selectDiceNumAnimation;			

				if (selectDiceNumAnimation == 6) 
				{
					playerTurn = "RED";
                    photonView.RPC("SyncGameState", RpcTarget.All);
				} 
				else 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
                    photonView.RPC("InitializeDice", RpcTarget.All);
				}
			

				//currentPlayer = RedPlayerIV_Script_Multiplayer.redPlayerI_ColName;
				currentPlayerName = "RED PLAYER IV";

				//if(redPlayerIV_Steps + selectDiceNumAnimation == redMovementBlocks.Count)
				if (redPlayer_Path.Length > 1) 
				{
					//redPlayerIV.transform.DOPath (redPlayer_Path, 2.0f, PathType.Linear, PathMode.Full3D, 10, Color.red);
					iTween.MoveTo (redPlayerIV, iTween.Hash ("path", redPlayer_Path, 
															"speed", 125,"time",2.0f, 
															"easetype", "elastic", "looptype", "none", 
															"oncomplete", "OnMoveComplete", 
															"oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (redPlayerIV, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			} 
        else 
			{
				if (selectDiceNumAnimation == 6 && redPlayerIV_Steps == 0) 
				{
					Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];
					redPlayer_Path [0] = redMovementBlocks [redPlayerIV_Steps].transform.position;
					redPlayerIV_Steps += 1;
					playerTurn = "RED";
                    photonView.RPC("SyncGameState", RpcTarget.All);
					//currentPlayer = RedPlayerIV_Script_Multiplayer.redPlayerIV_ColName;
					currentPlayerName = "RED PLAYER IV";
					iTween.MoveTo (redPlayerIV, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			}
    }
    else
	{
			// Condition when Player Coin is reached successfully in House....(Actual Number of required moves to get into the House)
			if (playerTurn == "RED" && (redMovementBlocks.Count - redPlayerIV_Steps) == selectDiceNumAnimation) 
			{
				Vector3[] redPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					redPlayer_Path [i] = redMovementBlocks [redPlayerIV_Steps + i].transform.position;
				}

				redPlayerIV_Steps += selectDiceNumAnimation;

				playerTurn = "RED";
                photonView.RPC("SyncGameState", RpcTarget.All);

				//redPlayerIV_Steps = 0;

				if (redPlayer_Path.Length > 1) 
				{
					iTween.MoveTo (redPlayerIV, iTween.Hash ("path", redPlayer_Path, "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (redPlayerIV, iTween.Hash ("position", redPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
				totalRedInHouse += 1;
				Debug.Log ("Cool !!");
				RedPlayerIV_Button.enabled = false;
			}
			else
			{
				Debug.Log ("You need "+  (redMovementBlocks.Count - redPlayerIV_Steps).ToString() + " to enter into the house.");
                

				if(redPlayerI_Steps + redPlayerII_Steps + redPlayerIII_Steps == 0 && selectDiceNumAnimation != 6)
				{   playerTurn = "GREEN";
					photonView.RPC("InitializeDice", RpcTarget.All);
                    photonView.RPC("SyncGameState", RpcTarget.All);
				}
			}
	}
    
}


public void greenPlayerI_UI()
{
    if (playerTurn  == "GREEN")
    {
        photonView.RPC("MoveGreenPlayerI", RpcTarget.All);
    }
}



[PunRPC]
void MoveGreenPlayerI()
{
    SoundManagerScript.playerAudioSource.Play();

    // Desativar bordas e botões para evitar conflitos de interação
    greenPlayerI_Border.SetActive(false);
    greenPlayerII_Border.SetActive(false);
    greenPlayerIII_Border.SetActive(false);
    greenPlayerIV_Border.SetActive(false);
    
    GreenPlayerI_Button.interactable = false;
    GreenPlayerII_Button.interactable = false;
    GreenPlayerIII_Button.interactable = false;
    GreenPlayerIV_Button.interactable = false;



    if (playerTurn == "GREEN" && (greenMovementBlocks.Count - greenPlayerI_Steps) > selectDiceNumAnimation)
    {
        if (greenPlayerI_Steps > 0) 
			{
				Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					greenPlayer_Path [i] = greenMovementBlocks [greenPlayerI_Steps + i].transform.position;
				}

				greenPlayerI_Steps += selectDiceNumAnimation;			

				if (selectDiceNumAnimation == 6) 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
				} 
				else 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
                    photonView.RPC("InitializeDice", RpcTarget.All);
				}
			

				//currentPlayer = GreenPlayerI_Script_Multiplayer.greenPlayerI_ColName;
				currentPlayerName = "GREEN PLAYER I";

				//if(greenPlayerI_Steps + selectDiceNumAnimation == greenMovementBlocks.Count)
				if (greenPlayer_Path.Length > 1) 
				{
					//greenPlayerI.transform.DOPath (greenPlayer_Path, 2.0f, PathType.Linear, PathMode.Full3D, 10, Color.green);
					iTween.MoveTo (greenPlayerI, iTween.Hash ("path", greenPlayer_Path, 
															"speed", 125,"time",2.0f, 
															"easetype", "elastic", "looptype", "none", 
															"oncomplete", "OnMoveComplete", 
															"oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (greenPlayerI, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			} 
        else 
			{
				if (selectDiceNumAnimation == 6 && greenPlayerI_Steps == 0) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];
					greenPlayer_Path [0] = greenMovementBlocks [greenPlayerI_Steps].transform.position;
					greenPlayerI_Steps += 1;
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
					//currentPlayer = GreenPlayerI_Script_Multiplayer.greenPlayerI_ColName;
					currentPlayerName = "GREEN PLAYER I";
					iTween.MoveTo (greenPlayerI, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			}
    }
    else
	{
			// Condition when Player Coin is reached successfully in House....(Actual Number of requigreen moves to get into the House)
			if (playerTurn == "GREEN" && (greenMovementBlocks.Count - greenPlayerI_Steps) == selectDiceNumAnimation) 
			{
				Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					greenPlayer_Path [i] = greenMovementBlocks [greenPlayerI_Steps + i].transform.position;
				}

				greenPlayerI_Steps += selectDiceNumAnimation;

				playerTurn = "GREEN";
                photonView.RPC("SyncGameState", RpcTarget.All);

				//greenPlayerI_Steps = 0;

				if (greenPlayer_Path.Length > 1) 
				{
					iTween.MoveTo (greenPlayerI, iTween.Hash ("path", greenPlayer_Path, "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (greenPlayerI, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
				totalGreenInHouse += 1;
				Debug.Log ("Cool !!");
				GreenPlayerI_Button.enabled = false;
			}
			else
			{
				Debug.Log ("You need "+  (greenMovementBlocks.Count - greenPlayerI_Steps).ToString() + " to enter into the house.");
                

				if(greenPlayerII_Steps + greenPlayerIII_Steps + greenPlayerIV_Steps == 0 && selectDiceNumAnimation != 6)
				{   playerTurn = "GREEN";
					photonView.RPC("InitializeDice", RpcTarget.All);
                    photonView.RPC("SyncGameState", RpcTarget.All);
				}
			}
	}
    
}

public void greenPlayerII_UI()
{
    if (playerTurn  == "GREEN")
    {
        photonView.RPC("MoveGreenPlayerII", RpcTarget.All);
    }
}



[PunRPC]
void MoveGreenPlayerII()
{
    SoundManagerScript.playerAudioSource.Play();

    // Desativar bordas e botões para evitar conflitos de interação
    greenPlayerI_Border.SetActive(false);
    greenPlayerII_Border.SetActive(false);
    greenPlayerIII_Border.SetActive(false);
    greenPlayerIV_Border.SetActive(false);
    
    GreenPlayerI_Button.interactable = false;
    GreenPlayerII_Button.interactable = false;
    GreenPlayerIII_Button.interactable = false;
    GreenPlayerIV_Button.interactable = false;



    if (playerTurn == "GREEN" && (greenMovementBlocks.Count - greenPlayerII_Steps) > selectDiceNumAnimation)
    {
        if (greenPlayerII_Steps > 0) 
			{
				Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					greenPlayer_Path [i] = greenMovementBlocks [greenPlayerII_Steps + i].transform.position;
				}

				greenPlayerII_Steps += selectDiceNumAnimation;			

				if (selectDiceNumAnimation == 6) 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
				} 
				else 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
                    photonView.RPC("InitializeDice", RpcTarget.All);
				}
			

				//currentPlayer = GreenPlayerII_Script_Multiplayer.greenPlayerII_ColName;
				currentPlayerName = "GREEN PLAYER II";

				//if(greenPlayerII_Steps + selectDiceNumAnimation == greenMovementBlocks.Count)
				if (greenPlayer_Path.Length > 1) 
				{
					//greenPlayerII.transform.DOPath (greenPlayer_Path, 2.0f, PathType.Linear, PathMode.Full3D, 10, Color.green);
					iTween.MoveTo (greenPlayerII, iTween.Hash ("path", greenPlayer_Path, 
															"speed", 125,"time",2.0f, 
															"easetype", "elastic", "looptype", "none", 
															"oncomplete", "OnMoveComplete", 
															"oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (greenPlayerII, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			} 
        else 
			{
				if (selectDiceNumAnimation == 6 && greenPlayerII_Steps == 0) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];
					greenPlayer_Path [0] = greenMovementBlocks [greenPlayerII_Steps].transform.position;
					greenPlayerII_Steps += 1;
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
					//currentPlayer = GreenPlayerII_Script_Multiplayer.greenPlayerII_ColName;
					currentPlayerName = "GREEN PLAYER II";
					iTween.MoveTo (greenPlayerII, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			}
    }
    else
	{
			// Condition when Player Coin is reached successfully in House....(Actual Number of requigreen moves to get into the House)
			if (playerTurn == "GREEN" && (greenMovementBlocks.Count - greenPlayerII_Steps) == selectDiceNumAnimation) 
			{
				Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					greenPlayer_Path [i] = greenMovementBlocks [greenPlayerII_Steps + i].transform.position;
				}

				greenPlayerII_Steps += selectDiceNumAnimation;

				playerTurn = "GREEN";
                photonView.RPC("SyncGameState", RpcTarget.All);

				//greenPlayerII_Steps = 0;

				if (greenPlayer_Path.Length > 1) 
				{
					iTween.MoveTo (greenPlayerII, iTween.Hash ("path", greenPlayer_Path, "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (greenPlayerII, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
				totalGreenInHouse += 1;
				Debug.Log ("Cool !!");
				GreenPlayerII_Button.enabled = false;
			}
			else
			{
				Debug.Log ("You need "+  (greenMovementBlocks.Count - greenPlayerII_Steps).ToString() + " to enter into the house.");
                

				if(greenPlayerI_Steps + greenPlayerIII_Steps + greenPlayerIV_Steps == 0 && selectDiceNumAnimation != 6)
				{   playerTurn = "GREEN";
					photonView.RPC("InitializeDice", RpcTarget.All);
                    photonView.RPC("SyncGameState", RpcTarget.All);
				}
			}
	}
    
}

public void greenPlayerIII_UI()
{
    if (playerTurn  == "GREEN")
    {
        photonView.RPC("MoveGreenPlayerIII", RpcTarget.All);
    }
}



[PunRPC]
void MoveGreenPlayerIII()
{
    SoundManagerScript.playerAudioSource.Play();

    // Desativar bordas e botões para evitar conflitos de interação
    greenPlayerI_Border.SetActive(false);
    greenPlayerII_Border.SetActive(false);
    greenPlayerIII_Border.SetActive(false);
    greenPlayerIV_Border.SetActive(false);
    
    GreenPlayerI_Button.interactable = false;
    GreenPlayerII_Button.interactable = false;
    GreenPlayerIII_Button.interactable = false;
    GreenPlayerIV_Button.interactable = false;



    if (playerTurn == "GREEN" && (greenMovementBlocks.Count - greenPlayerIII_Steps) > selectDiceNumAnimation)
    {
        if (greenPlayerIII_Steps > 0) 
			{
				Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					greenPlayer_Path [i] = greenMovementBlocks [greenPlayerIII_Steps + i].transform.position;
				}

				greenPlayerIII_Steps += selectDiceNumAnimation;			

				if (selectDiceNumAnimation == 6) 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
				} 
				else 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
                    photonView.RPC("InitializeDice", RpcTarget.All);
				}
			

				//currentPlayer = GreenPlayerIII_Script_Multiplayer.greenPlayerI_ColName;
				currentPlayerName = "GREEN PLAYER III";

				//if(greenPlayerIII_Steps + selectDiceNumAnimation == greenMovementBlocks.Count)
				if (greenPlayer_Path.Length > 1) 
				{
					//greenPlayerIII.transform.DOPath (greenPlayer_Path, 2.0f, PathType.Linear, PathMode.Full3D, 10, Color.green);
					iTween.MoveTo (greenPlayerIII, iTween.Hash ("path", greenPlayer_Path, 
															"speed", 125,"time",2.0f, 
															"easetype", "elastic", "looptype", "none", 
															"oncomplete", "OnMoveComplete", 
															"oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (greenPlayerIII, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			} 
        else 
			{
				if (selectDiceNumAnimation == 6 && greenPlayerIII_Steps == 0) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];
					greenPlayer_Path [0] = greenMovementBlocks [greenPlayerIII_Steps].transform.position;
					greenPlayerIII_Steps += 1;
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
					//currentPlayer = GreenPlayerIII_Script_Multiplayer.greenPlayerIII_ColName;
					currentPlayerName = "GREEN PLAYER III";
					iTween.MoveTo (greenPlayerIII, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			}
    }
    else
	{
			// Condition when Player Coin is reached successfully in House....(Actual Number of requigreen moves to get into the House)
			if (playerTurn == "GREEN" && (greenMovementBlocks.Count - greenPlayerIII_Steps) == selectDiceNumAnimation) 
			{
				Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					greenPlayer_Path [i] = greenMovementBlocks [greenPlayerIII_Steps + i].transform.position;
				}

				greenPlayerIII_Steps += selectDiceNumAnimation;

				playerTurn = "GREEN";
                photonView.RPC("SyncGameState", RpcTarget.All);

				//greenPlayerIII_Steps = 0;

				if (greenPlayer_Path.Length > 1) 
				{
					iTween.MoveTo (greenPlayerIII, iTween.Hash ("path", greenPlayer_Path, "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (greenPlayerIII, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
				totalGreenInHouse += 1;
				Debug.Log ("Cool !!");
				GreenPlayerIII_Button.enabled = false;
			}
			else
			{
				Debug.Log ("You need "+  (greenMovementBlocks.Count - greenPlayerIII_Steps).ToString() + " to enter into the house.");
                

				if(greenPlayerI_Steps + greenPlayerII_Steps + greenPlayerIV_Steps == 0 && selectDiceNumAnimation != 6)
				{   playerTurn = "GREEN";
					photonView.RPC("InitializeDice", RpcTarget.All);
                    photonView.RPC("SyncGameState", RpcTarget.All);
				}
			}
	}
    
}

public void greenPlayerIV_UI()
{
    if (playerTurn  == "GREEN")
    {
        photonView.RPC("MoveGreenPlayerIV", RpcTarget.All);
    }
}



[PunRPC]
void MoveGreenPlayerIV()
{
    SoundManagerScript.playerAudioSource.Play();

    // Desativar bordas e botões para evitar conflitos de interação
    greenPlayerI_Border.SetActive(false);
    greenPlayerII_Border.SetActive(false);
    greenPlayerIII_Border.SetActive(false);
    greenPlayerIV_Border.SetActive(false);
    
    GreenPlayerI_Button.interactable = false;
    GreenPlayerII_Button.interactable = false;
    GreenPlayerIII_Button.interactable = false;
    GreenPlayerIV_Button.interactable = false;



    if (playerTurn == "GREEN" && (greenMovementBlocks.Count - greenPlayerIV_Steps) > selectDiceNumAnimation)
    {
        if (greenPlayerIV_Steps > 0) 
			{
				Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					greenPlayer_Path [i] = greenMovementBlocks [greenPlayerIV_Steps + i].transform.position;
				}

				greenPlayerIV_Steps += selectDiceNumAnimation;			

				if (selectDiceNumAnimation == 6) 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
				} 
				else 
				{
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
                    photonView.RPC("InitializeDice", RpcTarget.All);
				}
			

				//currentPlayer = GreenPlayerIV_Script_Multiplayer.greenPlayerI_ColName;
				currentPlayerName = "GREEN PLAYER IV";

				//if(greenPlayerIV_Steps + selectDiceNumAnimation == greenMovementBlocks.Count)
				if (greenPlayer_Path.Length > 1) 
				{
					//greenPlayerIV.transform.DOPath (greenPlayer_Path, 2.0f, PathType.Linear, PathMode.Full3D, 10, Color.green);
					iTween.MoveTo (greenPlayerIV, iTween.Hash ("path", greenPlayer_Path, 
															"speed", 125,"time",2.0f, 
															"easetype", "elastic", "looptype", "none", 
															"oncomplete", "OnMoveComplete", 
															"oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (greenPlayerIV, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			} 
        else 
			{
				if (selectDiceNumAnimation == 6 && greenPlayerIV_Steps == 0) 
				{
					Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];
					greenPlayer_Path [0] = greenMovementBlocks [greenPlayerIV_Steps].transform.position;
					greenPlayerIV_Steps += 1;
					playerTurn = "GREEN";
                    photonView.RPC("SyncGameState", RpcTarget.All);
					//currentPlayer = GreenPlayerIV_Script_Multiplayer.greenPlayerIV_ColName;
					currentPlayerName = "GREEN PLAYER IV";
					iTween.MoveTo (greenPlayerIV, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
			}
    }
    else
	{
			// Condition when Player Coin is reached successfully in House....(Actual Number of requigreen moves to get into the House)
			if (playerTurn == "GREEN" && (greenMovementBlocks.Count - greenPlayerIV_Steps) == selectDiceNumAnimation) 
			{
				Vector3[] greenPlayer_Path = new Vector3[selectDiceNumAnimation];

				for (int i = 0; i < selectDiceNumAnimation; i++) 
				{
					greenPlayer_Path [i] = greenMovementBlocks [greenPlayerIV_Steps + i].transform.position;
				}

				greenPlayerIV_Steps += selectDiceNumAnimation;

				playerTurn = "GREEN";
                photonView.RPC("SyncGameState", RpcTarget.All);

				//greenPlayerIV_Steps = 0;

				if (greenPlayer_Path.Length > 1) 
				{
					iTween.MoveTo (greenPlayerIV, iTween.Hash ("path", greenPlayer_Path, "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				} 
				else 
				{
					iTween.MoveTo (greenPlayerIV, iTween.Hash ("position", greenPlayer_Path [0], "speed", 125,"time",2.0f, "easetype", "elastic", "looptype", "none", "oncomplete", "OnMoveComplete", "oncompletetarget", this.gameObject));
				}
				totalGreenInHouse += 1;
				Debug.Log ("Cool !!");
				GreenPlayerIV_Button.enabled = false;
			}
			else
			{
				Debug.Log ("You need "+  (greenMovementBlocks.Count - greenPlayerIV_Steps).ToString() + " to enter into the house.");
                

				if(greenPlayerI_Steps + greenPlayerII_Steps + greenPlayerIII_Steps == 0 && selectDiceNumAnimation != 6)
				{   playerTurn = "GREEN";
					photonView.RPC("InitializeDice", RpcTarget.All);
                    photonView.RPC("SyncGameState", RpcTarget.All);
				}
			}
	}
    
}

}
