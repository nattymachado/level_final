using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class SpecialItemsCanvasBehaviour : MonoBehaviour
{
    public void AnimateItemsJoin()
    {
        GameEvents.UIEvents.TriggerItemsJoinAnimation.SafeInvoke();
    }

}
