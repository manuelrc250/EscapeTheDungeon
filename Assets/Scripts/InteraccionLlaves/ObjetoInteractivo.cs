using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjetoInteractivo : MonoBehaviour
{
    private GameController gameController;
    

    
    
    private void Awake()
    {
        GameObject gameObjeto = GameObject.FindGameObjectWithTag("GameController");
            gameController = gameObjeto.GetComponent<GameController>();
        Debug.Log("");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameController.SumarLlaves();
            Destroy(gameObject);
        }
    }

}
