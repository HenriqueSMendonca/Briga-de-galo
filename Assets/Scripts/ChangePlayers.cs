using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangePlayers : MonoBehaviour
{
    public GameObject[] gameObjects;
    Carro carro1, carro2;
    CursorControls cursor1, cursor2;
    public PlayerInputManager playerManager;
    bool roomFull = false;
    public GameObject[] pistas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.playerCount == 4 && roomFull == false)
        {
            roomFull = true;
            playerManager.DisableJoining();
            int random = Random.Range(0, pistas.Length);
            Debug.Log(random);
            Instantiate(pistas[random]);
        }
        if (gameObjects[3] == null)
        {
            
            
        }
        else if (gameObjects[3] != null)
        {
            cursor1 = gameObjects[0].GetComponent<CursorControls>();
            carro1 = gameObjects[1].GetComponent<Carro>();
            cursor2 = gameObjects[2].GetComponent<CursorControls>();
            carro2 = gameObjects[3].GetComponent<Carro>();
        }
       if(Input.GetKeyDown(KeyCode.Space) && roomFull)
        {
            gameObjects[0].GetComponent<SpriteRenderer>().enabled = !gameObjects[0].GetComponent<SpriteRenderer>().enabled;
            cursor1.inputEnabled = !cursor1.inputEnabled;
            gameObjects[1].GetComponent<SpriteRenderer>().enabled = !gameObjects[1].GetComponent<SpriteRenderer>().enabled;
            carro1.inputEnabled = !carro1.inputEnabled;
            gameObjects[2].GetComponent<SpriteRenderer>().enabled = !gameObjects[2].GetComponent<SpriteRenderer>().enabled;
            cursor2.inputEnabled = !cursor2.inputEnabled;
            gameObjects[3].GetComponent<SpriteRenderer>().enabled = !gameObjects[3].GetComponent<SpriteRenderer>().enabled;
            carro2.inputEnabled = !carro2.inputEnabled;
        }

        
    }
}
