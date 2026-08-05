using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<CardData> cards = new List<CardData>();

    void Start()
    {
        foreach (var card in cards)
        {
            Debug.Log("Casting " + card.cardName);
            foreach (var effect in card.effects)
            {

                effect.Resolve();
            }
        }
    }
}
