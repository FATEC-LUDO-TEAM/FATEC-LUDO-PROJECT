using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codjogadorvermelho1 : MonoBehaviour
{
    public static string codjogadorvermelho1col;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Blocks")
        {
           codjogadorvermelho1col = col.gameObject.name;
           if(col.gameObject.name.Contains ("Vermelho_Posicao_Final"))
           {
             SoundManager.safeHouseAudioSource.Play();
           }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codjogadorvermelho1col = "none";
        
    }

}
