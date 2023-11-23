using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;

    public bool preDash;
    public bool preWallJump;
    public bool preDoubleJump;
    
    public bool dash;
    public bool wallJump;
    public bool doubleJump;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveState()
    {
        doubleJump = preDoubleJump;
        wallJump = preWallJump;
        dash = preDash;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
