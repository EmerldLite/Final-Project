using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    public int livesValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary" || other.CompareTag("Enemy"))
        {
            return;
        }

        if (explosion != null)
        {
            Instantiate (explosion, transform.position, transform.rotation);
        }

        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.SubtractLives();
        }
        
        //Transfer over most of this into a new script. This script is in context to the asteroid's perspective. (i.e. if enemy hits player "x" happens)
        gameController.AddLives (livesValue);

        gameController.AddScore (scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
