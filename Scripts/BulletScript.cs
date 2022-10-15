using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    public int DamageMin;
    public int DamageMax;
    public float crit;
    //public ParticleSystem Flash;
    
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(this.gameObject, timeToDestroy);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision){
        //Debug.Log(collision.gameObject.name);
        if(collision.gameObject.GetComponent<HeathHitBox>()){
            //Flash.Play();
            HeathHitBox box = collision.gameObject.GetComponent<HeathHitBox>();
            int num = Random.Range(DamageMin,DamageMax+1);
            float RandomRange = (float)num;

            if(box.ishead){//headshot
                RandomRange *= crit;
                box.TakeDamage(RandomRange);
            }else{
                box.TakeDamage(RandomRange);   
            }
        }
        Destroy(this.gameObject);
    }
}
