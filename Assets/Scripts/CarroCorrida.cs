using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Carro : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    [SerializeField]
    private float maxSpeed, acceleration, target, currentSpeed;
    private Vector2 speed;
    public float hori, vert;
    private float aux;
    private float driftForce;
    public Vector2 relativeForce;

    private List<GameObject> marcas = new List<GameObject>();


    void Start()
    {
    }
    private void FixedUpdate()
    {

        vert = Input.GetAxis("Vertical");
        hori = -Input.GetAxis("Horizontal");
        
        if (vert > 0)
        {
            speed = transform.up * acceleration;
        } else if (vert < 0)
        {
            speed = -transform.up * acceleration;
        } else  if (vert == 0)
        {
            speed = Vector2.zero;
            rb.velocity = Vector2.zero;
        }
        rb.AddForce(speed);
        aux = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));

        if (aux >= 0.0f)
        {
            rb.rotation += hori * target * (rb.velocity.magnitude / maxSpeed * 0.8f);
        }
        else
        {
            rb.rotation -= hori * target * (rb.velocity.magnitude / maxSpeed * 0.8f);
        }

        driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;

        relativeForce = Vector2.right * driftForce;

        rb.AddForce(rb.GetRelativeVector(relativeForce));

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        currentSpeed = rb.velocity.magnitude;
    }
}
