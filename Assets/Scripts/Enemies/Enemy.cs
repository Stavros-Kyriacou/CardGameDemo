using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    private bool _isTargetable = false;
    private bool _isSelected = false;
    public bool IsTargetable => _isTargetable;
    public bool IsSelected => _isSelected;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private SpriteRenderer _highlightedSprite;
    

    private EnemyStats _enemyStats;
    private EnemyVisuals _enemyVisuals;
    public EnemyStats EnemyStats => _enemyStats;
    public EnemyVisuals EnemyVisuals => _enemyVisuals;

    public event Action<Enemy, PointerEventData.InputButton> Clicked;
    void Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
        _enemyVisuals = GetComponent<EnemyVisuals>();
    }
    
    public void SetTargetable(bool targetable)
    {   
        _isTargetable = targetable;
    }
    public void SetSelected(bool selected)
    {
        _enemyVisuals.SetSelected(selected);
        _isSelected = selected;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsTargetable) return;

        Clicked?.Invoke(this, eventData.button);
    }
}