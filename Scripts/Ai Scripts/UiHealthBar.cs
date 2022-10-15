using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiHealthBar : MonoBehaviour
{
    public Transform target;
    public Image ShieldBack;
    public Image ShieldFore;
    public Image HealthBack;
    public Image HealthFore;
    public Vector3 offset;
    public bool notAi = false;

    [HideInInspector] public int currentAmmo;
    [HideInInspector] public int MaxAmmo;
    public TextMeshProUGUI MaxAmmo_txt;
    public TextMeshProUGUI currentAmmo_txt;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!notAi){
        Vector3 direction = (target.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(direction,Camera.main.transform.forward) <= 0.0f;
        ShieldBack.enabled = !isBehind;
        ShieldFore.enabled = !isBehind;
        HealthBack.enabled = !isBehind;
        HealthFore.enabled = !isBehind;

        transform.position = Camera.main.WorldToScreenPoint(target.position+ offset);
        }

        if(MaxAmmo_txt != null && currentAmmo_txt != null){
            MaxAmmo_txt.text = "/"+ MaxAmmo.ToString();
            currentAmmo_txt.text = currentAmmo.ToString();
        }
    }

    public void SetHealthBarPercentage(float percentage, bool isShield){
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth*percentage;
        if(isShield){
            ShieldFore.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,width);
        }else{
            HealthFore.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,width);
        }
        
    }
}
