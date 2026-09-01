using TMPro;
using UnityEngine;

public class EnemyVisuals : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private SpriteRenderer _highlightedSprite;
    [SerializeField] private SpriteRenderer _selectedSprite;
    [SerializeField] private HealthBar _healthBar;
    private EnemyStats _enemyStats;
    

    private void Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
    }
    private void Start()
    {
        UpdateHP();
        SetHighlighted(false);
        SetSelected(false);
    }
    public void SetHighlighted(bool highlighted)
    {
        _highlightedSprite.enabled = highlighted;
    }
    public void SetSelected(bool selected)
    {
        _selectedSprite.enabled = selected;
    }
    public void UpdateHP()
    {
        _healthBar.UpdateHealth(_enemyStats.CurrentHP, _enemyStats.EnemyData.BaseHP);
    }
}