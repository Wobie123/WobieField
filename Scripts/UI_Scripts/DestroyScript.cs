using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
   public void DestroyMusic(){
        try{
            GameObject Music = GameObject.Find("Background_music");
            Destroy(Music);
        }catch{
            Debug.Log("no object");
        }
   }
}
