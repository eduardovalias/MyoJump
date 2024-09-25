using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class playSprite : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite heldSprite;
    private Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = defaultSprite;
    }

    // Detecta quando o botão é pressionado
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = heldSprite;  // Muda para o sprite de "segurando"
    }

    // Detecta quando o botão é liberado
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.sprite = defaultSprite;  // Volta ao sprite original ao soltar
    }
}
