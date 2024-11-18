using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System.Collections;


public class CreateandJoinRooms : MonoBehaviourPunCallbacks
{

    private string[] availableScenes = { "LudoMultiplayer", "LudoMultiMago", "LudoMultiDragao", "LudoMultiArqueiro"}; // Substitua pelos nomes reais das cenas
    private const string ScenePropertyKey = "SelectedScene";

    public InputField createInput;
    public InputField joinInput;
    public Text feedbackText;

    public Text playerCountText; 

    public Button startGameButton;

    public GameObject CreateJoin;

    private const int maxPlayers = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = maxPlayers };
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }


    public void ExitLobby()
{
    Debug.Log("Saindo do lobby e desconectando...");
    PhotonNetwork.LeaveRoom(); // Sai da sala atual
    PhotonNetwork.Disconnect(); // Desconecta do Photon

    StartCoroutine(ReturnToMenuAfterDisconnect());
}

private IEnumerator ReturnToMenuAfterDisconnect()
{
    // Aguarda até que o jogador esteja desconectado
    while (PhotonNetwork.IsConnected)
    {
        yield return null;
    }

    // Carrega o menu principal
    SceneManager.LoadScene("CoreMenu");

}

public override void OnMasterClientSwitched(Player newMasterClient)
{
    Debug.Log("O Master Client mudou para: " + newMasterClient.NickName);

    // Define o novo Master Client como jogador vermelho
    if (PhotonNetwork.LocalPlayer.IsMasterClient)
    {
        AssignTeam("Red"); // Define a equipe como vermelho
        feedbackText.text = "Você se tornou o jogador vermelho, pois o antigo Master saiu!";
    }

    // Atualiza o texto de contagem de jogadores
    UpdatePlayerCountText();
}

    public override void OnJoinedRoom()
    {
        // Atribuir equipe automaticamente
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            AssignTeam("Red");
            feedbackText.text = "Você é o jogador vermelho!";
             createInput.gameObject.SetActive(false);
            joinInput.gameObject.SetActive(false);
            CreateJoin.SetActive(false);

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            AssignTeam("Green");
            feedbackText.text = "Você é o jogador verde!";
            createInput.gameObject.SetActive(false);
        joinInput.gameObject.SetActive(false);
         CreateJoin.SetActive(false);   
        }
        UpdatePlayerCountText();

        // Oculta os campos de entrada após entrar na sala
        createInput.gameObject.SetActive(false);
        joinInput.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(true);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerCountText(); // Atualiza o texto quando um jogador entra
    }

     public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerCountText(); // Atualiza o texto quando um jogador sai
    }

    
    private void UpdatePlayerCountText()
    {
        int playerCount = PhotonNetwork.CurrentRoom != null ? PhotonNetwork.CurrentRoom.PlayerCount : 0;

        if (playerCount == 1)
        {
            playerCountText.text = "1 jogador na sala. Aguardando outro jogador...";
        }
        else if (playerCount == 2)
        {
            playerCountText.text = "2 jogadores na sala. Aperte Start Game para começar!";
        }
        else
        {
            playerCountText.text = "Aguardando jogadores...";
        }
    }


    private void AssignTeam(string teamColor)
    {
        var playerProperties = new ExitGames.Client.Photon.Hashtable { { "Team", teamColor } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        feedbackText.text = "Falha ao entrar na sala. Tente novamente!";
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        feedbackText.text = "Falha ao criar a sala. Tente outro nome!";
    }

    private void StartGame()
    {
       if (PhotonNetwork.CurrentRoom.PlayerCount == 2) // Substitua 2 pelo número máximo de jogadores
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // Escolha uma cena aleatória
                string selectedScene = availableScenes[Random.Range(0, availableScenes.Length)];

                // Salve a cena escolhida como propriedade da sala
                PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { ScenePropertyKey, selectedScene } });

                // Carregue a cena para todos
                PhotonNetwork.LoadLevel(selectedScene);
            }
            else
            {
                // Aguarda o MasterClient definir a cena
                string selectedScene = PhotonNetwork.CurrentRoom.CustomProperties[ScenePropertyKey] as string;
                if (!string.IsNullOrEmpty(selectedScene))
                {
                    PhotonNetwork.LoadLevel(selectedScene);
                }
                else
                {
                    Debug.Log("Aguardando o Master Client selecionar a cena...");
                }
            }
        }
        else
        {
            Debug.Log("Aguardando outros jogadores...");
        }
    
    }
}