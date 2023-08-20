using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSwitcher : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Button Components")]
    [SerializeField] private Image image;

    [Header("Sprite Components")]
    [SerializeField] private Sprite spriteOn;
    [SerializeField] private Sprite spriteOff;

    private void Start()
    {
        ButtonSwitch(false);
    }

    public void OnPointerEnter(PointerEventData eventData) => ButtonSwitch(true);

    public void OnPointerExit(PointerEventData eventData) => ButtonSwitch(false);

    private void ButtonSwitch(bool switcher)
    {
        image.sprite = switcher ? spriteOn : spriteOff;
    }
}