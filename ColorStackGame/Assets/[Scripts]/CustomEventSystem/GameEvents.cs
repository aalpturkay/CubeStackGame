using System;
using UnityEngine;

namespace CustomEventSystem
{
    public sealed class GameEvents : MonoBehaviour
    {
        #region Singleton

        private static GameEvents _instance;

        public static GameEvents Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }

            _instance = this;
        }

        #endregion


        public event Action<GameObject> PlayerTriggerEnter;


        public void OnPlayerTriggerEnter(GameObject other)
        {
            PlayerTriggerEnter?.Invoke(other);
        }
    }
}