using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Image borderImage;

    [SerializeField]
    private Text quantityTxt;

    public event Action<InventoryItem> onItemClicked, onItemRightMouseClicked, onItemDroppedOn, onItemBeginDrag, onItemEndDrag;

    private bool empty = true;


    public void Awake()
    {
        ResetData();
        DeselectData();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        empty = true;

    }
    private void DeselectData()
    {
        borderImage.enabled = false;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = quantity + "";
        empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }
    public void onBeginDrag()
    {
        if (empty)
            return;
        onItemBeginDrag?.Invoke(this);
    }
    public void onDrop()
    {
        onItemDroppedOn?.Invoke(this);
    }
    public void onEndDrag()
    {
        onItemEndDrag?.Invoke(this);
    }
    public void onPointerClick(BaseEventData data)
    {
        if (empty)
            return;
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            onItemRightMouseClicked?.Invoke(this);
        }
        else
        {
            onItemClicked?.Invoke(this);
        }
    }
}
