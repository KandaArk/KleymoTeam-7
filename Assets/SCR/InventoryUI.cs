using UnityEngine;
using UnityEngine.UI;

// Скрипт, связывающий инвентарь с UI
public class InventoryUI : MonoBehaviour
{
    // Контейнер, в который будут добавляться слоты
    public Transform slotsParent;

    // Префаб кнопки слота инвентаря
    public GameObject slotPrefab;

    private void Start()
    {
        // Подписываемся на событие добавления предмета
        Inventory.Instance.OnItemAdded += AddSlot;
    }

    private void OnDisable()
    {
        // Отписываемся от события при отключении объекта
        if (Inventory.Instance != null)
            Inventory.Instance.OnItemAdded -= AddSlot;
    }

    // Создание нового UI-слота
    private void AddSlot(ItemData item)
    {
        // Создаём кнопку-слот внутри контейнера
        GameObject slot = Instantiate(slotPrefab, slotsParent);

        // Назначаем иконку предмета
        Image icon = slot.GetComponentInChildren<Image>();
        if (icon != null)
            icon.sprite = item.icon;

        // Передаём данные предмета в логику слота
        InventorySlot slotLogic = slot.GetComponent<InventorySlot>();
        slotLogic.Init(item);
    }
}