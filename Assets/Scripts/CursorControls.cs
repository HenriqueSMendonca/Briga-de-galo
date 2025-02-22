using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorControls : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    Vector2 movement;
    private Button button;
    private string buttonName;


    private void Awake()
    {
        button = GetComponent<Button>();
    }
    void Start()
    {
        
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
        if (collision.gameObject.tag == "Button")
        {
            buttonName = collision.gameObject.name;
            button = collision.gameObject.GetComponent<Button>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Button")
        {
            button = null;
        }
    }
}
