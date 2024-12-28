using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Referencia al Renderer del modelo
    private Renderer modelRenderer;

    // Inicialización
    void Start()
    {
        // Obtén el Renderer del objeto
        modelRenderer = GetComponent<Renderer>();

        // Verifica si el Renderer existe
        if (modelRenderer == null)
        {
            Debug.LogError("No se encontró el Renderer en el objeto. Asegúrate de asignar este script al modelo 3D.");
        }
    }

    // Función para cambiar el color del modelo
    public void ChangeColor(Color newColor)
    {
        if (modelRenderer != null)
        {
            // Cambia el color del material
            modelRenderer.material.color = newColor;
        }
        else
        {
            Debug.LogError("Renderer no asignado.");
        }
    }
}
