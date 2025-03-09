using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject win;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject restart;
    [SerializeField]
    public TextMeshProUGUI textoBotones;
    public Image barraDeVida;

    private GameObject puertaJefe;
    private GameObject puertaEscape;

    private int llavesTotales = 5;
    private int llavesCogidas = 0;

    public bool perder;
    public bool ganar;
    public bool restartXogo;

    [Header("UI Movil")]
    [SerializeField]
    private GameObject goUIMovil;

    public bool Perder
    {
        get
        {
            return perder;
        }
        set
        {
            perder = value;
        }
    }

    public bool Ganar
    {
        get
        {
            return ganar;
        }
        set
        {
            ganar = value;
        }
    }

    public bool RestartXogo
    {
        get
        {
            return restartXogo;
        }
        set
        {
            restartXogo = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ActualizarTextoLlaves();
        perder = false;
        ganar = false;
        RestartXogo = false;

        puertaJefe = GameObject.FindGameObjectWithTag("PuertaJefe");
        puertaEscape = GameObject.FindGameObjectWithTag("PuertaEscape");

        //&& Application.platform != RuntimePlatform.WindowsEditor
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
        {
            goUIMovil.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (RestartXogo)
        {
            SceneManager.LoadScene("Game");
            return;
        }
        
        if(perder)
        {
            gameOver.SetActive(true);
            restart.SetActive(true);
        }

        if (ganar)
        {
            win.SetActive(true);
            return;
        }

    }

    public void SumarLlaves()
    {
        llavesCogidas++; 
        ActualizarTextoLlaves();
        if (llavesCogidas == 4)
        {
            puertaJefe.SetActive(false);
        }
        else if (llavesCogidas == 5)
        {
            puertaEscape.SetActive(false);
        }
    }

    public void ActualizarTextoLlaves()
    {
        textoBotones.text = "Llaves: " + llavesCogidas.ToString() + "/" + llavesTotales.ToString();
    }

    public  void actualizarBarraDeVida(int vida, float vidaMaxima)
    {
        barraDeVida.fillAmount = vida / vidaMaxima;
    }

   
   

   
}
