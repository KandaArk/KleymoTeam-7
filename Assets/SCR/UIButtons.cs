using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtons : MonoBehaviour
{
    [Header("Ссылки")]
    [Tooltip("Панель, которую нужно показывать/скрывать")]
    [SerializeField] private RectTransform InventoryPanel;
    [SerializeField] private RectTransform SettingsPanel;

    [Tooltip("Кнопка на сцене, которая открывает панель")]
    [SerializeField] private Button openInventoryButton;
    [SerializeField] private Button openSettingsButton;

    [Header("Настройки")]
    [Tooltip("Клавиша для открытия/закрытия")]
    [SerializeField] private KeyCode toggleKey = KeyCode.E;

    [Tooltip("Скорость анимации появления")]
    [SerializeField] private float animationSpeed = 8f;

    [Header("Позиции панели")]
    [Tooltip("Позиция, когда панель скрыта (например, за пределами экрана)")]
    [SerializeField] private Vector2 InventoryHiddenPosition;

    [Tooltip("Позиция, когда панель показана")]
    [SerializeField] private Vector2 InventoryShownPosition;

    private bool isOpen = false;
    private Coroutine animationCoroutine;

    private void Start()
    {
        // Ставим панель в скрытое положение при старте
        InventoryPanel.anchoredPosition = InventoryHiddenPosition;
        InventoryPanel.gameObject.SetActive(true); // панель должна быть активна, чтобы анимация работала

        // Подписываемся на кнопку
        if (openInventoryButton != null)
            openInventoryButton.onClick.AddListener(TogglePanel);
    }

    private void Update()
    {
        // Обработка нажатия клавиши
        if (Input.GetKeyDown(toggleKey))
        {
            TogglePanel();
        }
    }

    public void TogglePanel()
    {
        isOpen = !isOpen;

        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        Vector2 targetPos = isOpen ? InventoryShownPosition : InventoryHiddenPosition;
        animationCoroutine = StartCoroutine(AnimatePanel(targetPos));
    }

    private IEnumerator AnimatePanel(Vector2 targetPos)
    {
        while (Vector2.Distance(InventoryPanel.anchoredPosition, targetPos) > 0.5f)
        {
            InventoryPanel.anchoredPosition = Vector2.Lerp(
                InventoryPanel.anchoredPosition,
                targetPos,
                Time.deltaTime * animationSpeed
            );
            yield return null;
        }

        InventoryPanel.anchoredPosition = targetPos;
    }

    private void OnDestroy()
    {
        if (openInventoryButton != null)
            openInventoryButton.onClick.RemoveListener(TogglePanel);
    }
}
