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
            // Activate the first and second flames
            fireEffects[0].SetActive(true);
            fireEffects[1].SetActive(true);

            // Wait for a duration
            yield return new WaitForSeconds(firstSecondFlameDuration);

            // Deactivate the first flame
            fireEffects[0].SetActive(false);

            // Deactivate the second flame
            fireEffects[1].SetActive(false);

            // Wait for the remaining duration
            yield return new WaitForSeconds(timeBetweenToggle - firstSecondFlameDuration);

            // Activate the third flame
            fireEffects[2].SetActive(true);

            // Wait for the full timeBetweenToggle before starting the next cycle
            yield return new WaitForSeconds(timeBetweenToggle);

            // Deactivate the third flame
            fireEffects[2].SetActive(false);
        }
    }
}



