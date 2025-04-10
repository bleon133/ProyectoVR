using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Vector3 posicionAnterior;
    public GameObject player;

    private void Start()
    {
        posicionAnterior = transform.position;
    }

    public void trasladarPlayer(Vector3 posicionDestino)
    {
        posicionAnterior = transform.position;

    }

}
