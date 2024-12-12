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

}
