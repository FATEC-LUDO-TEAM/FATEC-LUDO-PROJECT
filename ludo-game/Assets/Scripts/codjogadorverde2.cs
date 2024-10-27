using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class codjogadorverde2 : MonoBehaviour
{
    public static string codjogadorverde2col;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Blocks")
        {
           codjogadorverde2col = col.gameObject.name;
           if(col.gameObject.name.Contains ("verde_Posicao_Final"))
           {
             SoundManager.safeHouseAudioSource.Play();
           }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        codjogadorverde2col = "none";
        
    }

}
