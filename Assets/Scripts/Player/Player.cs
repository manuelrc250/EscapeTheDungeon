using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;


public class MoverPlayer : MonoBehaviour
{
    [SerializeField]
    private float velocidade = 10;
    [SerializeField]
    private float velocidadRotacion = 5;
    
    private PlayerInput playerInput;
    private Rigidbody rb;
    private float tempo;
    private Vector3 tmp;
    private Vector2 valor;
    private InputAction accionMover;
    private InputAction accionAtacar;

    private GameController gameController;

    private Animator anim;

    private Collider ultimoCollider;


    private int vida = 100;
    public float vidaMaxima = 100;
    private int dañoPorAtaque;

    [SerializeField]
    private AudioClip sonidoAtaque;
    
    public AudioSource sonidoSource;

    public float tiempoEntreAtaque = 2f;
    public float tiempoUltimogolpe = 0f;

    

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        accionMover = playerInput.actions["MoverPersonaje"];
        accionAtacar = playerInput.actions["Atacar"];
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        

    }

    // Start is called before the first frame update
    void Start()
    {
        tmp = Vector3.zero;
        valor = Vector2.zero;
        tempo = Time.time;
        anim = GetComponentInChildren<Animator>();
        sonidoSource = GetComponent<AudioSource>();
        
        
    }

    private void FixedUpdate()
    {
        tmp.Set(valor.x * velocidade, rb.velocity.y, valor.y * velocidade);
        rb.velocity = tmp;

        tmp.Set(rb.position.x,
            rb.position.y,
            rb.position.z);
        rb.position = tmp;
        Vector3 moverDirection = new Vector3(valor.x, 0, valor.y);

        if (moverDirection.sqrMagnitude > 0.01f)
        {
            Quaternion rotacionPersonaje = Quaternion.LookRotation(moverDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, rotacionPersonaje, velocidadRotacion * Time.deltaTime);
        }

        

        valor = accionMover.ReadValue<Vector2>();
        anim.SetBool("Esta_Corriendo", valor.sqrMagnitude > 0.01f);



    }
    // Update is called once per frame
    void Update()
    {
        valor = accionMover.ReadValue<Vector2>();
        if (accionAtacar.WasPressedThisFrame())
        {
            RealizarAtaque();
        }

        
    }

    public void RealizarAtaque()
    {
        anim.SetTrigger("ataca");
        ataqueAire();


    }

    private void OnTriggerEnter(Collider coll)
    {

        if (coll.CompareTag("Arma"))
        {
            dañoPorAtaque = 5;

            // Obtener el Rigidbody del enemigo
            Vector3 direccionKnockback = (transform.position - coll.transform.position).normalized;
            direccionKnockback.y = 0;

            vida -= dañoPorAtaque;

            //Aplicar fuerza de Knockback
            float fuerzaKnockback = 5f;
            rb.AddForce(direccionKnockback * fuerzaKnockback, ForceMode.Impulse);

            gameController.actualizarBarraDeVida(vida, vidaMaxima);

        }


        if (vida <= 0)
        {
            gameController.perder = true;
            
            anim.SetBool("muerto", true);
        }

        if (coll.CompareTag("Final"))
        {
            
            gameController.ganar = true;
        }

    }

    public void ataqueAire()
    {
        if (Time.time - tiempoUltimogolpe < tiempoEntreAtaque) return;
        sonidoSource.PlayOneShot(sonidoAtaque);
        tiempoUltimogolpe = Time.time;
    }

   
    

}

