using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialFadePanel : MonoBehaviour, IPointerClickHandler
{
    private Animator animator;
    private bool isVisible;
    private Coroutine fadeout;
    [SerializeField] float defaultFadeOutTime = 5f;
    public bool IsVisible { get { return isVisible; } }

    void OnEnable()
    {
        GameEvents.LevelEvents.OpenedInventory += Hide;
    }

    void OnDisable()
    {
        GameEvents.LevelEvents.OpenedInventory = Hide;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowSpecials()
    {
        animator.SetBool("visible", true);
        animator.SetBool("specials", true);
        isVisible = true;
        if (fadeout != null) StopCoroutine(fadeout);
        fadeout = StartCoroutine(FadeoutAfterTime());
    }

    public void ShowItens()
    {
        animator.SetBool("visible", true);
        animator.SetBool("itens", true);
        isVisible = true;
        if (fadeout != null) StopCoroutine(fadeout);
        fadeout = StartCoroutine(FadeoutAfterTime());
    }

    public void Hide()
    {

        if (!isVisible)
        {
            return;
        }
        animator.SetBool("visible", false);
        animator.SetBool("specials", false);
        animator.SetBool("itens", false);
        isVisible = false;
        if (fadeout != null) StopCoroutine(fadeout);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("ue");
        Hide();
    }

    private IEnumerator FadeoutAfterTime()
    {
        yield return new WaitForSeconds(defaultFadeOutTime);
        Hide();
    }
}
