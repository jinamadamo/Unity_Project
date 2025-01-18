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

        currentModel = model;
        currentModel.transform.localScale = Vector3.one * 0.1f;
        Debug.Log($"[GameManager] Modelo actual establecido: {currentModel.name}");
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
            Vector3 newScale = currentModel.transform.localScale + Vector3.one * 0.01f;
            if(newScale.x <= 3.0f && newScale.y <= 3.0f && newScale.z <= 3.0f)
            {
                currentModel.transform.localScale = newScale;
            }
        }
        else
        {
            Debug.LogWarning("No hay modelo activo para escalar.");
        }
    }

    public void ShrinkModel()
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
    }
}