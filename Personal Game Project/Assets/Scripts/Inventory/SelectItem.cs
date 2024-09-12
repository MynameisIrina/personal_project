using UnityEngine;

public class SelectItem : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform weaponInHand;
    [SerializeField] private Transform playerSpine;
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject gun;
    private GameObject selectedItem;
    private bool selectItem;
    private Item.ItemType currentItem;


    // Update is called once per frame
    void Update()
    {
        /*
         * select item from inventory based on the current position of the border
         */

        if (selectItem && playerController.GetInventory().itemList.Count > 0)
        {
            if (currentItem == Item.ItemType.Arrow)
            {
                gun.SetActive(false);
                sword.SetActive(false);
                bow.SetActive(true);
                arrow.SetActive(true);
            }
            else if (currentItem == Item.ItemType.Sword)
            {
                gun.SetActive(false);
                bow.SetActive(false);
                arrow.SetActive(false);
                sword.SetActive(true);
            }
            else if (currentItem == Item.ItemType.Gun)
            {
                arrow.SetActive(false);
                sword.SetActive(false);
                bow.SetActive(false);
                gun.SetActive(true);
            }
        }
    }

    public bool ifItemisSelected()
    {
        if (gun.activeSelf || bow.activeSelf || sword.activeSelf)
        {
            return true;
        }

        return false;
    }

    public void ReceiveSelectItem(bool _selectItem, Item.ItemType _currentItem)
    {
        selectItem = _selectItem;
        currentItem = _currentItem;
    }
}