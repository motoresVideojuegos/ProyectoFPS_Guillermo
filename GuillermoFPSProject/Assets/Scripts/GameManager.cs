using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<EnemyBehaviour> enemyList;
    public static GameManager gm;
    public EndGameCanvas endGameCanvas;

    public PlayerController player;
    private int countEnemys;

    void Start()
    {
        gm = this;
        countEnemys = enemyList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endGame(){
        endGameCanvas.gameObject.SetActive(true);
        player.canMove = false;
        Time.timeScale = 0;
    }

    public void removeEnemy(){
        countEnemys--;
        if(countEnemys == 0){
            endGame();
        }
    }
}
