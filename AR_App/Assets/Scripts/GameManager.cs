using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action OnMainMenu;
    public event Action OnItemsMenu;
    public event Action OnARPosition;

    public static GameManager instance;
    private GameObject currentModel; 
    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>(); 
    // private bool isModelApproved = false;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Método Start: se llama antes de la
    //               primera actualización de los frames
    void Start()
    {
        // Verificar y crear la etiqueta InstantiatedModel si no existe
        if (!TagExists("InstantiatedModel"))
        {
            Debug.LogError("La etiqueta 'InstantiatedModel' no está definida. Por favor, añádela manualmente en Tags and Layers.");
        }
        MainMenu();
    }

    public void MainMenu()
    {
        OnMainMenu?.Invoke();
        Debug.Log("Main Menu Activated");
    }

    public void ItemsMenu()
    {
        OnItemsMenu?.Invoke();
        Debug.Log("Items Menu Activated");
    }

    public void ARPosition()
    {
        OnARPosition?.Invoke();
        Debug.Log("AR Position Activated");
    }

    public void CloseAPP()
    {
        Application.Quit();
    }
    public void ApproveModel()
    {
        // isModelApproved = true; // Marcar el modelo como aprobado
        Debug.Log("Modelo aprobado. Regresando al menú principal.");
        MainMenu(); // Regresar al menú principal
    }

    public void DeleteCurrentModel()
    {
        Debug.Log("Notificando al ARInteractionsManager para eliminar el modelo actual.");
        FindObjectOfType<ARInteractionsManager>().DeleteModel(); // Notificar al ARInteractionsManager
        MainMenu(); // Regresar al menú principal
    }

    public void Closemodels()
    {
        ARInteractionsManager interactionsManager = FindObjectOfType<ARInteractionsManager>();
        if (interactionsManager != null)
        {
            interactionsManager.DeleteAllModels();
        }

        Debug.Log("Todos los elementos visibles han sido eliminados de la pantalla.");
        MainMenu(); // Regresar al menú principal
    }
    private bool TagExists(string tag)
    {
        try
        {
            GameObject.FindGameObjectsWithTag(tag);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public void SetCurrentModel(GameObject model)
    {
        if (model == null)
        {
            Debug.LogError("SetCurrentModel fue llamado con un modelo nulo.");
            return;
        }
        // Si ya hay un modelo seleccionado, deseleccionarlo
        if (currentModel != null)
        {
            Debug.Log($"[GameManager] Deseleccionando el modelo anterior: {currentModel.name}");
            DeselectCurrentModel(); // Llama a una función para manejar la deselección
        }

        currentModel = model;
        //currentModel.transform.localScale = Vector3.one * 0.1f;
        Debug.Log($"[GameManager] Modelo actual establecido: {currentModel.name}");
        // Almacenar colores originales
        originalColors.Clear(); // Limpiar colores previos
        var renderers = currentModel.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            if (renderer.material.HasProperty("_Color"))originalColors[renderer] = renderer.material.color;
        }
    }
    private void DeselectCurrentModel()
{
    if (currentModel == null) return;

    // Restaurar colores originales (opcional)
    ChangeModelColorToDefualt();

    // Opción 1: Desactivar el modelo
    currentModel.SetActive(false);
    Debug.Log($"[DeselectCurrentModel] Modelo {currentModel.name} desactivado.");

    // Opción 2: Eliminar el modelo de la escena
    // Destroy(currentModel);
    // Debug.Log($"[DeselectCurrentModel] Modelo {currentModel.name} eliminado.");

    // Opción 3: Mover el modelo fuera de la pantalla
    // currentModel.transform.position = new Vector3(10000, 10000, 10000);
    // Debug.Log($"[DeselectCurrentModel] Modelo {currentModel.name} movido fuera de la pantalla.");

    // Una vez deseleccionado, limpiamos la referencia al modelo actual
    currentModel = null;
}

    public void ChangeModelColorToRed()
    {
        if (currentModel == null)
        {
            Debug.LogWarning("No hay modelo actual para cambiar el color.");
            return;
        }

        var renderers = currentModel.GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            foreach (var renderer in renderers) renderer.material.color = Color.red;
            Debug.Log($"El modelo '{currentModel.name}' tiene Renderer. Color cambiado a rojo.");
        }
        else
        {
            Debug.LogWarning($"El modelo '{currentModel.name}' no tiene Renderer.");
        }
    }
    public void ChangeModelColorToBlue()
    {
        if (currentModel == null)
        {
            Debug.LogWarning("No hay modelo actual para cambiar el color.");
            return;
        }

        var renderers = currentModel.GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            foreach (var renderer in renderers) renderer.material.color = Color.blue;
            Debug.Log($"El modelo '{currentModel.name}' tiene Renderer. Color cambiado a rojo.");
        }
        else
        {
            Debug.LogWarning($"El modelo '{currentModel.name}' no tiene Renderer.");
        }
    }
    public void ChangeModelColorToLila()
    {
        if (currentModel == null)
        {
            Debug.LogWarning("No hay modelo actual para cambiar el color.");
            return;
        }

        var renderers = currentModel.GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            foreach (var renderer in renderers)
            {
                if (renderer.material.HasProperty("_Color"))
                    renderer.material.color = new Color(0.5f, 0f, 0.5f); // Lila
            }
            Debug.Log($"El modelo '{currentModel.name}' tiene Renderer. Color cambiado a lila.");
        }
        else
        {
            Debug.LogWarning($"El modelo '{currentModel.name}' no tiene Renderer.");
        }
    }

    public void ChangeColorToPurple()
    {
        ChangeModelColorToLila(); // Lila
    }

    public void ChangeModelColorToDefualt()
    {
        if (currentModel == null)
        {
            Debug.LogWarning("No hay modelo actual para cambiar el color.");
            return;
        }

        var renderers = currentModel.GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            foreach (var renderer in renderers) {
                if (originalColors.ContainsKey(renderer) && renderer.material.HasProperty("_Color")) renderer.material.color = originalColors[renderer];
            }
            Debug.Log($"Colores originales restaurados para el modelo '{currentModel.name}'.");
        }
        else
        {
            Debug.LogWarning($"El modelo '{currentModel.name}' no tiene Renderer.");
        }
    }
    public void EnableMoveMode()
    {
        if (currentModel != null)
        {
            var modelController = currentModel.GetComponent<ModelTouchRotation>();
            if (modelController != null)
            {
                modelController.EnableMoveMode();
                Debug.Log("Modo mover activado para el modelo actual.");
            }
            else
            {
                Debug.LogError("El modelo actual no tiene el componente ModelTouchRotation.");
            }
        }
        else
        {
            Debug.LogWarning("No hay modelo activo para habilitar el modo mover.");
        }
    }

    public void EnableRotateMode()
    {
        if (currentModel != null)
        {
            var modelController = currentModel.GetComponent<ModelTouchRotation>();
            if (modelController != null)
            {
                modelController.EnableRotateMode();
                Debug.Log("Modo rotar activado para el modelo actual.");
            }
            else
            {
                Debug.LogError("El modelo actual no tiene el componente ModelTouchRotation.");
            }
        }
        else
        {
            Debug.LogWarning("No hay modelo activo para habilitar el modo rotar.");
        }
    }

    public void DisableAllModes()
    {
        if (currentModel != null)
        {
            var modelController = currentModel.GetComponent<ModelTouchRotation>();
            if (modelController != null)
            {
                modelController.DisableAllModes();
                Debug.Log("Todos los modos deshabilitados para el modelo actual.");
            }
            else
            {
                Debug.LogError("El modelo actual no tiene el componente ModelTouchRotation.");
            }
        }
        else
        {
            Debug.LogWarning("No hay modelo activo para deshabilitar los modos.");
        }
    }

    public void EnlargeModel()
    {
        if (currentModel != null)
        {
            Debug.LogWarning("Boton Para Agrandar El Modelo Pulsado.");
           // Debug.Log($"[ShrinkModel] Tamaño actual del modelo: {currentModel.transform.localScale}");
            //Vector3 newScale = currentModel.transform.localScale + Vector3.one * 0.1f;
            Vector3 newScale = currentModel.transform.localScale * 1.2f;
            if(newScale.x <= 15.0f && newScale.y <= 15.0f && newScale.z <= 15.0f)
            {
                currentModel.transform.localScale = newScale;
            }
            else {
                Debug.LogWarning("ES GRANDE");
            }
        }
        else
        {
            Debug.LogWarning("No hay modelo activo para escalar.");
        }
    }

    /*public void ShrinkModel()
    {
        if (currentModel != null)
        {
            Debug.LogWarning("Boton Para Encoger El Modelo Pulsado.");
            Vector3 newScale = currentModel.transform.localScale - Vector3.one * 0.01f;
            if(newScale.x >= 0.01f && newScale.y >= 0.01f && newScale.z >= 0.01f)
            {
                currentModel.transform.localScale = newScale;
            }
        }
        else
        {
            Debug.LogWarning("No hay modelo activo para escalar.");
        }
    }*/
    public void ShrinkModel()
    {
        if (currentModel != null)
        {
            Debug.LogWarning("Botón para encoger el modelo pulsado.");
            
            // Calculamos el nuevo tamaño
            //Vector3 newScale = currentModel.transform.localScale - Vector3.one * 0.1f;
            Vector3 newScale = currentModel.transform.localScale * 0.8f;
            
            // Verificamos si el nuevo tamaño cumple con los límites mínimos
            if (newScale.x >= 0.00001f && newScale.y >= 0.00001f && newScale.z >= 0.00001f)
            {
                currentModel.transform.localScale = newScale;
                Debug.Log($"Nuevo tamaño del modelo: {currentModel.transform.localScale}");
            }
            else
            {
                Debug.LogWarning($"El modelo no puede encogerse más allá del límite mínimo (0.01). Tamaño actual: {currentModel.transform.localScale}");
            }
        }
        else
        {
            Debug.LogWarning("No hay modelo activo para encoger.");
        }
    }

}