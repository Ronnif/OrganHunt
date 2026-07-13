using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instancia;

    [SerializeField] private Image panelFade;
    [SerializeField] private float velocidadFade = 1f;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void CargarEscena(string nombreEscena)
    {
        StartCoroutine(FadeOutYCargar(nombreEscena));
    }

    IEnumerator FadeIn()
    {
        float alpha = 1f;
        panelFade.color = new Color(0, 0, 0, alpha);
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * velocidadFade;
            panelFade.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        panelFade.color = new Color(0, 0, 0, 0);
    }

    IEnumerator FadeOutYCargar(string nombreEscena)
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * velocidadFade;
            panelFade.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        SceneManager.LoadScene(nombreEscena);
    }
}