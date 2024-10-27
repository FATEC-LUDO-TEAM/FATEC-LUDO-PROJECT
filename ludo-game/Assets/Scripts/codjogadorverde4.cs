using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codjogadorverde4 : MonoBehaviour
{
    public static string codjogadorverde4col;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Blocks")
        {
           codjogadorverde4col = col.gameObject.name;
           if(col.gameObject.name.Contains ("verde_Posicao_Final"))
           {
             SoundManager.safeHouseAudioSource.Play();
           }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codjogadorverde4col = "none";
        
    }

}
