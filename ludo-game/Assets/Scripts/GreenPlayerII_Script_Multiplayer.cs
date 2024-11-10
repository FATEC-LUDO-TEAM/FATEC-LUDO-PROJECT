

using UnityEngine;
using Photon.Pun;

public class GreenPlayerII_Script_Multiplayer : MonoBehaviourPun
{
    // Removendo o static para torná-la uma variável instanciada.
    public static string greenPlayerII_ColName;

    void Start()
    {
        greenPlayerII_ColName = "none";
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "blocks")
        {
            greenPlayerII_ColName = col.gameObject.name;
            photonView.RPC("UpdateGreenPlayerII_ColName", RpcTarget.All, greenPlayerII_ColName);

            if (col.gameObject.name.Contains("Safe House"))
            {
                SoundManagerScript.safeHouseAudioSource.Play();
            }
        }
    }

    // RPC para sincronizar a variável greenPlayerII_ColName entre todos os jogadores
    [PunRPC]
    void UpdateGreenPlayerII_ColName(string colName)
    {
        greenPlayerII_ColName = colName;
    }
}
