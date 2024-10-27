using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codjogadorazul3 : MonoBehaviour
{
    public static string codjogadorazul3col;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Blocks")
        {
           codjogadorazul3col = col.gameObject.name;
           if(col.gameObject.name.Contains ("azul_Posicao_Final"))
           {
             SoundManager.safeHouseAudioSource.Play();
           }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codjogadorazul3col = "none";
        
    }

}
