using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu()]
public class AiStateConfig : ScriptableObject
{
    [Header("Chase")]
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float maxSightDistance = 5.0f;

    [Header("Patrol")]
    public Vector3 walkPoint;
    public float walkPointRange;

    public LayerMask Ground;

}
