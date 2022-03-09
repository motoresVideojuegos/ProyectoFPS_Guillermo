using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvas : MonoBehaviour
{
    public Image LifeImage;
    public Transform playerTransform;

    // Start is called before the first frame update

    private void Update() {

        this.transform.LookAt(playerTransform);
        
    }
    public void LifeBar(float ActualLife, float maxLife){
        LifeImage.fillAmount = ActualLife / maxLife;
    }

}
