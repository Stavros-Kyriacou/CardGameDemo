using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerClickHandler
{
    private int _health = 10;
    public void TakeDamage(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            Debug.Log("Took " + damage + " damage");
        }
        else
        {
            Debug.Log("I am dead");
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicked");
        Debug.Log(eventData);
    }
}
