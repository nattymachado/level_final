using UnityEngine;
using System.Collections;
using System.Linq;


public class InteractableItemBehaviour : MonoBehaviour
{

    private bool _isActive = false;
    private bool _isHighlighted = false;
    protected bool _isLocked = false;
    private float _elapsed = 0f;
    private bool toUp = true;
    [SerializeField] private Animator _animator;

    void OnTriggerEnter(Collider other)
    {
        if (_isActive)
        {
            ExecuteAction(other);
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (_isActive)
        {
            ExecuteAction(other);
        }
    }

    protected void Shine()
    {

        if (toUp)
        {
            _elapsed += Time.fixedDeltaTime * 0.1f;
        } else
        {
            _elapsed -= Time.fixedDeltaTime * 0.1f;
        }

        if (_elapsed > 0.3)
        {
            toUp = false;
        } else if (_elapsed < 0)
        {
            toUp = true;
        }
            if (!_isLocked)
             {
                 _isHighlighted = !_isHighlighted;
                 UpdateOutline(_isHighlighted, _elapsed);
             }
    }

    protected void UpdateOutline(bool emission, float value)
    {
        foreach (MeshRenderer render in GetComponentsInChildren<MeshRenderer>())
        {
            Color baseColor = Color.white; //Replace this with whatever you want for your base color at emission level '1'
            Color finalColor = baseColor * value;
            render.material.SetColor("_EmissionColor", finalColor);

        }
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
        if (isActive)
        {
            StartCoroutine(WaitToInactivate());
        }

    }

    IEnumerator WaitToInactivate()
    {
        yield return new WaitForSeconds(10);
        _isActive = false;
    }

    protected virtual void ExecuteAction(Collider other)
    {

    }

}
