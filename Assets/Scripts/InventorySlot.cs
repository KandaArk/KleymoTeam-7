using UnityEngine;
using UnityEngine.EventSystems;

// Скрипт для UI-слота инвентаря
public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    // Данные предмета, связанного с этим слотом
    private ItemData item;

    // Инициализация слота данными предмета
    public void Init(ItemData data)
    {
        item = data;
    }

    // Обработка кликов по UI-кнопке
    public void OnPointerClick(PointerEventData eventData)
    {
        // Левый клик — показать описание предмета
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ItemDescriptionUI.Instance.Show(item);
        }
        // Правый клик — выбрать или снять выбор предмета
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (Inventory.Instance.SelectedItem == item)
                Inventory.Instance.ClearSelection();
            else
                Inventory.Instance.Select(item);
        }
    }
}