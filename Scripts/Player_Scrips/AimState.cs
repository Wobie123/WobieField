using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimState : MonoBehaviour//player only
{
    //public Cinemachine.AxisState xAxis, yAxis;
    float xAxis, yAxis;
    [SerializeField] float mouseSense = 1;
    [SerializeField] Transform camFollowPos;

    public CinemachineVirtualCamera vCam;
    public float adsFov = 40;
    public float zoom = 20;
    private float current;
    private float original;
    public float fovSmoothSpeed = 10;
    [HideInInspector] public bool isAim = false;
    
    public Transform aimPos;//use by weapon manager
    
    //[HideInInspector] public Vector3 actualAimPos;
    //[SerializeField] float aimSmoothSpeed = 20;
    //[SerializeField] LayerMask aimMask;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;//locks the Cursor
        current = vCam.m_Lens.FieldOfView;
        original = current;
    }

    // Update is called once per frame
    void Update()
    {
        //xAxis.Update(Time.deltaTime);
        //yAxis.Update(Time.deltaTime);
        
        
        xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;

        yAxis = Mathf.Clamp(yAxis,-15,10);
        

        if(Input.GetKeyDown(KeyCode.Mouse1)){
            isAim = true;
            current = current -zoom;
            
        }else if(Input.GetKeyUp(KeyCode.Mouse1)){
            isAim = false;
            current = current +zoom;  
        }else if(current != original && isAim == false){
            current = original;
        }
        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView,current,fovSmoothSpeed*Time.deltaTime);

        //Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height /2);
        //Ray ray = Camera.main.ScreenPointToRay(screenCentre);
        
    }

    private void LateUpdate(){

        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y,camFollowPos.localEulerAngles.z);
        
        if(!PauseMenu.GameIsPaused){
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
        }
    }
}
