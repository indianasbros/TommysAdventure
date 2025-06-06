using UnityEngine;
public class Item : MonoBehaviour
{
    public string itemName;
    public string description;
    public Sprite icon;
    public string itemID;
    [HideInInspector]
    public bool pickedUp;

}