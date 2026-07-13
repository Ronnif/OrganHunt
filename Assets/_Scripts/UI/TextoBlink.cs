using System.Collections;
using UnityEngine;
using TMPro;

public class TextoBlink : MonoBehaviour
{
    [SerializeField] private float velocidadParpadeo = 1f;
    private TextMeshProUGUI texto;

    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Parpadear());
    }

    IEnumerator Parpadear()
    {
        while (true)
        {
            texto.alpha = 1f;
            yield return new WaitForSeconds(velocidadParpadeo);
            texto.alpha = 0f;
            yield return new WaitForSeconds(velocidadParpadeo);
        }
    }
}