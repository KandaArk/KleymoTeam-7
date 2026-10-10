using System;
using System.Collections.Generic;
using UnityEngine;

// Инвентарь для хранения данных предметов
public class Inventory : MonoBehaviour
{
    // Статичная ссылка на инвентарь (Singleton)
    public static Inventory Instance;

    // Текущий выбранный предмет
    public ItemData SelectedItem { get; private set; }

    // Список предметов в инвентаре
    public List<ItemData> items = new();

    // Событие, которое вызывается при добавлении предмета
    public event Action<ItemData> OnItemAdded;
    // Событие, которое вызывается при смене выбранного предмета
    public event Action<ItemData> OnSelectionChanged;

    private void Awake()
    {
        // Проверяем, чтобы в сцене был только один инвентарь
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Добавление предмета в инвентарь
    public void Add(ItemData item)
    {
        if (item == null) return;

        items.Add(item);

        // Уведомляем подписчиков о добавлении предмета
        OnItemAdded?.Invoke(item);
    }

    // Выбор предмета в инвентаре
    public void Select(ItemData item)
    {
        SelectedItem = item;
        // Уведомляем подписчиков об изменении выбранного предмета
        OnSelectionChanged?.Invoke(SelectedItem);
    }

    // Сброс выбранного предмета
    public void ClearSelection()
    {
        SelectedItem = null;
        // Уведомляем подписчиков об изменении выбранного предмета
        OnSelectionChanged?.Invoke(SelectedItem);
    }
}