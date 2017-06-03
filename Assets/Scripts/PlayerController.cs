﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 300.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 6.0f;

        transform.Rotate(0, x, 0);

        if(Input.GetAxis("Vertical") > 0)
        {
            transform.Translate(0, 0, z);
        }

    }
}