using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject currentAmmo;
    public GameObject maxAmmo;
    public Image LifeImage;

    public GameObject scoreText;

    private void Start() {
        scoreText.GetComponent<Text>().text = "0";
    }

    public void setCurrentAmmo(int ammo){
        currentAmmo.GetComponent<Text>().text = ammo.ToString();
    }

    public void setMaxAmmo(int ammo){
        maxAmmo.GetComponent<Text>().text = ammo.ToString();
    }

    public void LifeBar(float ActualLife, float maxLife){
        LifeImage.fillAmount = ActualLife / maxLife;
    }

    public void UpdateScore(int score){
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
