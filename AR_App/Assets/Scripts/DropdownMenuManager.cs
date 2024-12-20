using UnityEngine;
using UnityEngine.UI;

public class DropdownMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject dropdownMenu; // Contenedor de los botones (UIButtons)
    private bool isMenuActive = false; // Controla si el menú está activo o no

    void Start()
    {
        // Asegurarse de que el menú esté oculto al inicio
        if (dropdownMenu != null)
        {
            dropdownMenu.SetActive(false);
        }
    }

    // Método para activar/desactivar el menú
    public void ToggleMenu()
    {
        if (dropdownMenu != null)
        {
            isMenuActive = !isMenuActive;
            dropdownMenu.SetActive(isMenuActive);
            Debug.Log("Menú desplegable " + (isMenuActive ? "abierto" : "cerrado"));
        }
        else
        {
            Debug.LogError("El menú desplegable no está asignado.");
        }
    }
}
