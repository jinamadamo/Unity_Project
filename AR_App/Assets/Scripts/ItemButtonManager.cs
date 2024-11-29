using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // Asigna el texto a los componentes hijos
        transform.GetChild(0).GetComponent<Text>().text = itemName;
        transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture; // Usa la textura de la imagen
        transform.GetChild(2).GetComponent<Text>().text = itemDescription;

        // Configura el botón
        var button = GetComponent<Button>(); // Cambiado a "Button" (con mayúscula)
        button.onClick.AddListener(GameManager.instance.ARPosition); // Supongo que ARPosition es un método estático
        button.onClick.AddListener(Create3DModel);
    }

    private void Create3DModel()
    {
        if (item3DModel != null)
        {
            Instantiate(item3DModel); // Instancia el modelo 3D
        }
        else
        {
            Debug.LogError("Item3DModel no está asignado.");
        }
    }
}
