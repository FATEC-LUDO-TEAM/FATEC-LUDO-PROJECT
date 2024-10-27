using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codjogadorverde1 : MonoBehaviour
{
    public static string codjogadorverde1col;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Blocks")
        {
           codjogadorverde1col = col.gameObject.name;
           if(col.gameObject.name.Contains ("verde_Posicao_Final"))
           {
             SoundManager.safeHouseAudioSource.Play();
           }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codjogadorverde1col = "none";
        
    }

}
