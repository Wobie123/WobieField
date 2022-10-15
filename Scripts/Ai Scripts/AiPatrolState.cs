using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiPatrolState : AiState
{
    bool walkPointSet;
    

   public AiStateId GetId(){
       return AiStateId.Patrol;
   }

   public void Enter(AIAgent agent){
   }

   public void Update(AIAgent agent){
        Patroling(agent);
        //from idle---------------------------------------------
        //chase if get close
        //if(agent.sensor.objects[1] != null){
             
        if(agent.targetSystem.HasTarget){
            Vector3 playerDerection = agent.targetSystem.TargetPosition - agent.transform.position;
            if(playerDerection.magnitude > agent.config.maxSightDistance){
                return;
            }

            Vector3 agentDerection = agent.transform.forward;

            playerDerection.Normalize();
        
        //changes the state
            float dotProduct = Vector3.Dot(playerDerection,agentDerection);
            if(dotProduct > 0.0f){
                agent.stateMachine.ChangeState(AiStateId.ChasePlayer);
        }

        }
        //--------------------------------------------------------------
   }

   public void Exit(AIAgent agent){

   }

   private void Patroling(AIAgent agent){
        bool isWalking = agent.isGoing;
        if(!walkPointSet ) SeachWalkPoint(agent);

        if(walkPointSet) agent.navMeshAgent.SetDestination(agent.config.walkPoint);

        Vector3 distanceToWalkPoint = agent.AiTransform.position - agent.config.walkPoint;

        if(distanceToWalkPoint.magnitude <1.0f || !isWalking){
            walkPointSet = false;
        }

   }

   private void SeachWalkPoint(AIAgent agent){
        //randompoint
        float range = agent.config.walkPointRange;
        float randomZ = Random.Range(-range,range);
        float randomX = Random.Range(-range,range);

        agent.config.walkPoint = new Vector3(agent.AiTransform.position.x + randomX,agent.AiTransform.position.y,agent.AiTransform.position.z+ randomZ);

        if(Physics.Raycast(agent.config.walkPoint, -agent.AiTransform.up,2f, agent.config.Ground)){
            walkPointSet = true;
        }
   }
  
}
