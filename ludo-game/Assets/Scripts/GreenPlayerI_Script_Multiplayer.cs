

using UnityEngine;
using Photon.Pun;

public class GreenPlayerI_Script_Multiplayer : MonoBehaviourPun
{
    // Removendo o static para torná-la uma variável instanciada.
    public static string greenPlayerI_ColName;

    void Start()
    {
        greenPlayerI_ColName = "none";
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "blocks")
        {
            greenPlayerI_ColName = col.gameObject.name;
            photonView.RPC("UpdateGreenPlayerI_ColName", RpcTarget.All, greenPlayerI_ColName);

            if (col.gameObject.name.Contains("Safe House"))
            {
                SoundManagerScript.safeHouseAudioSource.Play();
            }
        }
    }

    // RPC para sincronizar a variável greenPlayerI_ColName entre todos os jogadores
    [PunRPC]
    void UpdateGreenPlayerI_ColName(string colName)
    {
        greenPlayerI_ColName = colName;
    }
}
