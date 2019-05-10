using UnityEngine;
using System.Collections;

public class CardBox1Behaviour : InteractableItemBehaviour
{
    [SerializeField] CharacterBehaviour character;
    [SerializeField] Animator gateAnimator;
    [SerializeField] string cardName;
    [SerializeField] string cardName2;
    private bool _canOpenDoor = false;

    protected override void ExecuteAction(Collider other)
    {
        gateAnimator.SetBool("isOpen", true);
        if (character && character.CheckInventaryObjectOnSelectedPosition(cardName))
        {
            gateAnimator.SetBool("isOpen", true);
            SetActive(false);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("InsertedKeycard", false);
        }
        else if (character && character.CheckInventaryObjectOnSelectedPosition(cardName2))
        {
            _canOpenDoor = true;
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("InsertedKeycard", false);
        }

    }

    /*private void Update()
    {
        if (_canOpenDoor && gate2.transform.position.y > yPosition)
        {
            Vector3 target = new Vector3(gate2.transform.position.x, yPosition, gate2.transform.position.z);
            gate2.transform.position = Vector3.MoveTowards(gate2.transform.position, target, speed * Time.deltaTime);
        }
    }*/


}
