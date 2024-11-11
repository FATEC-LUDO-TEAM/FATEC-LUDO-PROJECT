

using UnityEngine;
using Photon.Pun;

public class RedPlayerIV_Script_Multiplayer : MonoBehaviourPun
{
    // Removendo o static para torná-la uma variável instanciada.
    public static string redPlayerIV_ColName;

    void Start()
    {
        redPlayerIV_ColName = "none";
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "blocks")
        {
            redPlayerIV_ColName = col.gameObject.name;
            photonView.RPC("UpdateRedPlayerIV_ColName", RpcTarget.All, redPlayerIV_ColName);

            if (col.gameObject.name.Contains("Safe House"))
            {
                SoundManagerScript.safeHouseAudioSource.Play();
            }
        }
    }

    // RPC para sincronizar a variável redPlayerIV_ColName entre todos os jogadores
    [PunRPC]
    void UpdateRedPlayerIV_ColName(string colName)
    {
        redPlayerIV_ColName = colName;
    }
}
