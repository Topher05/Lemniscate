using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   public void Setup(){
    gameObject.SetActive(true);

   }
   public void QuitGame()
   {
    Debug.Log("quit");
    Application.Quit();
   }
   public void RestartButton()
   {
    SceneManager.LoadScene(1);
   }
}
