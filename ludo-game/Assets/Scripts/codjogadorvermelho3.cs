using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codjogadorvermelho3 : MonoBehaviour
{
    public static string codjogadorvermelho3col;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Blocks")
        {
           codjogadorvermelho3col = col.gameObject.name;
           if(col.gameObject.name.Contains ("Vermelho_Posicao_Final"))
           {
             SoundManager.safeHouseAudioSource.Play();
           }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codjogadorvermelho3col = "none";
        
    }

}
