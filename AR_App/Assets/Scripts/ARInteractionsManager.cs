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
  //  private bool isModelApproved = false;

    public GameObject Item3DModel
    {
        get { return item3DModel; }
        set
        {
            if (value != null)
            {
                item3DModel = value;

                // Calcular la posición frente a la cámara
                Vector3 cameraForward = arCamera.transform.forward; // Dirección frente a la cámara
                Vector3 cameraPosition = arCamera.transform.position;

                // Ajustar la posición a una distancia fija
                float distanceFromCamera = 1.0f; // Cambiar si se requiere mayor o menor distancia
                Vector3 positionInFrontOfCamera = cameraPosition + cameraForward * distanceFromCamera;

                // Asignar la posición y orientación
                item3DModel.transform.position = positionInFrontOfCamera;
                item3DModel.transform.rotation = Quaternion.LookRotation(cameraForward); // Orientación hacia la cámara

                // Agregar el script de rotación táctil al modelo
                item3DModel.AddComponent<ModelTouchRotation>();
                
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
        if (item3DModel != null /*&& !isModelApproved*/)
        {
            // Eliminar el modelo actual si regresamos al menú principal
            Destroy(item3DModel);
            item3DModel = null;
            Debug.Log("Modelo no aprobado eliminado al regresar al menú principal.");
        }
    }

    /* public void DeleteItem()
     {
         // Método para eliminar el modelo manualmente desde un botón u otra lógica
         if (item3DModel != null)
         {
             Destroy(item3DModel);
             Debug.Log("Modelo 3D eliminado manualmente.");
         }
     }*/
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
          //  isModelApproved = false;

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


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteractionsManager : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject arPointer;
    public GameObject item3DModel;
    private bool isInitialPosition;

    public GameObject Item3DModel
    {
        get { return item3DModel; }
        set
        {
            item3DModel = value;
            item3DModel.transform.position = arPointer.transform.position;
            item3DModel.transform.parent = arPointer.transform;
        }

    }

    void Start()
    {
        arPointer = transform.GetChild(0).gameObject;
       // arRaycastManager = FindObjectOfType<ARRaycastManager>();
       arRaycastManager = FindFirstObjectByType<ARRaycastManager>();
        GameManager.instance.OnMainMenu += SetItemPosition;
    }

    void Update()
    {
        if (isInitialPosition)
        {
            Vector2 middlePointScreen = new Vector2(Screen.width / 2, Screen.height / 2);
            arRaycastManager.Raycast(middlePointScreen, hits, TrackableType.Planes);

            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
                transform.rotation = hits[0].pose.rotation;
                arPointer.SetActive(true);
                isInitialPosition = false;
            }
        }
    }

    private void SetItemPosition()
    {
        if (item3DModel != null)
        {
            item3DModel.transform.parent = null;
            arPointer.SetActive(false);
            item3DModel = null;
        }
    }

    public void DeleteItem()
    {
        Destroy(item3DModel);
        arPointer.SetActive(false);
    }
}*/



/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARInteractionsManager : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject arPointer;
    private GameObject item3DModel;
    private bool isInitialPosition;

    public GameObject Item3DModel
    {
        set
        {
            item3DModel = value;
            item3DModel.transform.position = arPointer.transform.position;
            item3DModel.transform.parent = arPointer.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        arPointer = transform.GetChild(0).gameObject;
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        GameManager.instance.OnMainMenu += SetItemPosition;
    }

// Update is called once per frame
void Update()
{
    if (isInitialPosition)
    {
        Vector2 middlePointScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        arRaycastManager.Raycast(middlePointScreen, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            arPointer.SetActive(true);
            isInitialPosition = false;
        }
    }
}

// Método para establecer la posición del objeto 3D
private void SetItemPosition()
{
    if (item3DModel != null)
    {
        item3DModel.transform.parent = null;
        arPointer.SetActive(false);
        item3DModel = null;
    }
}

// Método para eliminar el objeto 3D
public void DeleteItem()
{
    Destroy(item3DModel);
    arPointer.SetActive(false);
}

}
*/