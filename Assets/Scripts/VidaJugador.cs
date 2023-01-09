using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJugador : MonoBehaviour
{
    public static VidaJugador instance;

    [SerializeField] private float vida;

    [SerializeField] private float maximoVida;

    [SerializeField] private BarraDeVida barraDeVida;

    public GameObject death;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        vida = maximoVida;
        barraDeVida.InicializarBarraVida(vida);
    }
    
    public void TakeDamage(float damage)
    {
        vida -= damage;
        barraDeVida.CambiarVidaActual(vida);
        if(vida <= 0)
        {
            Destroy(gameObject);
            death.SetActive(true);
        }
    }
    
    public void Curar(int curacion)
    {
        if ((vida + curacion) > maximoVida)
        {
            vida = maximoVida;
            barraDeVida.CambiarVidaActual(vida);
        }
        else
        {
            vida += curacion;
            barraDeVida.CambiarVidaActual(vida);
        }
    }
}

