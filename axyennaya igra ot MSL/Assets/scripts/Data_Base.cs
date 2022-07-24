using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data_Base : MonoBehaviour
{
    public List<Item> items = new List<Item>();
}

[System.Serializable]

public class Item
{
    public bool draggable;
    public int id;
    public int maxItem;
    public string name;
    public Sprite image;
}