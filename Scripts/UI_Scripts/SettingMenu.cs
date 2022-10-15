using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;

    private static float SoundVolume = 1;//to save it
    private static bool is_FullScreen;
    private static int Gdrop;

    private Slider soundSlider;
    private Toggle tog;
    private TMPro.TMP_Dropdown GraphicDropdown;

    Resolution[] resolutions;

    void Awake(){

          soundSlider = GetComponentInChildren<Slider>();
          tog = GetComponentInChildren<Toggle>();
          GraphicDropdown = this.transform.Find("GraphicDropdown").GetComponent<TMPro.TMP_Dropdown>();

          //audioMixer.SetFloat("Volume", SoundVolume);//sets audioMixer
          //soundSlider.value = SoundVolume;
          audioMixer.SetFloat("Volume", Mathf.Log10(SoundVolume)*20);
          soundSlider.value = SoundVolume;

          tog.isOn = is_FullScreen;
          GraphicDropdown.value = Gdrop;
          
    }

    void Start(){


          resolutions = Screen.resolutions;
          resolutionDropdown.ClearOptions();

          List<string> options = new List<string>();
          
          int currentResolutionIndex = 0;
          for(int i = 0; i < resolutions.Length;i++){
               string option = resolutions[i].width + " x " + resolutions[i].height;
               options.Add(option);

               if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height){
                    currentResolutionIndex = i;
               }
          }
          resolutionDropdown.AddOptions(options);
          resolutionDropdown.value = currentResolutionIndex;
          resolutionDropdown.RefreshShownValue();
    }

    private void Update(){
    }

   public void SetVolume(float volume){
        SoundVolume = volume;
        audioMixer.SetFloat("Volume", Mathf.Log10(SoundVolume)*20);
   }

   public void SetQuality(int qualityIndex){
        QualitySettings.SetQualityLevel(qualityIndex);
        Gdrop = qualityIndex;
   }

   public void SetFullScreen(bool isFullscreen){
       Screen.fullScreen = isFullscreen;
       is_FullScreen = isFullscreen;
   }

   public void SetResolution(int resolutionIndex){
     Resolution resolution = resolutions[resolutionIndex];
     Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);

   }
}
