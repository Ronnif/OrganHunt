using UnityEngine;
using TMPro;
using System.Collections;

public class TextoFlotanteVida : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI texto;
    [SerializeField] private float duracion = 1.5f;
    [SerializeField] private float velocidadSubida = 30f;

    public void Mostrar(string mensaje)
    {
        gameObject.SetActive(true);
        texto.text = mensaje;
        texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 1f);
        StopAllCoroutines();
        StartCoroutine(AnimarTexto());
    }

    IEnumerator AnimarTexto()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 posicionInicial = rect.anchoredPosition;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracion)
        {
            tiempoTranscurrido += Time.deltaTime;
            float porcentaje = tiempoTranscurrido / duracion;

            rect.anchoredPosition = posicionInicial + Vector2.up * (velocidadSubida * porcentaje);
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, 1f - porcentaje);

            yield return null;
        }

        gameObject.SetActive(false);
        rect.anchoredPosition = posicionInicial;
    }
}