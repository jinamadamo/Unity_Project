using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteractionsManager : MonoBehaviour
{
    [SerializeField] private Camera arCamera; // Cámara AR asignada en el inspector
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject item3DModel;
    private bool isModelApproved = false; // Variable para rastrear si el modelo está aprobado

    public GameObject Item3DModel
    {
        get { return item3DModel; }
        set
        {
            if (value != null)
            {
                item3DModel = value;

                // Calcular la posición frente a la cámara
                Vector3 cameraForward = arCamera.transform.forward;
                Vector3 cameraPosition = arCamera.transform.position;

                // Ajustar la posición a una distancia fija
                float distanceFromCamera = 1.0f; 
                Vector3 positionInFrontOfCamera = cameraPosition + cameraForward * distanceFromCamera;

                // Asignar la posición y orientación
                item3DModel.transform.position = positionInFrontOfCamera;
                item3DModel.transform.rotation = Quaternion.LookRotation(cameraForward); 

                // Agregar el script de rotación táctil al modelo
                item3DModel.AddComponent<ModelTouchRotation>();

                // Notificar al GameManager
                GameManager.instance.SetCurrentModel(item3DModel);
                
                Debug.Log($"Modelo 3D posicionado en: {item3DModel.transform.position}");
            }
            else
            {
                Debug.LogError("El modelo 3D es nulo.");
            }
        }
    }

    void Start()
    {
        // Inicializar ARRaycastManager (por si necesitas usarlo más adelante)
        arRaycastManager = FindFirstObjectByType<ARRaycastManager>();

        // Registrar al evento del menú principal para limpiar el modelo actual
      //  GameManager.instance.OnMainMenu += ResetItemPosition;
    }

    private void ResetItemPosition()
    {
        if (!isModelApproved && item3DModel != null) // No eliminar si el modelo está aprobado
        {
            Destroy(item3DModel);
            item3DModel = null;
            Debug.Log("Modelo no aprobado eliminado al regresar al menú principal.");
        }
    }

    public void ApproveModel()
    {
        if (item3DModel != null)
        {
            isModelApproved = true; // Marcar el modelo como aprobado
            Debug.Log("El modelo ha sido aprobado. Regresando al menú principal.");

            // Redirigir al MainMenuCanvas
            GameManager.instance.MainMenu();
          // GameManager.instance.ItemsMenu();
        }
        else
        {
            Debug.LogError("No hay modelo 3D para aprobar.");
        }
    }

    public void DeleteModel()
    {
        if (item3DModel != null)
        {
            Destroy(item3DModel); // Eliminar el modelo de la escena
            item3DModel = null; // Limpiar la referencia
            isModelApproved = false; // Resetear el estado de aprobación
            Debug.Log("Modelo eliminado de la pantalla.");
        }
        else
        {
            Debug.LogWarning("No hay un modelo activo para eliminar.");
        }
    }

    public void DeleteAllModels()
    {
        GameObject[] instantiatedModels = GameObject.FindGameObjectsWithTag("InstantiatedModel");

        if (instantiatedModels.Length == 0)
        {
            Debug.LogError("No se encontraron modelos con la etiqueta 'InstantiatedModel'.");
            return;
        }

        foreach (GameObject model in instantiatedModels)
        {
            Destroy(model); // Eliminar el objeto
            Debug.Log($"Modelo eliminado: {model.name}");
        }

        isModelApproved = false; // Resetear el estado de aprobación
        Debug.Log("Todos los modelos visibles han sido eliminados.");
    }
}



/*
public class ARInteractionsManager : MonoBehaviour
{
    [SerializeField] private Camera arCamera; // Cámara AR asignada en el inspector
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject item3DModel;
    public GameObject Item3DModel
    {
        get { return item3DModel; }
        set
        {
            if (value != null)
            {
                item3DModel = value;

                // Calcular la posición frente a la cámara
                Vector3 cameraForward = arCamera.transform.forward;
                Vector3 cameraPosition = arCamera.transform.position;

                // Ajustar la posición a una distancia fija
                float distanceFromCamera = 1.0f; 
                Vector3 positionInFrontOfCamera = cameraPosition + cameraForward * distanceFromCamera;

                // Asignar la posición y orientación
                item3DModel.transform.position = positionInFrontOfCamera;
                item3DModel.transform.rotation = Quaternion.LookRotation(cameraForward); 

                // Agregar el script de rotación táctil al modelo
                item3DModel.AddComponent<ModelTouchRotation>();

                // Notificar al GameManager
            GameManager.instance.SetCurrentModel(item3DModel);
                
                Debug.Log($"Modelo 3D posicionado en: {item3DModel.transform.position}");
            }
            else
            {
                Debug.LogError("El modelo 3D es nulo.");
            }
        }
    }

    void Start()
    {
        // Inicializar ARRaycastManager (por si necesitas usarlo más adelante)
        arRaycastManager = FindFirstObjectByType<ARRaycastManager>();

        // Registrar al evento del menú principal para limpiar el modelo actual
        GameManager.instance.OnMainMenu += ResetItemPosition;
    }

    void Update()
    {
        // Ya no necesitamos `arPointer` ni detección de planos aquí
    }

    private void ResetItemPosition()
    {
        if (item3DModel != null && item3DModel != null)
        {
            Destroy(item3DModel);
            item3DModel = null;
            Debug.Log("Modelo no aprobado eliminado al regresar al menú principal.");
        }
    }

    public void ApproveModel()
    {
        if (item3DModel != null)
        {
             GameManager.instance.ApproveModel();
            Debug.Log("El modelo ha sido aprobado. Ahora permanecerá en su posición.");
            
        }
        else
        {
            Debug.LogError("No hay modelo 3D para aprobar.");
        }
    }
    public void DeleteModel()
    {
        if (item3DModel != null)
        {
            Destroy(item3DModel); // Eliminar el modelo de la escena
            item3DModel = null; // Limpiar la referencia
            isModelApproved = false; // Resetear el estado de aprobación
            Debug.Log("Modelo eliminado de la pantalla.");
        }
        else
        {
            Debug.LogWarning("No hay un modelo activo para eliminar.");
        }
    }
public void DeleteAllModels()
    {
        // Depuración: Lista las etiquetas activas en la escena
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            Debug.Log($"Objeto encontrado: {obj.name}, Etiqueta: {obj.tag}");
        }

        // Intentar encontrar los objetos con la etiqueta "InstantiatedModel"
        GameObject[] instantiatedModels = GameObject.FindGameObjectsWithTag("InstantiatedModel");

        if (instantiatedModels.Length == 0)
        {
            Debug.LogError("No se encontraron modelos con la etiqueta 'InstantiatedModel'.");
            return;
        }

        foreach (GameObject model in instantiatedModels)
        {
            Destroy(model); // Eliminar el objeto
            Debug.Log($"Modelo eliminado: {model.name}");
        }

        Debug.Log("Todos los modelos visibles han sido eliminados.");
    }
}
*/
