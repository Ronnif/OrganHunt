using System.Collections.Generic;
using UnityEngine;

public class ProyectilPool : MonoBehaviour
{
    public static ProyectilPool instancia;

    [SerializeField] private GameObject proyectilPrefab;
    [SerializeField] private int tamanoPool = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        instancia = this;
        CrearPool();
    }

    void CrearPool()
    {
        for (int i = 0; i < tamanoPool; i++)
        {
            GameObject obj = Instantiate(proyectilPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject ObtenerProyectil()
    {
        if (pool.Count == 0)
        {
            // Si se acaban, creamos uno extra
            GameObject nuevo = Instantiate(proyectilPrefab);
            return nuevo;
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void DevolverProyectil(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}