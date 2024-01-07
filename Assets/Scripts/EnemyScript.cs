using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("Death");
            FindObjectOfType<AudioManager>().Pause("Theme");
            FindObjectOfType<AudioManager>().Stop("Running");
            StartCoroutine(WaitAndDie());
        }
    }
    
    IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<AudioManager>().UnPause("Theme");
    }
}
