using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    public bool IsTargetable;
    public bool IsSelected { get; private set; }
    [SerializeField] private int _health = 10;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private SpriteRenderer _highlightedSprite;
    [SerializeField] private SpriteRenderer _selectedSprite;

    public event Action<Enemy, PointerEventData.InputButton> Clicked;

    private void Start()
    {
        _healthText.text = "HP: " + _health;
        SetHighlight(false);
        SetSelected(false);
        IsSelected = false;
    }
    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            _healthText.text = "HP: " + _health;
        }
        else
        {
            Debug.Log("I am dead");
        }
    }

    public void SetHighlight(bool highlighted)
    {
        _highlightedSprite.enabled = highlighted;
    }
    public void SetSelected(bool selected)
    {
        _selectedSprite.enabled = selected;
        IsSelected = selected;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsTargetable) return;

        Clicked?.Invoke(this, eventData.button);
    }
}