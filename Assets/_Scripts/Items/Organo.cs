using UnityEngine;

public enum TipoOrgano { Corazon, Pulmon, Hueso, Cerebro }

public class Organo : MonoBehaviour
{
    [SerializeField] private TipoOrgano tipo;
    [SerializeField] private AudioClip sonidoRecoger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AplicarEfecto(other);
            if (sonidoRecoger != null)
            {
                AudioSource.PlayClipAtPoint(sonidoRecoger, transform.position, 1.5f);
            }
            Destroy(gameObject);
        }
    }

    void AplicarEfecto(Collider2D jugador)
    {
        switch (tipo)
        {
            case TipoOrgano.Corazon:
                jugador.GetComponent<PlayerHealth>().AumentarVidaMaxima(50f);
                break;
            case TipoOrgano.Pulmon:
                jugador.GetComponent<PlayerMovement>().ActivarHabilidadCorrer();
                break;
            case TipoOrgano.Hueso:
                jugador.GetComponent<PlayerHealth>().ActivarReduccionDaño(0.25f);
                break;
            case TipoOrgano.Cerebro:
                Proyectil.multiplicadorDano += 0.25f;
                break;
        }
        Debug.Log("Órgano recolectado: " + tipo);
    }
}