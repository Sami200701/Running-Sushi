using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float timeBetweenToggle = 4f;  // Time between appearances/disappearances
    public float firstSecondFlameDuration = 2f;  // Duration for which the first and second flames stay active
    public GameObject[] fireEffects;      // Array of flame effects

    void Start()
    {
        // Start the appearance/disappearance cycle
        StartCoroutine(ToggleFlames());
    }

    System.Collections.IEnumerator ToggleFlames()
    {
        while (true)
        {
            // Activar llama 1,3,5,7,9
            fireEffects[0].SetActive(true);
            fireEffects[2].SetActive(true);
            fireEffects[4].SetActive(true);
            fireEffects[6].SetActive(true);
            fireEffects[8].SetActive(true);

            // Wait for a duration
            yield return new WaitForSeconds(firstSecondFlameDuration);

            // Desactivar la 1,3,5,7,9
            fireEffects[0].SetActive(false);
            fireEffects[2].SetActive(false);
            fireEffects[4].SetActive(false);
            fireEffects[6].SetActive(false);
            fireEffects[8].SetActive(false);

            // Wait for the remaining duration
            yield return new WaitForSeconds(timeBetweenToggle - firstSecondFlameDuration);

            // Activar la 2,4,6 y 8
            fireEffects[1].SetActive(true);
            fireEffects[3].SetActive(true);
            fireEffects[5].SetActive(true);
            fireEffects[7].SetActive(true);


            // Wait for the full timeBetweenToggle before starting the next cycle
            yield return new WaitForSeconds(timeBetweenToggle);

            // Desactivar la 2,4,6,8
            fireEffects[1].SetActive(false);
            fireEffects[3].SetActive(false);
            fireEffects[5].SetActive(false);
            fireEffects[7].SetActive(false);
        }
    }
}



