using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log("Trigger entered");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
