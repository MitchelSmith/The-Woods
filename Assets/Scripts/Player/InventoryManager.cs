using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    public void PickupCollectable(RaycastHit hit)
    {
        string tag = hit.transform.tag;

        if (inventory.ContainsKey(tag))
        {
            // Add to already tracked object count
            inventory[tag]++;
        }
        else
        {
            // Start tracking new object count
            inventory.Add(tag, 1);
        }
    }

    public int GetCountOfCollectable(RaycastHit hit)
    {
        string tag = hit.transform.tag;
        int count = 0;

        if (inventory.ContainsKey(tag))
        {
            count = inventory[tag];
        }

        return count;
    }
}
