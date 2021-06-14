using System;
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

        private Level.Level _savedLevel;
        public Level.Level CurrentLevel => _currentLevel;


        public Level.Level LoadedLevel
        {
            get => _loadedLevel;
            set => _loadedLevel = value;
        }

        public int CurrentLevelVal
        {
            get => levels.IndexOf(_currentLevel) + 1;
            set => CurrentLevelVal = value;
        }

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
            Application.targetFrameRate = 60;

            if (SaveManager.Instance.Level > levels.Count)
            {
                print(SaveManager.Instance.Level % levels.Count);
                _currentLevel = levels[(SaveManager.Instance.Level % levels.Count) - 1];
                _loadedLevel = LoadLevel(_currentLevel);
                print(levels.IndexOf(_currentLevel));
                return;
            }


            _currentLevel = levels[SaveManager.Instance.Level - 1];


            _loadedLevel = LoadLevel(_currentLevel);
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
            SaveManager.Instance.Level += 1;

            var currentLevelIndex = levels.IndexOf(_currentLevel);
            print(currentLevelIndex);
            if (_currentLevel != null && currentLevelIndex + 1 != levels.Count)
            {
                DestroyLevel(_loadedLevel);
                _currentLevel = levels[currentLevelIndex + 1];
                _loadedLevel = LoadLevel(levels[currentLevelIndex + 1]);
            }
            else
            {
                var currentLevelVal = CurrentLevelVal;
                DestroyLevel(_loadedLevel);
                _currentLevel = levels[0];
                _loadedLevel = LoadLevel(_currentLevel);
            }

            PlayerPrefs.SetInt("level", SaveManager.Instance.Level);
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