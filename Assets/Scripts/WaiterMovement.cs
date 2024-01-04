using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class WaiterMovement : MonoBehaviour
{
    public Transform[] pathPoints;  // Array de puntos en la ruta rectangular
    public float speed = 2f;        // Velocidad de movimiento
    public int collisionIndex;

    private Transform targetPoint;
    private int currentPointIndex = 0;  // �ndice del punto actual en la ruta

    void Start()
    {
        // Al inicio, establece el primer punto como destino
        targetPoint = pathPoints[0];
    }

    void Update()
    {
        // Mueve al camarero hacia el punto objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Si el camarero alcanza el punto objetivo, cambia el destino al siguiente punto en la ruta
        if (transform.position == targetPoint.position)
        {
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;
            targetPoint = pathPoints[currentPointIndex];
        }

        if (currentPointIndex != collisionIndex)
        {
            transform.Find("tray").gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        // Mira hacia el punto objetivo
        transform.LookAt(targetPoint);

        if (currentPointIndex == collisionIndex)
        {
            transform.Find("tray").gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}

