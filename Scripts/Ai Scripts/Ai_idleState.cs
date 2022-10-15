using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_idleState : AiState
{
    private float timer;
    private float maxTimer = 3f;

   public AiStateId GetId(){
       return AiStateId.Idle;
   }

   public void Enter(AIAgent agent){
        timer = maxTimer;
   }
    //going to be used when attacking
   public void Update(AIAgent agent){
        timer -=Time.deltaTime;
        agent.navMeshAgent.isStopped = true;
        agent.navMeshAgent.ResetPath();

        //changes the state 

        if(agent.targetSystem.HasTarget){
            Vector3 playerDerection = agent.targetSystem.TargetPosition - agent.transform.position;
            if(playerDerection.magnitude > agent.config.maxSightDistance){
                return;
            }
        

            Vector3 agentDerection = agent.transform.forward;

            playerDerection.Normalize();
            float dotProduct = Vector3.Dot(playerDerection,agentDerection);
            if(dotProduct > 0.0f){
                agent.navMeshAgent.isStopped = false;
                agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
            }
        }

        //to chase
        //if(agent.targetSystem.HasTarget && !agent.sensor.IsInSight(agent.targetSystem.Target)){
        //    agent.navMeshAgent.isStopped = false;
        //    agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        //}

        if(!agent.targetSystem.HasTarget || timer <= 0){
            agent.navMeshAgent.isStopped = false;
            agent.stateMachine.ChangeState(AiStateId.Patrol);
        }
        if(agent.sensor.objects.Count == 0){
            agent.navMeshAgent.isStopped = false;
            agent.stateMachine.ChangeState(AiStateId.Patrol);
        }

            Vector3 direction = (agent.targetSystem.TargetPosition - agent.AiTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            agent.AiTransform.rotation = Quaternion.Slerp(agent.AiTransform.rotation, lookRotation, Time.deltaTime * 3);
        
   }

   public void Exit(AIAgent agent){

   }
}
