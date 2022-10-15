using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChaseState : AiState
{
    //public Transform playerTransform;
    //public float maxTime = 1.0f;
    //public float maxDistance = 1.0f;
    private float timer = 3;

    public AiStateId GetId(){
        return AiStateId.ChasePlayer;
    }

    public void Enter(AIAgent agent){
        timer = 3f;
    }

    public void Update(AIAgent agent){
        timer -= Time.deltaTime;
        //back to patrol
        if(!agent.targetSystem.HasTarget || timer <= 0){
            agent.stateMachine.ChangeState(AiStateId.Patrol);
        }

        if(!agent.isGoing){
            agent.stateMachine.ChangeState(AiStateId.Patrol);
        }
        if(agent.sensor.IsInSight(agent.targetSystem.Target)){
            agent.stateMachine.ChangeState(AiStateId.Idle);
        }
        if(agent.sensor.objects.Count == 0){
            //Debug.Log("in");
            agent.stateMachine.ChangeState(AiStateId.Patrol);
        }

        //if(!agent.enabled) return;

        if(!agent.navMeshAgent.hasPath && agent.targetSystem.HasTarget){
            //agent.navMeshAgent.destination = agent.playerTransform.position;
            agent.navMeshAgent.destination = agent.targetSystem.TargetPosition;
        }

        //turns the ai
        //try{
        Vector3 direction = (agent.targetSystem.TargetPosition - agent.navMeshAgent.destination);
        direction.y = 0;
        if(direction.sqrMagnitude > agent.config.maxDistance*agent.config.maxDistance){
            if(agent.navMeshAgent.pathStatus != UnityEngine.AI.NavMeshPathStatus.PathPartial){
                agent.navMeshAgent.destination = agent.targetSystem.TargetPosition;
            }
        }
        //}catch{

        //}
    }

    public void Exit(AIAgent agent){
        
    }
}
