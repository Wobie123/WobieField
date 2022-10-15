using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    //private BulletScript bulletScript;

    public float MaxHealth;
    [HideInInspector]public float currentHealth;
    public float MaxShield;
    [HideInInspector]public float currentShield;
    public float ShieldRegenTime;
    [HideInInspector]public float TotalRegenTime;
    //public bool isAi;

    UiHealthBar healthBar;

    private GameObject[] ChildObj = new GameObject[4];

    
    [Tooltip("for ai optional")]
    public AIAgent agent;
    // Start is called before the first frame update
    void Start()
    {

         for(int i = 0; i < 3;i++){
            ChildObj[i] = transform.GetChild(i).gameObject;
         }

        currentHealth = MaxHealth;
        currentShield = MaxShield;
        TotalRegenTime = ShieldRegenTime;
        //if(isAi) 
        healthBar = GetComponentInChildren<UiHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(isAi){
        healthBar.SetHealthBarPercentage(currentHealth/MaxHealth, false);
        healthBar.SetHealthBarPercentage(currentShield/MaxShield, true);

        if(currentHealth <= 0){
            currentHealth = 0;
            //Debug.Log("died");
            healthBar.gameObject.SetActive(false);

            if(ChildObj[0].GetComponent<Rigidbody>() == null){
                for(int i = 0; i < 3;i++){
                    ChildObj[i].AddComponent<Rigidbody>();
                    ChildObj[i].GetComponent<Rigidbody>().mass = 3;
                    ChildObj[i].GetComponent<Rigidbody>().AddExplosionForce(10,transform.position,5,1);
                }
            }

            if(agent != null){
                AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
                agent.stateMachine.ChangeState(AiStateId.Death);
            }
            Destroy(gameObject,4);
        }
        /*
        // end of if ai
        }else{//player
            if(currentHealth <= 0){
            currentHealth = 0;
            Debug.Log("died");
            }
        }
        */
    }

}
