using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public GameManager gameManager;

    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private Card cardPrefab;

    private Sprite[] _cardSprites;
    private List<Card> _cards;
    private List<Card> _clearedCards;
    private int[] _shuffledCardsIndex;
    private Card _firstSelectedCard;
    private Card _secondSelectedCard;
    private void Awake()
    {
        Instance = this;
        _cardSprites = Resources.LoadAll<Sprite>("AllCards"); // Get all sprites from Resources/AllCards folder.

    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnCards();
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
        _clearedCards = new List<Card>(_cards.Capacity);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < columns; j++)
            {
                var cell = i * columns + j;
                var card = Instantiate(cardPrefab, gridLayoutGroup.transform);
                var cardId = _shuffledCardsIndex[cell];
                var frontImage = _cardSprites[cardId];
                card.Setup(cardId,frontImage);
                _cards.Add(card);
            }
        }
    }

    public void CardFlipped(Card selectedCard)
    {
        selectedCard.ShowFront();

        if (_firstSelectedCard == null)
        {
            _firstSelectedCard = selectedCard;
        }
        else if (_secondSelectedCard == null)
        {
            _secondSelectedCard = selectedCard;
            StartCoroutine(CheckCardMatch(_firstSelectedCard, _secondSelectedCard));
        }
    }

    IEnumerator CheckCardMatch(Card firstSelection, Card secondSelection) // Check is card match or not.
    {
        _firstSelectedCard = null;
        _secondSelectedCard = null;

        if (firstSelection.CardId == secondSelection.CardId)
        {
            Debug.Log("Card Match");
            _clearedCards.Add(firstSelection);
            _clearedCards.Add(secondSelection);
            CheckForLevelComplete();
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            Debug.Log("Card Not Match");
            firstSelection.ShowBack();
            secondSelection.ShowBack();
        }
    }

    private void CheckForLevelComplete()
    {
        if (_clearedCards.Count == _cards.Count)
        {
            _clearedCards.Clear();
            _cards.Clear();
            gameManager.LevelCompleted();
        }
    }
}