using UnityEngine;
public class Item : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int itemID;
    [HideInInspector]
    public bool pickedUp;

}