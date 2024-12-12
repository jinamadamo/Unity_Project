using UnityEngine;
using UnityEngine.InputSystem; // Necesario para usar el nuevo Input System

public class ModelTouchRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Velocidad de rotación
    private bool isDragging = false; // Indica si el modelo está siendo rotado
    private Vector2 lastTouchPosition; // Almacena la posición anterior del toque o ratón

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed) // Detectar clic del ratón en el nuevo sistema de entrada
        {
            if (!isDragging) // Si comienza a arrastrar
            {
                isDragging = true;
                lastTouchPosition = Mouse.current.position.ReadValue(); // Obtener posición inicial del ratón
            }

            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            Vector2 delta = currentMousePosition - lastTouchPosition;

            RotateModel(delta.x); // Solo usa deltaX para rotar en el eje Y

            lastTouchPosition = currentMousePosition; // Actualizar la posición anterior
        }
        else if (isDragging) // Liberar el clic
        {
            isDragging = false;
        }

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed) // Detectar toque en pantalla táctil
        {
            Vector2 touchDelta = Touchscreen.current.primaryTouch.delta.ReadValue();

            RotateModel(touchDelta.x); // Solo usa deltaX para rotar en el eje Y
        }
    }

    private void RotateModel(float deltaX)
    {
        // Rotar el modelo en el eje Y
        transform.Rotate(Vector3.up, -deltaX * rotationSpeed * Time.deltaTime, Space.World); // Rotación horizontal
    }
}
