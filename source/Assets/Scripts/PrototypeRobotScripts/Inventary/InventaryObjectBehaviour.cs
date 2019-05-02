using UnityEngine;
using UnityEngine.UI;

namespace prototypeRobot
{
    public class InventaryObjectBehaviour : MonoBehaviour
    {

        [SerializeField] public string Name;
        [SerializeField] public Image objectImage;
        [SerializeField] public Image objectImageCentrer;
        [SerializeField] public InventaryCenterBehaviour inventaryCenter;
        [SerializeField] public int Position;
        [SerializeField] public AudioClip _audioClip;

        void OnTriggerEnter(Collider other)
        {
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            if (character != null)
            {
                IncludeItemOnInventary();
                PlayClipAtPosition();
                gameObject.SetActive(false);
            }
        }

        private void PlayClipAtPosition()
        {
            if (_audioClip != null) AudioSource.PlayClipAtPoint(_audioClip, this.transform.position);
        }

        private void IncludeItemOnInventary()
        {
            inventaryCenter.AddNewItem(this);
        }
    }
}
