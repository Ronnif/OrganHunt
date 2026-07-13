using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instancia;

    private AudioSource musicaFondo;

    [SerializeField] private AudioClip sonidoClick;
    private AudioSource sfxSource;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicaFondo = GetComponent<AudioSource>();
        musicaFondo.Play();
        sfxSource = gameObject.AddComponent<AudioSource>();
    }

    public void SetVolumenMusica(bool activar)
    {
        if (activar)
        {
            musicaFondo.volume = 0.5f;
        }
        else
        {
            musicaFondo.volume = 0f;
        }
    }

    public void SetVolumenSFX(bool activar)
    {
        sfxSource.volume = activar ? 1f : 0f;
    }

    public void PlayClick()
    {
        sfxSource.PlayOneShot(sonidoClick);
    }
}