using DG.Tweening;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private SpriteRenderer _primaryFill;
    [SerializeField] private SpriteRenderer _secondaryFill;
    [SerializeField][Range(0f, 0.5f)] private float _primaryFillAnimationDuration;
    [SerializeField][Range(0f, 2f)] private float _secondaryFillAnimationDuration;

    public void UpdateHealth(float currentHP, float maxHP)
    {
        float fillPercent = currentHP / maxHP;
        Vector3 fillScale = new Vector3(fillPercent, 1, 1);
        // var animationVariance = _secondaryFillAnimationDuration / 10;
        // var randomSecondaryAnimationDuration = Random.Range(_secondaryFillAnimationDuration - animationVariance, _secondaryFillAnimationDuration + animationVariance);

        _primaryFill.transform.DOScale(fillScale, _primaryFillAnimationDuration);
        _secondaryFill.transform.DOScale(fillScale, _secondaryFillAnimationDuration);

        _healthText.text = $"{currentHP}/{maxHP}";
    }
}
