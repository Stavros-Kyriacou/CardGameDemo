using TMPro;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [Header("Card Information")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _manaCostText;
    [SerializeField] private TMP_Text _quantityText;
    private int _quantity;
    private CardData _cardData;
    public CardData Data => _cardData;
    public int Quantity => _quantity;

    public void Initialise(CardData data, int quantity)
    {
        _cardData = data;
        _quantity = quantity;
        UpdateVisuals();
    }
    public void UpdateVisuals()
    {
        _nameText.text = _cardData.CardName;
        _descriptionText.text = _cardData.CardDescription;
        _manaCostText.text = _cardData.ManaCost.ToString();
        _quantityText.text = "x" + _quantity.ToString();
    }
}
