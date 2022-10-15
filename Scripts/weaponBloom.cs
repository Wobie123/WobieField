using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponBloom : MonoBehaviour
{
    public float BloomAngle;//org 3f
    public float aimBloomMultiplier;//0.5f
    private float currentBloom;

    public AimState aim;


    // Start is called before the first frame update
    void Start()
    {

    }

    public Vector3 BloomAng(Transform barrelPos){
        if(!aim.isAim) currentBloom = BloomAngle;
        if(aim.isAim) currentBloom *=aimBloomMultiplier;

        float randX = Random.Range(-currentBloom,currentBloom);
        float randY = Random.Range(-currentBloom,currentBloom);
        float randZ = Random.Range(-currentBloom,currentBloom);

        Vector3 randomRotation = new Vector3(randX,randY,randZ);
        return barrelPos.localEulerAngles + randomRotation;
    }
}
