using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private Sprite _cardFrontImage;
    private Image _cardImage;

    [SerializeField]
    private Sprite cardBackImage;

    void Awake()
    {
        _cardImage = GetComponent<Image>();
    }

    public void Setup(Sprite frontImage)
    {
        _cardFrontImage = frontImage;
        GetComponent<Button>().onClick.AddListener(OnClicked);
        ShowBack();
    }

    private void OnClicked()
    {
        Debug.Log("OnClicked");
        ShowFront();
    }

    public void ShowFront()
    {
        Debug.Log("ShowFront");
        _cardImage.sprite = _cardFrontImage;
    }

    public void ShowBack()
    {
        Debug.Log("ShowBack");
        _cardImage.sprite = cardBackImage;
    }
}
