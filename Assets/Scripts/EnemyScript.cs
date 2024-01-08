using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    private bool playerIsDead = false; //para rastrear si el jugador esta muerto

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerIsDead)
        {
            StartCoroutine(WaitAndDie(other.gameObject));
        }
    }

    IEnumerator WaitAndDie(GameObject player)
    {
        playerIsDead = true; //esta muerto

        // Detener el movimiento del jugador y rotarlo en 90 grados.
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        PlayerMovement playerController = player.GetComponent<PlayerMovement>();
        FindObjectOfType<AudioManager>().Play("Death");
        FindObjectOfType<AudioManager>().Pause("Theme");
        FindObjectOfType<AudioManager>().Stop("Running");

        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero; //Detener el movimiento
        }

        if (playerController != null)
        {            
            playerController.enabled = false;//Desactivar el script de movimiento del jugador                    
            playerController.RotatePlayer(-90f);// Girar en 90 grados
        }

        yield return new WaitForSeconds(0.4f); // tiempo antes de ser destruido

        //Destroy(player);

        

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<AudioManager>().UnPause("Theme");
    }
}
