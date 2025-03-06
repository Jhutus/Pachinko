using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascaraController : MonoBehaviour
{
    public GameObject bolaPrefab; // Prefab de la bola que caerá
    public float smoothSpeed = 0.1f; // Velocidad de suavizado del movimiento
    public Sprite nuevaImagen; // Nueva imagen para la máscara al hacer clic
    public float tiempoCambioImagen = 0.5f; // Tiempo antes de volver a la imagen original

    private Vector3 startPosition;
    private SpriteRenderer spriteRenderer;
    private Sprite imagenOriginal; // Guarda la imagen original de la máscara
    void Start()
    {
        // Guarda la posición inicial en Y
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Mover la máscara con el mouse en X, pero más lento
        MoverMascaraConMouse();
        // Detectar clic
        if (Input.GetMouseButtonDown(0)) // 0 es el botón izquierdo del mouse
        {
            SoltarBola();
            CambiarImagenMascara();
        }
    }

    void MoverMascaraConMouse()
    {
        // Obtener la posición del mouse en el mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Suavizar el movimiento en X
        float targetX = Mathf.Lerp(transform.position.x, mousePosition.x, smoothSpeed);

        // Mantener la posición en Y fija
        transform.position = new Vector3(targetX, startPosition.y, transform.position.z);
    }

    void SoltarBola()
    {
        // Instanciar la bola en la posición de la máscara
        GameObject bola = Instantiate(bolaPrefab, transform.position, Quaternion.identity);

        // Opcional: Aplicar una fuerza hacia abajo (si la bola tiene un Rigidbody2D)
        Rigidbody2D rb = bola.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.down * 5f; // Ajusta la velocidad según sea necesario
        }
    }

    void CambiarImagenMascara()
    {
        // Cambiar la imagen de la máscara
        if (spriteRenderer != null && nuevaImagen != null)
        {
            spriteRenderer.sprite = nuevaImagen;

            // Iniciar la corrutina para volver a la imagen original
            StartCoroutine(VolverAImagenOriginal());
        }
    }

    // Corrutina para volver a la imagen original después de un tiempo
    System.Collections.IEnumerator VolverAImagenOriginal()
    {
        // Esperar el tiempo definido
        yield return new WaitForSeconds(tiempoCambioImagen);

        // Volver a la imagen original
        spriteRenderer.sprite = imagenOriginal;
    }
}
