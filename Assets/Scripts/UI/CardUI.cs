using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardUI : MonoBehaviour
{
    [Header("Card Information")]
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _manaCostText;
    private CardData _cardData;
    public CardData Data => _cardData;

    public void Initialise(CardData data)
    {
        _cardData = data;
        UpdateVisuals();
    }
    public void UpdateVisuals()
    {
        _nameText.text = _cardData.CardName;
        _descriptionText.text = _cardData.CardDescription;
        _manaCostText.text = _cardData.ManaCost.ToString();
    }
}
