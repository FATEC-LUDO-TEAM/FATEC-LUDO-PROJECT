using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codjogadoramarelo2 : MonoBehaviour
{
    public static string codjogadoramarelo2col;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Blocks")
        {
           codjogadoramarelo2col = col.gameObject.name;
           if(col.gameObject.name.Contains ("amarelo_Posicao_Final"))
           {
             SoundManager.safeHouseAudioSource.Play();
           }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codjogadoramarelo2col = "none";
        
    }

}
