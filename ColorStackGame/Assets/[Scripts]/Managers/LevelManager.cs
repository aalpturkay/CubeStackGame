using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private List<Level.Level> levels;
        [SerializeField] private Transform parentLevels;
        private Level.Level _currentLevel;
        private Level.Level _nextLevel;
        private Level.Level _loadedLevel;

        public Level.Level CurrentLevel => _currentLevel;
        public int CurrentLevelVal => levels.IndexOf(_currentLevel) + 1;

        #region Singleton

        private static LevelManager _instance;
        public static LevelManager Instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
            }

            else
            {
                _instance = this;
            }
        }

        #endregion


        private void Start()
        {
            if (_currentLevel == null)
            {
                _currentLevel = levels[0];
                _loadedLevel = LoadLevel(_currentLevel);
                print(levels.IndexOf(_currentLevel));
            }
        }

        private Level.Level LoadLevel(Level.Level level)
        {
            Level.Level loadedLevel;
            if (level != null)
            {
                loadedLevel = Instantiate(level, parent: parentLevels);
                return loadedLevel;
            }

            return null;
        }

        private void DestroyLevel(Level.Level level)
        {
            Destroy(level.gameObject);
        }

        public void LoadNextLevel()
        {
            var currentLevelIndex = levels.IndexOf(_currentLevel);
            print(currentLevelIndex);
            if (_currentLevel != null && currentLevelIndex + 1 != levels.Count)
            {
                DestroyLevel(_loadedLevel);
                _currentLevel = levels[currentLevelIndex + 1];
                _loadedLevel = LoadLevel(levels[currentLevelIndex + 1]);
            }
        }

        public void ReloadLevel()
        {
            if (_currentLevel != null && _loadedLevel != null)
            {
                DestroyLevel(_loadedLevel);
                _loadedLevel = LoadLevel(_currentLevel);
            }
        }
    }
}