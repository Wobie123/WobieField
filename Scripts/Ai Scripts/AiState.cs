using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AiStateId{
    ChasePlayer,
    Death,
    Idle,
    Patrol
}


public interface AiState 
{
    AiStateId GetId();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}
