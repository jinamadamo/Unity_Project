using UnityEngine;
using UnityEngine.InputSystem;

public class ModelTouchRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; 
    public float moveSpeed = 0.01f;    
    private bool isDragging = false;  
    private Vector2 lastTouchPosition; 
    private bool isMoving = false; 

    private void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            if (!isDragging)
            {
                isDragging = true;
                lastTouchPosition = currentMousePosition;
            }

            Vector2 delta = currentMousePosition - lastTouchPosition;
            if (isMoving)
            {
                MoveModelToMouse(currentMousePosition);
            }
            else
            {
                RotateModel(delta.x);
            }

            lastTouchPosition = currentMousePosition;
        }
        else if (isDragging)
        {
            isDragging = false;
        }

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            if (isMoving)
            {
                MoveModelToMouse(touchPosition);
            }
            else
            {
                Vector2 touchDelta = Touchscreen.current.primaryTouch.delta.ReadValue();
                RotateModel(touchDelta.x);
            }
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isMoving = !isMoving;
            Debug.Log(isMoving ? "Modo mover activado" : "Modo rotar activado");
        }
    }

    private void RotateModel(float deltaX)
    {
        transform.Rotate(Vector3.up, -deltaX * rotationSpeed * Time.deltaTime, Space.World);
    }

    private void MoveModelToMouse(Vector2 mousePosition)
    {
        // Define un plano en el espacio donde se moverá el modelo
        Plane plane = new Plane(Vector3.forward, transform.position); // Plano paralelo al eje Z
        Vector3 mouseWorldPosition = new Vector3(mousePosition.x, mousePosition.y, 0);

        // Convierte las coordenadas de pantalla directamente al espacio local
        mouseWorldPosition.x = mouseWorldPosition.x / Screen.width - 0.5f; // Normaliza entre -0.5 y 0.5
        mouseWorldPosition.y = mouseWorldPosition.y / Screen.height - 0.5f;
        mouseWorldPosition *= 2; // Ajusta al rango completo [-1, 1]

        // Calcula la posición del modelo en el plano definido
        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
    }
}
