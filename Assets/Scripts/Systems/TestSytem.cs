using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSytem : MonoBehaviour
{
    [SerializeField] private HandView handView;
    private InputAction createCardAction;

    private void Awake()
    {
        createCardAction = InputSystem.actions.FindAction("Player/CreateCard");
    }
    void Update()
    {
        if (createCardAction.WasPressedThisFrame())
        {
            CreateCard();
        }
    }
    private void CreateCard()
    {
        if (handView.CardsInHand() >= CardViewCreator.Instance.maxHandSize) return;

        CardView cardView = CardViewCreator.Instance.CreateCardView(transform.position, Quaternion.identity);
        StartCoroutine(handView.AddCard(cardView));

    }
}
