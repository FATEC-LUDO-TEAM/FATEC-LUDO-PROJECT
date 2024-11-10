using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class CreateandJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    public Text feedbackText;
    public Button startGameButton;

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

    public override void OnJoinedRoom()
    {
        // Atribuir equipe automaticamente
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            AssignTeam("Red");
            feedbackText.text = "Você é o jogador vermelho!";
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            AssignTeam("Green");
            feedbackText.text = "Você é o jogador verde!";
        }

        // Oculta os campos de entrada após entrar na sala
        createInput.gameObject.SetActive(false);
        joinInput.gameObject.SetActive(false);
        startGameButton.gameObject.SetActive(true);
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
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
        {
            SceneManager.LoadScene("LudoMultiplayer");
        }
        else
        {
            feedbackText.text = "Aguardando o segundo jogador...";
        }
    }
}
