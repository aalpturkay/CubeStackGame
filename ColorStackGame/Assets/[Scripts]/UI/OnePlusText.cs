using Player;
using UnityEngine;

namespace UI
{
    public class OnePlusText : MonoBehaviour
    {
        private PlayerHeadRef _playerHead;

        private Vector3 _offset = Vector3.up * 250;

        private void Awake()
        {
            _playerHead = FindObjectOfType<Player.PlayerHeadRef>();
        }

        private void Start()
        {
            Destroy(this.gameObject, .3f);
        }

        void Update()
        {
            if (_playerHead != null)
            {
                transform.position = Camera.main.WorldToScreenPoint(_playerHead.transform.position);
            }
        }
    }
}