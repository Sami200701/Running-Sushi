using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private GameMaster gm;

    public bool isEnd = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isEnd)
            {
                SceneManager.LoadScene("creditos");
            }
            else
            {
                gm.lastCheckPointPos = transform.position;
                gm.SaveState();
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
