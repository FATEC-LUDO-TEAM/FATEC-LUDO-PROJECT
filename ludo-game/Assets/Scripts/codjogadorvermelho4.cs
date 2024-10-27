using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codjogadorvermelho4 : MonoBehaviour
{
    public static string codjogadorvermelho4col;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Blocks")
        {
           codjogadorvermelho4col = col.gameObject.name;
           if(col.gameObject.name.Contains ("Vermelho_Posicao_Final"))
           {
             SoundManager.safeHouseAudioSource.Play();
           }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codjogadorvermelho4col = "none";
        
    }

}
