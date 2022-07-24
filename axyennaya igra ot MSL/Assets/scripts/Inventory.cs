using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public Data_Base data;

    public List<ItemInventory> items = new List<ItemInventory>();

    public GameObject GameObjShow;

    public GameObject InventoryMainObj;
    public int maxCount;

    public Camera cam;
    public EventSystem es;

    public int currentID;
    public ItemInventory currentItem;

    public RectTransform movingObject;
    public Vector3 offset;

    int randomItem;

    public GameObject backGround;

    public void Start()
    {
        if (items.Count == 0)
        {
            AddGraphics();
        }

        for (int i = 0; i < maxCount; i++)
        {
            randomItem = Random.Range(0, data.items.Count);
            if (randomItem == 0)
            {
                AddItem(i, data.items[randomItem], 0);
            }
            else
            {
                AddItem(i, data.items[randomItem], Random.Range(1, data.items[randomItem].maxItem));
            }
            
        }
        UpdateInventory();
    }

    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            backGround.SetActive(!backGround.activeSelf);
            if (backGround.activeSelf)
            {
                UpdateInventory();
            }
        }
    }

    public void SearchForSameItem(Item item, int count)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[0].count < data.items[i].maxItem)
            {
                items[i].count += count;
                if (items[i].count > data.items[i].maxItem)
                {
                    count = items[i].count - data.items[i].maxItem;
                    items[i].count = 32;
                }
                else
                {
                    count = 0;
                    i = maxCount;
                }
            }
        }
        if (count > 0)
        {
            for (int i = 0; i< maxCount; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }

    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.image;

        if (count >= 2)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].image;

        if (invItem.count > 1)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(GameObjShow, InventoryMainObj.transform) as GameObject;

            newItem.name = i.ToString();
            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;
            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { if (data.items[currentItem.id].draggable) { SelectObject(); } });

            items.Add(ii);
        }
    }

    public void UpdateInventory()
    {
        for(int i =0; i < maxCount; i++)
        {
            if (items[i].id !=0 && items[i].count > 1)
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
            }

            items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].image;
        }
    }

    public void SelectObject()
    {
     
            if (currentID == -1)
            {
                currentID = int.Parse(es.currentSelectedGameObject.name);
                currentItem = CopyInventoryItem(items[currentID]);
                movingObject.gameObject.SetActive(true);
                movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].image;

                AddItem(currentID, data.items[0], 0);
            }
            else
            {
                ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

                if (currentItem.id != II.id)
                {
                    AddInventoryItem(currentID, II);

                    AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
                }
                else
                {
                    if (II.count + currentItem.count <= data.items[II.id].maxItem)
                    {
                        II.count += currentItem.count;
                    }
                    else
                    {
                        AddItem(currentID, data.items[II.id], II.count + currentItem.count - data.items[II.id].maxItem);
                        II.count = data.items[II.id].maxItem;
                    }
                    II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
                }
                currentID = -1;

                movingObject.gameObject.SetActive(false);
            }
        
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObj.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemGameObj = old.itemGameObj;
        New.count = old.count;

        return New;
    }
}

[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;
    public int count;
}