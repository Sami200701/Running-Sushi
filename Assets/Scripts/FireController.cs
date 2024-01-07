using UnityEngine;

public class FireController : MonoBehaviour
{
    public float timeBetweenToggle = 4f;  // Tiempo entre apariciones/desapariciones
    public GameObject fireEffect;         // El efecto de fuego

    void Start()
    {
        // Comienza el ciclo de aparici�n/desaparici�n
        InvokeRepeating("ToggleFire", 0f, timeBetweenToggle);
    }

    void ToggleFire()
    {
        // Activa o desactiva el efecto de fuego
        if (fireEffect.activeSelf)
        {
            // Si el fuego est� activo, desact�valo
            fireEffect.SetActive(false);
        }
        else
        {
            // Si el fuego est� desactivado, act�valo
            fireEffect.SetActive(true);
        }
    }
}
