using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class SpecialItemCanvasBehaviour : MonoBehaviour
{
    public void AnimateItemsJoin()
    {
        GameEvents.UIEvents.TriggerItemsJoinAnimation.SafeInvoke();
    }

}
