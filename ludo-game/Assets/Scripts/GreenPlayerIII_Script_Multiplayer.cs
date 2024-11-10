

using UnityEngine;
using Photon.Pun;

public class GreenPlayerIII_Script_Multiplayer : MonoBehaviourPun
{
    // Removendo o static para torná-la uma variável instanciada.
    public static string greenPlayerIII_ColName;

    void Start()
    {
        greenPlayerIII_ColName = "none";
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "blocks")
        {
            greenPlayerIII_ColName = col.gameObject.name;
            photonView.RPC("UpdateGreenPlayerIII_ColName", RpcTarget.All, greenPlayerIII_ColName);

            if (col.gameObject.name.Contains("Safe House"))
            {
                SoundManagerScript.safeHouseAudioSource.Play();
            }
        }
    }

    // RPC para sincronizar a variável greenPlayerIII_ColName entre todos os jogadores
    [PunRPC]
    void UpdateGreenPlayerIII_ColName(string colName)
    {
        greenPlayerIII_ColName = colName;
    }
}
