using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    private Slider BlueScore;
    private Slider RedScore;

    public int currentRedScore = 0;
    public int currentBlueScore = 0;
    private int MaxScore = 30;

    public TextMeshProUGUI Victory;
    public TextMeshProUGUI Defeat;
    public TextMeshProUGUI RedScoreNumber;
    public TextMeshProUGUI BlueScoreNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        BlueScore = transform.Find("BlueSlider").GetComponent<Slider>();
        RedScore = transform.Find("RedSlider").GetComponent<Slider>();

        Victory.enabled = false;
        Defeat.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(currentRedScore < MaxScore) RedScore.value = currentRedScore;
        if(currentBlueScore < MaxScore)BlueScore.value = currentBlueScore;

        if(BlueScoreNumber != null && RedScoreNumber != null){
            BlueScoreNumber.text = currentBlueScore.ToString();
            RedScoreNumber.text = currentRedScore.ToString();
        }
    }

    public void SetScore(bool isRed){
        if(isRed){
            currentRedScore +=1;
        }
        if(!isRed){
            currentBlueScore +=1;  
        }
        if(currentRedScore >= MaxScore && Victory.enabled == false){
            Defeat.enabled = true;
        }
        if(currentBlueScore >= MaxScore && Defeat.enabled == false){
            Victory.enabled = true;
        }
        
    }
}
