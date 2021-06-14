using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private Player.Player _player;
        #region Singleton

        private static GameManager _instance;

        public static GameManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        public Player.Player Player => _player;
    }
}