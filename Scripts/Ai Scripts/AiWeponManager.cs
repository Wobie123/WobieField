using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeponManager : MonoBehaviour
{

    AIAgent agent;//fsm
    public Animator animator;

    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;

    [Header("Ammo")]
    [SerializeField] int total_mag_size;
    public float reloadTime;
    private int mag_size;
    private bool isReloading = false;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    //missing aim for ai

    [Header("other")]
    public ParticleSystem muzzleFlash;
    public float bloomAngle;
    [SerializeField] AudioClip gunshot;
    [SerializeField] AudioClip reloadSound;
    //public Transform body;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponentInParent<AIAgent>();
        
        audioSource = GetComponent<AudioSource>();
        fireRateTimer = fireRate;
        mag_size = total_mag_size;
    }

    // Update is called once per frame
    void Update()
    {
        //if(agent.State == "Idle" && agent.sensor.IsInSight(agent.playerBody.gameObject)){
        //    Debug.Log(agent.State);
        //}
        //try{
        if( agent.State == "Idle" && agent.sensor.IsInSight(agent.targetSystem.Target)){//logic here
            if(ShouldFire()){
            Fire();
            audioSource.PlayOneShot(gunshot);
            muzzleFlash.Play();
            mag_size -= 1;
            //Debug.Log(mag_size);
            }
        }
        //}catch{
            
        //}
    }

    bool ShouldFire(){
        fireRateTimer += Time.deltaTime;
        if( mag_size <= 0){ //auto reload for ai
            
            if(!isReloading) StartCoroutine(Reload());
        }
      
        if(isReloading){
            return false;
        }
        if(fireRateTimer < fireRate){
            return false;
        }
        if(agent.State == "Idle"){
            return true;
        }
        return false;
    }

    private Vector3 BloomAng(Transform barrelPos){//for ai
        float randX = Random.Range(-bloomAngle,bloomAngle);
        float randY = Random.Range(-bloomAngle,bloomAngle);
        float randZ = Random.Range(-bloomAngle,bloomAngle);

        Vector3 randomRotation = new Vector3(randX,randY,randZ);
        return barrelPos.localEulerAngles + randomRotation;
    }

    void Fire(){
        fireRateTimer = 0;
        
        //barrelPos.LookAt(agent.playerTransform.position);//problem
        //barrelPos.LookAt(agent.playerBody);
        barrelPos.LookAt(agent.targetSystem.Target.transform);
        barrelPos.localEulerAngles = BloomAng(barrelPos);
        for(int i = 0; i < bulletPerShot;i++){
            GameObject currentBullet = Instantiate(bullet, barrelPos.position, barrelPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    IEnumerator Reload(){
        audioSource.PlayOneShot(reloadSound);
        isReloading = true;
        animator.SetTrigger("isReload");
        yield return new WaitForSeconds(reloadTime);
        mag_size = total_mag_size;
        isReloading = false;
    }
}
