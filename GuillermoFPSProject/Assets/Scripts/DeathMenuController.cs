using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour
{

    public void restartButton(){
        SceneManager.LoadScene(0);
    }
    
    public void menuButton(){

    }

    public void exitButton(){
        Application.Quit();
    }
}
