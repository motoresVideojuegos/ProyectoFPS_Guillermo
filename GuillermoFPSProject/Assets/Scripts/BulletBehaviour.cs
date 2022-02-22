using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public int lifeDamage;
    public float activeTime_total;
    float activeTime_actual;

    public GameObject particleExplosion;

    private void OnEnable() {
        activeTime_actual = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - activeTime_actual >= activeTime_total){
            particleExplosion.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {

        particleExplosion.transform.position = this.transform.position;
        particleExplosion.transform.rotation = this.transform.rotation;
        particleExplosion.SetActive(true);

        if(other.tag == "Enemy"){
            other.GetComponent<EnemyBehaviour>().RemoveLife(lifeDamage);
        }else if(other.tag == "Player"){
            other.GetComponent<PlayerController>().plyClass.takeDmg(lifeDamage);
        }
    }
}
