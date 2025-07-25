using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private Card cardPrefab;

    private Sprite[] _cardSprites;
    private List<Card> _cards;
    private int[] _shuffledCardsIndex;

    private void Awake()
    {
        _cardSprites = Resources.LoadAll<Sprite>("AllCards"); // Get all sprites from Resources/AllCards folder.

    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnCards()
    {
        var rows = 2;
        var columns = 2;
        _shuffledCardsIndex = new int[rows * columns];
        for (var i = 0; i < _shuffledCardsIndex.Length; i++)
        {
            _shuffledCardsIndex[i] = i / 2; // Setup pair of cards.
        }

        // update grid layout
        gridLayoutGroup.constraintCount = rows;
        _cards = new List<Card>(rows * columns);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                var cell = i * columns + j;
                var card = Instantiate(cardPrefab, gridLayoutGroup.transform);
                var cardId = _shuffledCardsIndex[cell];
                var frontImage = _cardSprites[cardId];
                card.Setup(frontImage);
                _cards.Add(card);
            }
        }
    }
}