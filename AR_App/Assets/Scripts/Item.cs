using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite ItemImage;
    public string ItemDescription;
    public GameObject Item3DModel;
}
