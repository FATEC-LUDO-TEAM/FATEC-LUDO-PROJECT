

using UnityEngine;
using Photon.Pun;

public class RedPlayerI_Script_Multiplayer : MonoBehaviourPun
{
    // Removendo o static para torná-la uma variável instanciada.
    public static string redPlayerI_ColName;

    void Start()
    {
        redPlayerI_ColName = "none";
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "blocks")
        {
            redPlayerI_ColName = col.gameObject.name;
            photonView.RPC("UpdateRedPlayerI_ColName", RpcTarget.All, redPlayerI_ColName);

            if (col.gameObject.name.Contains("Safe House"))
            {
                SoundManagerScript.safeHouseAudioSource.Play();
            }
        }
    }

    // RPC para sincronizar a variável redPlayerI_ColName entre todos os jogadores
    [PunRPC]
    void UpdateRedPlayerI_ColName(string colName)
    {
        redPlayerI_ColName = colName;
    }
}
