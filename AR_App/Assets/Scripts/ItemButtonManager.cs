using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButtonManager : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private Sprite itemImage;
    private GameObject item3DModel;
    private ARInteractionsManager interactionsManager;

    // Propiedades públicas para asignar valores
    public string ItemName
    {
        set { itemName = value; }
    }

    public string ItemDescription
    {
        set { itemDescription = value; }
    }

    public Sprite ItemImage
    {
        set { itemImage = value; }
    }

    public GameObject Item3DModel
    {
        set { item3DModel = value; }
    }

    void Start()
    {
        // Validar y asignar el nombre
        if (transform.childCount > 1 && transform.GetChild(1).GetComponent<TextMeshProUGUI>() != null)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemName;
        }
        else
        {
            Debug.LogError("El hijo 1 no tiene un componente TextMeshProUGUI o no existe.");
        }

        // Validar y asignar la imagen
        if (transform.childCount > 0 && transform.GetChild(0).GetComponent<RawImage>() != null)
        {
            if (itemImage != null)
            {
                transform.GetChild(0).GetComponent<RawImage>().texture = itemImage.texture;
            }
            else
            {
                Debug.LogWarning("ItemImage no está asignado o es nulo.");
            }
        }
        else
        {
            Debug.LogError("El hijo 0 no tiene un componente RawImage o no existe.");
        }

        // Configurar el botón si existe
        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(GameManager.instance.ARPosition);
            button.onClick.AddListener(Create3DModel);
            // Encontrar el ARInteractionsManager en la escena
            interactionsManager = FindAnyObjectByType<ARInteractionsManager>();
            if (interactionsManager == null)
            {
                Debug.LogError("No se encontró ARInteractionsManager en la escena.");
            }
        }
        else
        {
            Debug.LogError("El componente Button no está asignado en este GameObject.");
        }
    }

    private void Create3DModel()
    {
        if (item3DModel != null && interactionsManager != null)
        {
            // Instanciar el modelo 3D
            GameObject modelInstance = Instantiate(item3DModel);
            modelInstance.tag = "InstantiatedModel"; 
            modelInstance.AddComponent<ModelTouchRotation>(); 
            Debug.Log($"Etiqueta asignada al modelo: {modelInstance.tag}");
            Debug.Log($"Instanciando modelo 3D: {item3DModel.name}");

            // Asignarlo al ARInteractionsManager para posicionarlo
            interactionsManager.Item3DModel = modelInstance;

            Debug.Log($"Modelo 3D asignado al ARInteractionsManager: {modelInstance.name}");
        }
        else
        {
            Debug.LogError("El modelo 3D o ARInteractionsManager no están disponibles.");
        }
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButtonManager : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private Sprite itemImage;
    private GameObject item3DModel;
    private ARInteractionsManager interactionsManager;


    // Propiedades públicas para asignar valores
    public string ItemName
    {
        set { itemName = value; }
    }

    public string ItemDescription
    {
        set { itemDescription = value; }
    }

    public Sprite ItemImage
    {
        set { itemImage = value; }
    }

    public GameObject Item3DModel
    {
        set { item3DModel = value; }
    }


    void Start()
    {
        // Log de los hijos para confirmar estructura
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log($"Hijo {i}: {transform.GetChild(i).name}");
        }

        //if (transform.childCount > 1 && transform.GetChild(1).GetComponent<Text>() != null)transform.GetChild(1).GetComponent<Text>().text = itemName; else Debug.LogError("El hijo 1 no tiene un componente Text o no existe.");

        if (transform.childCount > 1 && transform.GetChild(1).GetComponent<TextMeshProUGUI>() != null)
        {
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemName;
        }
        else
        {
            Debug.LogError("El hijo 1 no tiene un componente TextMeshProUGUI o no existe.");
        }

        // Validar y asignar la imagen
        if (transform.childCount > 0 && transform.GetChild(0).GetComponent<RawImage>() != null)
        {
            if (itemImage != null)
            {
                transform.GetChild(0).GetComponent<RawImage>().texture = itemImage.texture;
            }
            else
            {
                Debug.LogWarning("ItemImage no está asignado o es nulo.");
            }
        }
        else
        {
            Debug.LogError("El hijo 0 no tiene un componente RawImage o no existe.");
        }

        // Configurar el botón si existe
        var button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(GameManager.instance.ARPosition);
            button.onClick.AddListener(Create3DModel);
            // interactionsManager = FindObjectType<ARInteractionsManager>();
            interactionsManager = FindAnyObjectByType<ARInteractionsManager>();

        }
        else
        {
            Debug.LogError("El componente Button no está asignado en este GameObject.");
        }
    }
    private void Create3DModel()
    {
        // Instantiate(item3DModel); 
        // Instancia el modelo 3D
        Vector3 position = new Vector3(0, 0, 0); // Cambia por la posición deseada
        Instantiate(item3DModel, position, Quaternion.identity);
        interactionsManager.item3DModel = Instantiate(item3DModel);
    }
}*/

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButtonManager : MonoBehaviour
{
    private string itemName;
    private string itemDescription;
    private Sprite itemImage;
    private GameObject item3DModel;

    // Propiedades públicas para asignar valores
    public string ItemName
    {
        set { itemName = value; }
    }

    public string ItemDescription
    {
        set { itemDescription = value; }
    }

    public Sprite ItemImage
    {
        set { itemImage = value; }
    }

    public GameObject Item3DModel
    {
        set { item3DModel = value; }
    }
    
    void Start()
{
    // Log de los hijos para confirmar estructura
    for (int i = 0; i < transform.childCount; i++)
    {
        Debug.Log($"Hijo {i}: {transform.GetChild(i).name}");
    }
     // Validar y asignar el nombre
    if (transform.childCount > 1 && transform.GetChild(1).GetComponent<TextMeshProUGUI>() != null)
    {
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemName;
    }
    else
    {
        Debug.LogError("El hijo 1 no tiene un componente TextMeshProUGUI o no existe.");
    }

    // Validar y asignar la imagen
    if (transform.childCount > 0 && transform.GetChild(0).GetComponent<RawImage>() != null)
    {
        if (itemImage != null)
        {
            transform.GetChild(0).GetComponent<RawImage>().texture = itemImage.texture;
        }
        else
        {
            Debug.LogWarning("ItemImage no está asignado o es nulo.");
        }
    }
    else
    {
        Debug.LogError("El hijo 0 no tiene un componente RawImage o no existe.");
    }

    // Configurar el botón si existe
    var button = GetComponent<Button>();
    if (button != null)
    {
        button.onClick.AddListener(GameManager.instance.ARPosition);
        button.onClick.AddListener(Create3DModel);
    }
    else
    {
        Debug.LogError("El componente Button no está asignado en este GameObject.");
    }
}



    private void Create3DModel()
    {
        // Instantiate(item3DModel); 
         Vector3 position = new Vector3(0, 0, 0); // Cambia por la posición deseada
        Instantiate(item3DModel, position, Quaternion.identity);
    }
}
*/
