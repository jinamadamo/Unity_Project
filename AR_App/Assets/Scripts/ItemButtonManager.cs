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

    /*void Start()
    {
        // Asigna el texto a los componentes hijos
        // Debug para verificar que los datos están siendo asignados correctamente
        Debug.Log($"ItemName: {itemName}, ItemDescription: {itemDescription}, ItemImage: {itemImage}, Item3DModel: {item3DModel}");

         // Asegúrate de que los hijos existen antes de acceder
    if (transform.childCount > 0 && transform.GetChild(0).GetComponent<Text>() != null)
    {
        transform.GetChild(0).GetComponent<Text>().text = itemName;
    }
    else
    {
        Debug.LogError("El hijo 0 no tiene un componente Text o no existe.");
    }

    if (transform.childCount > 1 && transform.GetChild(1).GetComponent<RawImage>() != null && itemImage != null)
    {
        transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
    }
    else
    {
        Debug.LogError("El hijo 1 no tiene un componente RawImage, o itemImage es nulo.");
    }

    if (transform.childCount > 2 && transform.GetChild(2).GetComponent<Text>() != null)
    {
        transform.GetChild(2).GetComponent<Text>().text = itemDescription;
    }
    else
    {
        Debug.LogError("El hijo 2 no tiene un componente Text o no existe.");
    }

        // Configura el botón
        var button = GetComponent<Button>(); // Cambiado a "Button" (con mayúscula)
        if (button != null)
        {
            button.onClick.AddListener(GameManager.instance.ARPosition);
            button.onClick.AddListener(Create3DModel);
        }
        else
        {
            Debug.LogError("El componente Button no está asignado en este GameObject.");
        }
    }*/
    
    void Start()
{
    // Log de los hijos para confirmar estructura
    for (int i = 0; i < transform.childCount; i++)
    {
        Debug.Log($"Hijo {i}: {transform.GetChild(i).name}");
    }

    /*// Validar y asignar el nombre
    if (transform.childCount > 1 && transform.GetChild(1).GetComponent<Text>() != null)
    {
        transform.GetChild(1).GetComponent<Text>().text = itemName;

    }
    else
    {
        Debug.LogError("El hijo 1 no tiene un componente Text o no existe.");
    }*/
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
        // Instancia el modelo 3D
         Vector3 position = new Vector3(0, 0, 0); // Cambia por la posición deseada
        Instantiate(item3DModel, position, Quaternion.identity);
    }
}
