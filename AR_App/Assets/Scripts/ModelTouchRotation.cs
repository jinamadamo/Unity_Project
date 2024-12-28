using UnityEngine;
using UnityEngine.InputSystem;

public class ModelTouchRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float moveSpeed = 0.01f;

    private bool isDragging = false;
    private Vector2 lastTouchPosition;

    // Control de habilitación
    private bool isMovingEnabled = false; // Controla si mover está habilitado
    private bool isRotatingEnabled = false; // Controla si rotar está habilitado

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

            // Verifica cuál funcionalidad está habilitada
            if (isMovingEnabled)
            {
                Debug.Log($"[Update] Movimiento habilitado. Llamando a MoveModelToMouse.");
                MoveModelToMouse(currentMousePosition);
            }
            else if (isRotatingEnabled)
            {
                Debug.Log($"[Update] Rotación habilitada. Llamando a RotateModel.");
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
            if (isMovingEnabled)
            {
                Debug.Log($"[Update] Movimiento habilitado en Touchscreen. Llamando a MoveModelToMouse.");
                MoveModelToMouse(touchPosition);
            }
            else if (isRotatingEnabled)
            {
                Debug.Log($"[Update] Rotación habilitada en Touchscreen. Llamando a RotateModel.");
                Vector2 touchDelta = Touchscreen.current.primaryTouch.delta.ReadValue();
                RotateModel(touchDelta.x);
            }
        }
    }

    public void RotateModel(float deltaX)
    {
        Debug.Log($"[RotateModel] Entrando en función RotateModel. isMovingEnabled: {isMovingEnabled}, isRotatingEnabled: {isRotatingEnabled}");
        if (!isRotatingEnabled) return; // Solo rota si está habilitado
        transform.Rotate(Vector3.up, -deltaX * rotationSpeed * Time.deltaTime, Space.World);
        Debug.Log($"[RotateModel] Rotación realizada con deltaX: {deltaX}");
    }

    public void MoveModelToMouse(Vector2 mousePosition)
    {
        Debug.Log($"[MoveModelToMouse] Entrando en función MoveModelToMouse. isMovingEnabled: {isMovingEnabled}, isRotatingEnabled: {isRotatingEnabled}");
        if (!isMovingEnabled) return; // Solo mueve si está habilitado
        Plane plane = new Plane(Vector3.forward, transform.position);
        Vector3 mouseWorldPosition = new Vector3(mousePosition.x, mousePosition.y, 0);

        mouseWorldPosition.x = mouseWorldPosition.x / Screen.width - 0.5f;
        mouseWorldPosition.y = mouseWorldPosition.y / Screen.height - 0.5f;
        mouseWorldPosition *= 2;

        transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, transform.position.z);
        Debug.Log($"[MoveModelToMouse] Movimiento realizado a posición: {transform.position}");
    }

    // Función para habilitar el modo de movimiento y desactivar el modo de rotación
    public void EnableMoveMode()
    {
        isMovingEnabled = true;
        isRotatingEnabled = false;
        Debug.Log($"[EnableMoveMode] Modo mover activado. isMovingEnabled: {isMovingEnabled}, isRotatingEnabled: {isRotatingEnabled}");
    }

    // Función para habilitar el modo de rotación y desactivar el modo de movimiento
    public void EnableRotateMode()
    {
        isRotatingEnabled = true;
        isMovingEnabled = false;
        Debug.Log($"[EnableRotateMode] Modo rotar activado. isMovingEnabled: {isMovingEnabled}, isRotatingEnabled: {isRotatingEnabled}");
    }

    // Función para deshabilitar ambos modos
    public void DisableAllModes()
    {
        isRotatingEnabled = false;
        isMovingEnabled = false;
        Debug.Log($"[DisableAllModes] Mover y rotar deshabilitados. isMovingEnabled: {isMovingEnabled}, isRotatingEnabled: {isRotatingEnabled}");
    }
}


/*using UnityEngine;
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

    public void MoveModelToMouse(Vector2 mousePosition)
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
*/