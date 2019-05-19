using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class InventoryObjectBehaviour : MonoBehaviour
{

    [SerializeField] public string Name;
    [SerializeField] public Image objectImage;
    [SerializeField] public InventoryCenterBehaviour inventaryCenter;
    [SerializeField] public int Position;
    [SerializeField] public AudioClip _audioClip;
    private Animator _animator;
    private bool _isEnabled = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isEnabled)
        {
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            if (character != null)
            {
                _isEnabled = false;
                GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
                StartCoroutine(WaitToIncludeOnInventary(1f));
            }
        }
    }

    IEnumerator WaitToIncludeOnInventary(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        IncludeItemOnInventary();
        PlayClipAtPosition();
    }

    private void PlayClipAtPosition()
    {
        if (_audioClip != null) AudioSource.PlayClipAtPoint(_audioClip, this.transform.position);
    }

    private void IncludeItemOnInventary()
    {
        if (_animator != null)
        {
            _animator.SetBool("IsGoingToInventary", true);
        }
        inventaryCenter.AddNewItem(this);
        StartCoroutine(WaitToCloseOrOpenInventary(1));
        StartCoroutine(WaitToCloseOrOpenInventary(2));
        if (_animator == null)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator WaitToCloseOrOpenInventary(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        inventaryCenter.CloseOrOpen();
    }
}
