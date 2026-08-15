using System;
using UnityEngine;

[Serializable]
public class DrawEffect : CardEffect
{
    public int cardsToDraw;
    public override void Resolve()
    {
        Debug.Log("Drew " + cardsToDraw + " card(s)");
    }
}
