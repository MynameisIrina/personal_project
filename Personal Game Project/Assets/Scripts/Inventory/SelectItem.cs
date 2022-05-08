using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private Transform weaponInHand;
    [SerializeField] private Transform playerSpine;
    private GameObject bow;
    private GameObject quiver_arrow;
    private GameObject sword;
    private GameObject selectedItem;

    private bool selectItem;

    private Item.ItemType currentItem;
    // Start is called before the first frame update
    void Start()
    {
        bow = weaponInHand.Find("bow").gameObject;
        quiver_arrow = playerSpine.Find("bow_quiver").gameObject;
        sword = weaponInHand.Find("sword1").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectItem)
        {
            if (currentItem == Item.ItemType.Arrow)
            {
                bow.SetActive(true);
                quiver_arrow.SetActive(true);
                sword.SetActive(false);
            }
            else if (currentItem == Item.ItemType.Sword)
            {
                bow.SetActive(false);
                quiver_arrow.SetActive(false);
                sword.SetActive(true);
            }
            else if (currentItem == Item.ItemType.Potion)
            {
                
            }
            
        }
        
    }
    
    public void ReceiveSelectItem(bool _selectItem, Item.ItemType _currentItem)
    {
        selectItem = _selectItem;
        currentItem = _currentItem;
    }
}
