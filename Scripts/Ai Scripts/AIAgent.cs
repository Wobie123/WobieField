using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiStateConfig config;
    
    public string tagname;
    
    public Transform AiTransform;

    public AiSensor sensor;
    public AiTargetSystem targetSystem;

    public HealthSystem healthSystem;
    public bool isGoing;
    [Header("Current State")]
    public string State;//to return a string

    [Header("animation")]
    AudioSource audioSource;
    [SerializeField] AudioClip walkSound;

    [Header("guns")]
    public GameObject AssultRifle;
    public GameObject SniperRifle;
    public GameObject Pistol;

    // Start is called before the first frame update
    void Start()
    {
        AiTransform = gameObject.transform;
        //playerTransform = GameObject.FindGameObjectWithTag(tagname).transform;
        //playerBody = playerTransform.Find("body");
        targetSystem = GetComponent<AiTargetSystem>();
        healthSystem = GetComponent<HealthSystem>();
        sensor = GetComponent<AiSensor>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChaseState());
        stateMachine.RegisterState(new AiDeathState());
        stateMachine.RegisterState(new Ai_idleState());
        stateMachine.RegisterState(new AiPatrolState());
        stateMachine.ChangeState(initialState);
        audioSource = GetComponent<AudioSource>();

        int rand = Random.Range(0,3);
        switch(rand){
            case 0:
                AssultRifle.SetActive(true);
                break;
            case 1:
                SniperRifle.SetActive(true);
                break;
            case 2:
                Pistol.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(playerTransform == null){
        //    Debug.Log("can't find");
        //    playerTransform = GameObject.FindGameObjectWithTag(tagname).transform;
        //}
        stateMachine.Update();
        //Debug.Log(name + " "+ stateMachine.currentState);
        State = stateMachine.currentState +"";
        isGoing = isMoving();

    }

    void LateUpdate(){
        /*
        //check if moving
        if(isMoving() && audioSource.isPlaying == false){
            //Debug.Log("walking");
            audioSource.volume = Random.Range(0.8f,1);
            audioSource.pitch = Random.Range(0.8f,1.1f);
            audioSource.PlayOneShot(walkSound);
            
        }*/
    }

    public bool isMoving(){
        if(navMeshAgent.velocity.magnitude > 0.01){
            return true;
        }
        return false;
    }

    public bool isDead(){
        if(State == "Death"){
            Debug.Log("dead");
            return true;
        }
        return false;
    }
}
