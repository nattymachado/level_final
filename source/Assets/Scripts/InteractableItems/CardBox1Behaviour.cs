using UnityEngine;
using System.Collections;

public class CardBox1Behaviour : InteractableItemBehaviour
{
    [SerializeField] Animator gateAnimator;
    [SerializeField] Animator gateAnimator2;
    [SerializeField] string cardName;
    [SerializeField] string cardName2;
    private int _cardNumber = 0;
    private bool _canOpenDoor = false;

    protected override void ExecuteAction(CharacterBehaviour character)
    {
        _cardNumber = 0;
        if (character && character.CheckInventaryObjectOnSelectedPosition(cardName))
        {
            _cardNumber = 1;
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("InsertedKeycard", false, false);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            StartCoroutine(WaitToOpenGate(0.1f, _cardNumber));
        }
        else if (character && character.CheckInventaryObjectOnSelectedPosition(cardName2))
        {
            _cardNumber = 2;
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("InsertedKeycard", false, false);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            StartCoroutine(WaitToOpenGate(0.5f, _cardNumber));
        }
    }

    IEnumerator WaitToOpenGate(float seconds, int card)
    {
        yield return new WaitForSeconds(seconds);
        OpenGate(card);
    }

    private void OpenGate(int card)
    {
        if (card == 1)
        {
            gateAnimator.SetBool("isOpen", true);
            SetActive(false);
        }
        else if (card == 2)
        {
            gateAnimator2.SetBool("isOpen", true);
            SetActive(false);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("TechDoor", false, false);
        }
    }

}
