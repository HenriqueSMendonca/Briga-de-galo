using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject start1, start2, finish1, finish2;
    GameObject galo1GO, galo2GO;
    void Start()
    {
        galo1GO = GameObject.Find("Galo1");
        galo2GO = GameObject.Find("Galo2");
        galo1GO.transform.position = new Vector2(start1.transform.position.x, start1.transform.position.y);
        galo2GO.transform.position = new Vector2(start2.transform.position.x, start2.transform.position.y);
        galo1GO.transform.rotation = start1.transform.rotation;
        galo2GO.transform.rotation = start2.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Galo1")
        {
            Debug.Log("player1 ganhou");
        }
        else if (collision.gameObject.name == "Galo2")
        {
            Debug.Log("player2 ganhou");
        }
    }
}
