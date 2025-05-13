using System.Buffers;
using UnityEngine;

public class ClickVoid : MonoBehaviour,OnClick
{
    int i = 1;
    public Inventory_ItemSlot OnItemClick()
    {
        i = 0;
        return null;
    }
}
