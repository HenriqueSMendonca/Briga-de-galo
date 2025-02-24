using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CursorControls : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    Vector2 movement;
    private Button button;


    private void Awake()
    {
        button = GetComponent<Button>();
    }
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {
            gameObject.name = "P1";
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
        } else
        {
            gameObject.name = "P2";
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        }
    }

    
    void Update()
    {
        rb.velocity = movement * speed;
    }

    public void Move(InputAction.CallbackContext context) 
    {
         movement = context.ReadValue<Vector2>();
    }
    public void Touch(InputAction.CallbackContext context)
    {
        if (button != null && context.started == true)
        {
            button.onClick.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.name == "P1")
        {
            if (collision.gameObject.CompareTag("Button") || collision.gameObject.CompareTag("ButtonP1"))
            {
                button = collision.gameObject.GetComponent<Button>();
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Button") || collision.gameObject.CompareTag("ButtonP2"))
            {
                button = collision.gameObject.GetComponent<Button>();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Button") || collision.gameObject.CompareTag("ButtonP1") || collision.gameObject.CompareTag("ButtonP2"))
        {
            button = null;
        }
    }
}
