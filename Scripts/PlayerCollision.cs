using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int nbCoins = 0;
    public GameObject pickupEffect;
    public GameObject mobEffect;
    private bool canInstantiate;

    private void OnControllerColliderHit(ControllerColliderHit collision) // Check la collision, plus adapté pour le characterController
    {
        // Si la collision à le tag Coin
        if (collision.gameObject.tag == "Coin")
        {
            GameObject go = Instantiate(pickupEffect, collision.transform.position, Quaternion.identity); 
            Destroy(go, 0.5f);
            // Détruire le gameObject de la collision, donc le coin
            Destroy(collision.gameObject);
            // Incrémenter nbCoins
            nbCoins++;
        }
        // Si la collision à le tag Mob
        if (collision.gameObject.tag == "Mob" && canInstantiate)
        {
            canInstantiate = false;
            GameObject go = Instantiate(mobEffect, collision.transform.position, Quaternion.identity);
            Destroy(go, 0.6f);
            // Détruire le gameObject de la collision, donc le mob après 0.2 seconde
            Destroy(collision.gameObject.transform.parent.gameObject, 0.2f);
            StartCoroutine("ResetInstantiate");
        }
        // Si le tag de la collision est Hurt
        if (collision.gameObject.tag == "Hurt")
        {
            Debug.Log("Aie !");
        }
    }

    IEnumerator ResetInstantiate()
    {
        yield return new WaitForSeconds(0.8);
        canInstantiate = true;
    }

    // private void OnCollisionEnter(Collision collision){}
}
