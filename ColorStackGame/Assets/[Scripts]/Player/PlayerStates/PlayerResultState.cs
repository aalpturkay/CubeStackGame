using System.Collections;
using BaseState;
using Managers;
using UnityEngine;

namespace Player.PlayerStates
{
    public class PlayerResultState : IState
    {
        private PlayerSM.PlayerSM _playerSM;

        public PlayerResultState(PlayerSM.PlayerSM playerSM)
        {
            _playerSM = playerSM;
        }

        public void Enter()
        {
            _playerSM.BonusText.SetText($"X{_playerSM.BonusMngr.BonusTextString}");
            _playerSM.BonusText.gameObject.SetActive(true);
            _playerSM.StartCoroutine(IncreaseStackCounterTextIE());
        }

        private IEnumerator IncreaseStackCounterTextIE()
        {
            var currentVal = int.Parse(_playerSM.StackCounterText.text);
            var targetVal = _playerSM.BonusMngr.BonusVal * currentVal;

            for (int i = currentVal; i <= targetVal; i++)
            {
                yield return new WaitForSeconds(.03f);
                _playerSM.StackCounterText.SetText(i.ToString());
            }

            _playerSM.BonusText.gameObject.SetActive(false);
            ActivatePopAndButton();
        }

        private void ActivatePopAndButton()
        {
            _playerSM.FinishPopUpText.SetText(_playerSM.StackCounterText.text);
            _playerSM.FinishPopUpImage.gameObject.SetActive(true);
            _playerSM.NextButton.gameObject.SetActive(true);
            _playerSM.NextButton.onClick.AddListener(OnNextButtonClicked);
        }

        private void OnNextButtonClicked()
        {
            Debug.Log("next");
            LevelManager.Instance.LoadNextLevel();
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
        }
    }
}