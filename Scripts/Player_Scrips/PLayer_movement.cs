using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer_movement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    AudioSource audioSource;
    [SerializeField] AudioClip walkSound;

    public float speed = 6f;
    public float jumpHeight = 7.0f;
    [HideInInspector] public Vector3 dir;
    float hzInput,vInput;

    private bool groundedPlayer;
    Vector3 CharacterPos;
    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    [Header("Weapons")]
    [Tooltip("Only if avaiable")]
    public bool isWeaponSwitch = true;
    public GameObject WeaponSwitchUi;
    public GameObject AssultRifle;
    public GameObject SniperRifle;
    public GameObject Pistol;


    void Start(){
        audioSource = GetComponent<AudioSource>();

        if(isWeaponSwitch && WeaponSwitchUi != null){
            WeaponSwitchUi.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            PauseMenu.FreezePause = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        animator.SetFloat("speed", Mathf.Abs(hzInput) +Mathf.Abs(vInput));
        
        if( Mathf.Abs(hzInput) +Mathf.Abs(vInput) > 0.01f && groundedPlayer &&audioSource.isPlaying == false){
            audioSource.volume = Random.Range(0.8f,1);
            audioSource.pitch = Random.Range(0.8f,1.1f);
            audioSource.PlayOneShot(walkSound);
        }

        dir = transform.forward * vInput + transform.right * hzInput;
        controller.Move(dir.normalized * speed * Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;

        //groundedPlayer = controller.isGrounded;
        //Debug.Log(groundedPlayer);
        if (velocity.y < 0)
        {
            animator.SetBool("isJump",false);
            velocity.y = 0f;
            groundedPlayer = true;
        }else{
            groundedPlayer = false;
        }
        if(Input.GetButtonDown("Jump") && groundedPlayer){
            
            velocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            animator.SetBool("isJump",true);    
        }
        controller.Move(velocity*Time.deltaTime);

        //dir = transform.forward * vInput + transform.right * hzInput;
        //controller.Move(dir * speed * Time.deltaTime);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CharacterPos,controller.radius - 0.05f);
    }

    public void SelectWepon(int num){
        if(num == 0){
            AssultRifle.SetActive(true);
        }
        if(num == 1){
            SniperRifle.SetActive(true);
        }
        if(num == 2){
            Pistol.SetActive(true);
        }
        WeaponSwitchUi.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.FreezePause = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
