using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLives : MonoBehaviour
{
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

        if (other.tag == "Player")
        {
            Destroy(gameObject);
            gameController.AddLives(livesValue);
        }
    }
}
