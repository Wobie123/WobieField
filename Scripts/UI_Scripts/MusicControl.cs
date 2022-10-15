using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    // Start is called before the first frame update
    public static MusicControl instance;
    public bool noDestroy = true;

    private void Awake(){
        if(noDestroy){
            DontDestroyOnLoad(this.gameObject);

            if(instance == null){
            instance = this;
            }
            else{
                Destroy(gameObject);
            }
        }
    }
}
