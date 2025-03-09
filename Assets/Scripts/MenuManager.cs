using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Game"); 
    }

    public void Ayuda()
    {
        SceneManager.LoadScene("Help"); 
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Game");
    }

    public void Volver()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }


}
