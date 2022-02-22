using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
   [Header("Movement")]
    public float velocity;
    public float jumpForce;

   [Header("Stats")]
   public int maxHealth;
   public int currentHealth;
   public int playerScore;

   public void takeDmg(int dmg){
       currentHealth -= dmg;

        if(currentHealth <= 0){
           Debug.Log("Muerto");
        }
   }

   public void addLife(int heal){
      currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);

   }

   public void addPoints(int points){
      playerScore += points;
   }
}
