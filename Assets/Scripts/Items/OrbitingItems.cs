﻿using UnityEngine;

public class OrbitingItems : MonoBehaviour
{
    string whoHasItems;
    // Start is called before the first frame update
    void Start()
    {
        whoHasItems = transform.parent.parent.parent.parent.gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Opponent" && other.gameObject.name != whoHasItems)
        {
            OpponentItemManager itemManage = other.gameObject.GetComponent<OpponentItemManager>();
            //the person who has the items
            GameObject itemOwner = GameObject.Find(whoHasItems);
            if (itemOwner.tag == "Player")
            {
                if (gameObject.tag == "Shell")
                {
                    itemManage.HitByShell();
                }
                else
                {
                    itemManage.HitByBanana();
                }
                itemOwner.GetComponent<ItemManager>().tripleItemCount--;
                int item_index = itemOwner.GetComponent<ItemManager>().item_index;
                gameObject.SetActive(false);

                if (itemOwner.GetComponent<ItemManager>().tripleItemCount < 1)
                {
                    itemOwner.GetComponent<ItemManager>().item_gameobjects[item_index].SetActive(false);
                    itemOwner.GetComponent<ItemManager>().item_gameobjects[item_index].transform.GetChild(0).gameObject.SetActive(true);
                    itemOwner.GetComponent<ItemManager>().item_gameobjects[item_index].transform.GetChild(1).gameObject.SetActive(true);
                    itemOwner.GetComponent<ItemManager>().item_gameobjects[item_index].transform.GetChild(2).gameObject.SetActive(true);
                    itemOwner.GetComponent<ItemManager>().OnUsedItemDone();
                }
            }
        }
    }
}
