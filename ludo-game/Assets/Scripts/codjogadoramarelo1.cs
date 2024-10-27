using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codjogadoramarelo1 : MonoBehaviour
{
    public static string codjogadoramarelo1col;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Blocks")
        {
           codjogadoramarelo1col = col.gameObject.name;
           if(col.gameObject.name.Contains ("amarelo_Posicao_Final"))
           {
             SoundManager.safeHouseAudioSource.Play();
           }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codjogadoramarelo1col = "none";
        
    }

}
