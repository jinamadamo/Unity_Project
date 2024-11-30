using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private ItemButtonManager itemButtonManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OnItemsMenu += CreateButtons;
    }

    private void CreateButtons()
    {
        foreach (var item in items)
        {
            ItemButtonManager itemButton = Instantiate(itemButtonManager, buttonContainer.transform);
            itemButton.ItemName = item.ItemName;
            itemButton.ItemDescription = item.ItemDescription;
            itemButton.ItemImage = item.ItemImage;
            itemButton.Item3DModel = item.Item3DModel;
            itemButton.name = item.ItemName;
            // Debug para verificar que los datos están siendo asignados correctamente
        Debug.Log($"Nombre: {item.ItemName}, Descripción: {item.ItemDescription}, Imagen: {item.ItemImage}, Modelo: {item.Item3DModel}");
    
        }

        GameManager.instance.OnItemsMenu -= CreateButtons;
    }
}
