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
    public bool inputEnabled = true;
    private Vector2 screenBounds;


    private void Awake()
    {
        button = GetComponent<Button>();
    }
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        inputEnabled = true;
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
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1, screenBounds.x);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1, screenBounds.y);
        transform.position = viewPos;
        if (inputEnabled)
        {
            rb.velocity = movement * speed;
        }
        
    }

    public void Move(InputAction.CallbackContext context) 
    {
         movement = context.ReadValue<Vector2>();
    }
    public void Touch(InputAction.CallbackContext context)
    {
        if (button != null && context.started == true)
        {
            button.onClick?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (gameObject.name == "P1")
        {
            if (collision.gameObject.CompareTag("Button") || collision.gameObject.CompareTag("ButtonP1"))
            {
                button = collision.gameObject.GetComponent<Button>();
                collision.gameObject.GetComponent<Button>().onClick.Invoke();
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
