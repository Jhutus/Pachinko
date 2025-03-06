using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftRight : MonoBehaviour
{
    public float speed = 2.0f; // Velocidad de movimiento
    public float distance = 5.0f; // Distancia que se moverá el objeto

    private Vector3 startPosition;

    void Start()
    {
        // Guarda la posición inicial del objeto
        startPosition = transform.position;
    }

    void Update()
    {
        // Calcula el movimiento en el eje X usando una función seno para crear un bucle suave
        float newX = startPosition.x + Mathf.Sin(Time.time * speed) * distance;

        // Actualiza la posición del objeto
        transform.position = new Vector3(newX, startPosition.y, startPosition.z);
    }
}
