using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public void PlayGame()
  {
    SceneManager.LoadScene("Tutorial1");
  }

  public void QuitGame()
  {
    Debug.Log("QUIT!");
    Application.Quit();
  }
     
  public void ChooseLevel()
  {
    SceneManager.LoadScene("Levels");
  }

  public void GotoTutorial()
  {
    SceneManager.LoadScene("TutorialLevels");
  }
}
