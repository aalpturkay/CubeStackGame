using CustomEventSystem;
using UnityEngine;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            GameEvents.Instance.OnPlayerTriggerEnter(other.gameObject);
        }
    }
}