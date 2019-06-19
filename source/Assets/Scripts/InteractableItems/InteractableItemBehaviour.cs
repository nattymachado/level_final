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
    protected bool executeWhenActivate = false;
    [SerializeField] public Transform pointOnNavMesh;
    [SerializeField] public GridBehaviour grid;
    [SerializeField] public bool executeRotation = true;
    [SerializeField] private MovementController _movementController; 

    void OnTriggerEnter(Collider other)
    {
        if (_isActive)
        {
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            if (character)
            {
                
                CheckIfCanExecuteAction(character);
            }
            

        }

    }

    void OnTriggerStay(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
        if (character)
        {
            if (_isActive)
            {
               /* if (pointOnNavMesh != null && grid != null)
                {
                    Debug.Log("Position fixed:" + pointOnNavMesh.position);
                    _movementController.MoveToPosition(grid, pointOnNavMesh.position);
                }*/
                CheckIfCanExecuteAction(character);
            }
        }
    }

    private void SetRotation(CharacterBehaviour character)
    {
        character.SetRotation(transform);
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
        Debug.Log("Ativado");
        if (executeWhenActivate)
        {
            ExecuteAction();
            return;
        }
        _isActive = isActive;
        if (isActive)
        {
            StartCoroutine(WaitToInactivate());
        }
    }

    public void ActivateAndGo(bool isActive, CharacterBehaviour character)
    {
        Debug.Log("Ativado");
        if (executeWhenActivate)
        {
            ExecuteAction();
            return;
        }
        _isActive = isActive;
        if (isActive)
        {
            
            if (pointOnNavMesh != null && grid != null)
            {
                _movementController.MoveToPosition(grid, pointOnNavMesh.position);
            }
            StartCoroutine(WaitToInactivate());
        }
    }

    protected void Activate(){
        _isActive = true;
        if (executeWhenActivate)
        {
            ExecuteAction();
            return;
        }
    }


    IEnumerator WaitToInactivate()
    {
        yield return new WaitForSeconds(10);
        _isActive = false;
    }

    protected void CheckIfCanExecuteAction(CharacterBehaviour character)
    {

        if (character.targetToRotation == null && character.IsStoped())
        {
            if (executeRotation)
            {
                SetRotation(character);
            }
            ExecuteAction(character);
        }
    }

    protected virtual void ExecuteAction(CharacterBehaviour character)
    {

    }


    protected virtual void ExecuteAction()
    {

    }

}
