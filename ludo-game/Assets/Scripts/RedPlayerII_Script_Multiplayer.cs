

using UnityEngine;
using Photon.Pun;

public class RedPlayerII_Script_Multiplayer : MonoBehaviourPun
{
    // Removendo o static para torná-la uma variável instanciada.
    public  static string redPlayerII_ColName;

    void Start()
    {
        redPlayerII_ColName = "none";
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "blocks")
        {
            redPlayerII_ColName = col.gameObject.name;
            photonView.RPC("UpdateRedPlayerII_ColName", RpcTarget.All, redPlayerII_ColName);

            if (col.gameObject.name.Contains("Safe House"))
            {
                SoundManagerScript.safeHouseAudioSource.Play();
            }
        }
    }

    // RPC para sincronizar a variável redPlayerII_ColName entre todos os jogadores
    [PunRPC]
    void UpdateRedPlayerII_ColName(string colName)
    {
        redPlayerII_ColName = colName;
    }
}
