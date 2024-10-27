using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codjogadorazul1 : MonoBehaviour
{
    public static string codjogadorazul1col;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Blocks")
        {
           codjogadorazul1col = col.gameObject.name;
           if(col.gameObject.name.Contains ("azul_Posicao_Final"))
           {
             SoundManager.safeHouseAudioSource.Play();
           }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codjogadorazul1col = "none";
        
    }

}
