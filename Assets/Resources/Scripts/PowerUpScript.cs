using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour {

   void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.tag == "Player") {
         if(gameObject.tag == "DoubleJump") {
            other.gameObject.SendMessage("activateDoubleJump");
            GameObject.Destroy(gameObject);
         }
      }
   }
}
