using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

// UI-иконка выбранного предмета, которая следует за курсором
public class SelectedCursorUI : MonoBehaviour
{
    private Image image;
    private RectTransform rect;

    private void Awake()
    {
        // Берём компоненты UI-объекта
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        // Иконка не должна перехватывать клики мыши
        image.raycastTarget = false;

        // По умолчанию иконку скрываем
        image.enabled = false;
    }

    private void Start()
    {
        // Подписываемся на событие выбора предмета в инвентаре
        if (Inventory.Instance != null)
            Inventory.Instance.OnSelectionChanged += HandleSelectionChanged;
    }

    private void OnDisable()
    {
        // Отписываемся от события, когда объект отключается
        if (Inventory.Instance != null)
            Inventory.Instance.OnSelectionChanged -= HandleSelectionChanged;
    }

    // Обновляем иконку, когда меняется выбранный предмет
    private void HandleSelectionChanged(ItemData selected)
    {
        if (selected == null)
        {
            image.enabled = false;
            return;
        }

        image.sprite = selected.icon;
        image.enabled = true;
    }

    private void Update()
    {
        // Если иконка скрыта — позицию не обновляем
        if (!image.enabled) return;

        // Двигаем иконку за курсором (небольшое смещение, чтобы было видно)
        rect.position = Mouse.current.position.ReadValue() + new Vector2(24, -24);
    }
}
