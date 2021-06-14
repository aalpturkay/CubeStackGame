using BaseState;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Player.PlayerStates
{
    public class PlayerRunState : IState
    {
        private PlayerSM.PlayerSM _playerSm;
        private float _startPosX, _endPosX, _mousePosDeltaX;

        public PlayerRunState(PlayerSM.PlayerSM playerSm)
        {
            _playerSm = playerSm;
        }

        public void Enter()
        {
            _playerSm.LevelText.SetText($"{SaveManager.Instance.Level}");
            _playerSm.LevelBar.SetActive(true);
            _playerSm.CameraAnim.Play("PlayerCam");
            _playerSm.PlayerAnim.SetTrigger("boxJoggingTrigger");
            _playerSm.StackCounterText.gameObject.SetActive(true);
        }

        public void Tick()
        {
            MovePlayer();
            ControlIsFinish();
        }

        public void FixedTick()
        {
        }

        public void Exit()
        {
        }

        private void ControlIsFinish()
        {
            if (_playerSm.PlayerTransform.position.z >= _playerSm.FinishLineTransform.position.z)
            {
                _playerSm.ChangeState(_playerSm.FinishState);
            }
        }

        private void MovePlayer()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosX = Input.mousePosition.x;
                _mousePosDeltaX = 0;
            }

            else if (Input.GetMouseButton(0))
            {
                _mousePosDeltaX = Input.mousePosition.x - _startPosX;
                _startPosX = Mathf.Lerp(_startPosX, Input.mousePosition.x, 0.05f);
            }

            else if (Input.GetMouseButtonUp(0))
            {
                _startPosX = 0;
                _mousePosDeltaX = 0;
            }

            var playerPos = _playerSm.PlayerTransform.position;
            var speed = _playerSm.Player.PlayerSpeed;
            playerPos.z = playerPos.z + speed * Time.deltaTime;
            playerPos.x += _mousePosDeltaX * Time.deltaTime * .04f;
            playerPos.x = Mathf.Clamp(playerPos.x, -4f, 4f);
            _playerSm.PlayerTransform.position = playerPos;

            var rot = _mousePosDeltaX;
            rot = Mathf.Clamp(rot, -30, 30);
            _playerSm.PlayerTransform.DORotate(Mathf.Abs(_mousePosDeltaX) <= .2 ? Vector3.zero : new Vector3(0, rot, 0),
                .4f);
        }
    }
}