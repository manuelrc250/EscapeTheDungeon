using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportaminetoEsqueleto : MonoBehaviour
{
    [SerializeField]
    public int rutina;
    [SerializeField]
    public float cronometro;
    [SerializeField]
    public Animator anim;
    [SerializeField]
    public Quaternion angulo;
    [SerializeField]
    public float grado;
    [SerializeField]
    public AudioClip sonidoAtaque2;

    public GameObject target;
    public bool atacando;

    private int vida = 100;
    private int dañoPorAtaque = 20;

    private Rigidbody rb;

    
    public AudioSource sonidoSource;

    public float tiempoEntreAtaque = 2f;
    public float tiempoUltimogolpe = 0f;

    [SerializeField]
    private GameObject objetoAlMorir;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Personaje_principal");
        sonidoSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (!coll.gameObject.CompareTag("Arma")) return;


        if (coll.CompareTag("Arma"))
        {
            if (rb == null) return;

            // Obtener el Rigidbody del enemigo
            Vector3 direccionKnockback = (rb.position - coll.transform.position).normalized;
            direccionKnockback.y = 0;

            vida -= dañoPorAtaque; 

            //Aplicar fuerza de Knockback
            float fuerzaKnockback = 30f; 
            rb.AddForce(direccionKnockback * fuerzaKnockback, ForceMode.Impulse);

            ataqueEnemigo();

            if (vida <= 0)
            {
                Instantiate(objetoAlMorir, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }

    }

    public void Comportamiento() 
    {
        if(Vector3.Distance(rb.position, target.transform.position) > 10)
        {
            anim.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    anim.SetBool("walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    rb.MovePosition(rb.position + transform.forward * 1 * Time.deltaTime);
                    anim.SetBool("walk", true);
                    break;
            }

        }
        else
        {
            if(Vector3.Distance(rb.position, target.transform.position) > 3 && !atacando)
            {
                var lookPos = target.transform.position - rb.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                anim.SetBool("walk", false);

                anim.SetBool("run", true);
                rb.MovePosition(rb.position + transform.forward * 3 * Time.deltaTime);

                anim.SetBool("attack", false);
            }
            else
            {
                anim.SetBool("walk", false);
                anim.SetBool("run", false);

                
                anim.SetBool("attack", true);
                
                atacando = true;
            }
            
        }
        
    }

    public void Final_Ani()
    {
        anim.SetBool("attack", false);
        atacando = false;
    }

    public void ataqueEnemigo()
    {
        if (Time.time - tiempoUltimogolpe < tiempoEntreAtaque) return;
        sonidoSource.PlayOneShot(sonidoAtaque2);
        tiempoUltimogolpe = Time.time;
    }
}
