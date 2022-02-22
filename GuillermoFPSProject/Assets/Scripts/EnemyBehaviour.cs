using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    EnemyClass enemyClass;

    UnityEngine.AI.NavMeshAgent nav;
    public Transform player;
    public WeaponController weaponController;
    // Start is called before the first frame update
    void Start()
    {
        enemyClass = GetComponent<EnemyClass>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        nav.speed = enemyClass.velocityEnemy;
        enemyClass.currentHealth = enemyClass.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement(){
        if(Vector3.Distance( player.position, transform.position) < enemyClass.range && Vector3.Distance( player.position, transform.position) > enemyClass.rangeShoot){
            nav.isStopped = false;
            nav.SetDestination(player.transform.position);

        }else if(Vector3.Distance( player.position, transform.position) < enemyClass.rangeShoot){
            nav.isStopped = true;
            transform.LookAt(player);
            if (enemyClass.shootReload >= enemyClass.fireCad){
                this.weaponController.Fire();
                enemyClass.shootReload = 0;
            }
        
            enemyClass.shootReload += enemyClass.fireVelocity * Time.deltaTime;

        }else{
            nav.isStopped = true;
        }
    }
    public void RemoveLife(int dmg){
        enemyClass.currentHealth -= dmg;

        if(enemyClass.currentHealth <= 0){
            player.GetComponent<PlayerController>().plyClass.addPoints(enemyClass.points);
            Destroy(gameObject);
        }
    }

}
