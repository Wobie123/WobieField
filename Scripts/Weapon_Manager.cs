using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Manager : MonoBehaviour
{
    public Animator animator;
    public UiHealthBar healthBar;

    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;
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
    public bool isSniper;
    public AimState aim;

    [Header("other")]
    public ParticleSystem muzzleFlash;
    public weaponBloom bloom;
    [SerializeField] AudioClip gunshot;
    [SerializeField] AudioClip reloadSound;
    AudioSource audioSource;
    public GameObject scopeOverlay;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fireRateTimer = fireRate;
        
        if(healthBar != null){
            healthBar.MaxAmmo = total_mag_size;
        }

        mag_size = total_mag_size;
        if(isSniper) aim.zoom +=15;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShouldFire() && !PauseMenu.GameIsPaused){
            Fire();
            audioSource.PlayOneShot(gunshot);
            muzzleFlash.Play();
            mag_size -= 1;
            //Debug.Log(mag_size);
        }

        if(isSniper){
            scopeOverlay.SetActive(aim.isAim);
        }

        if(healthBar != null){
            healthBar.currentAmmo = mag_size;
        }

    }

    bool ShouldFire(){
        fireRateTimer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.R) || mag_size <= 0){ //reload button
            
            if(!isReloading) StartCoroutine(Reload());
        }
      
        if(isReloading){
            return false;
        }
        if(fireRateTimer < fireRate){
            return false;
        }
        if(semiAuto && Input.GetKeyDown(KeyCode.Mouse0)){
            return true;
        }
        if(!semiAuto && Input.GetKey(KeyCode.Mouse0)){
            return true;
        }

        return false;
    }

    void Fire(){
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        barrelPos.localEulerAngles = bloom.BloomAng(barrelPos);
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
