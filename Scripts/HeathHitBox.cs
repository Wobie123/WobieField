using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathHitBox : MonoBehaviour
{
    public HealthSystem health;
    public bool ishead;

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponentInParent<HealthSystem>();
       
    }

    // Update is called once per frame
    void Update()
    {
        health.ShieldRegenTime -= Time.deltaTime;
        if(health.ShieldRegenTime <= 0){//time to regen
            health.currentShield += 25f * Time.deltaTime;
            if(health.currentShield > health.MaxShield) health.currentShield = health.MaxShield;//if goes over


        }
    }


    public void TakeDamage(float amount){
        health.currentShield -= amount;

        if(health.currentShield <= 0f){
            health.currentHealth -= Mathf.Abs(health.currentShield);
            health.currentShield = 0;
        }
        //Debug.Log(health.name +" shield"+health.currentShield);
        //Debug.Log(health.name +"health" + health.currentHealth);
        health.ShieldRegenTime = health.TotalRegenTime; 
    }
}
