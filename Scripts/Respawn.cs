using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject spawnObj;
    public GameObject checker;
    public ScoreScript Score;
    [Tooltip("if have score")]
    public bool isBlue;
    
    // Start is called before the first frame update
    void Start()
    {
        checker = Instantiate(spawnObj,transform.position,transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
       if(checker == null){
            //if(Score != null){
                Score.SetScore(isBlue);
            //}
            checker = Instantiate(spawnObj,transform.position,transform.rotation);
       }

    }

    
}
