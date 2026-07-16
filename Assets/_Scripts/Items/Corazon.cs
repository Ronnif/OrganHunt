using UnityEngine;

public class Corazon : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoRecoger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Corazón recolectado!");

            GameManager.instancia.RecolectarCorazon();

            if (sonidoRecoger != null)
            {
                AudioSource.PlayClipAtPoint(sonidoRecoger, transform.position);
            }

            Destroy(gameObject);
        }
    }
}