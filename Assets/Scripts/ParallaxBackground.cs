using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Material mat;
    [Range(0f, 0.1f)]
    public float speed;
    Vector2 direction = new Vector2(0.1f, 0.1f);

    private void Start()
    {
        mat = gameObject.GetComponent<Renderer>().material;
    }
    private void LateUpdate()
    {
        mat.SetTextureOffset("_MainTex", direction * speed);
        direction += new Vector2(0.1f, 0.1f);
    }
}
