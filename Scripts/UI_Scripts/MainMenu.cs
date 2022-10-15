using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public string str_name;
   public bool HasBackKey;

   public void NextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);     
   }

   public void BackScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);     
   }

   public void SpecificScene(){
        SceneManager.LoadScene(str_name);     
   }

   public void QuitGame(){
    Debug.Log("quit");
    Application.Quit();
   }

   void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape) && HasBackKey){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
      }
      
    }
}
