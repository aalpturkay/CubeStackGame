using System.Collections;
using System.Collections.Generic;
using BaseState;
using DG.Tweening;
using UnityEngine;

namespace Player.PlayerStates
{
    public class PlayerFinishState : IState
    {
        private PlayerSM.PlayerSM _playerSM;
        private List<GameObject> _cubeList = new List<GameObject>();

        public PlayerFinishState(PlayerSM.PlayerSM playerSM)
        {
            _playerSM = playerSM;
        }

        public void Enter()
        {
            Debug.Log("Finish State");

            TapUISetActive(true);
            _playerSM.TapBar.SetMaxVal(100);
            _playerSM.StartCoroutine(DecreaseTapBarIE(10, delay: .5f));
            _playerSM.StartCoroutine(MoveCubesToStackPointIE());
            _playerSM.StartCoroutine(KickRoutineIE());
        }


        public void Tick()
        {
            MoveToEndLine();
            if (Input.GetMouseButtonDown(0))
            {
                _playerSM.TapBar.SetVal(_playerSM.TapBar.BarVal + 15);
            }
        }

        private void TapUISetActive(bool state)
        {
            _playerSM.TapBar.gameObject.SetActive(state);
            _playerSM.TapText.gameObject.SetActive(state);
        }

        private void ResetRot()
        {
            _playerSM.PlayerTransform.DORotate(Vector3.zero, .3f);
        }

        private void MoveToEndLine()
        {
            var playerPos = _playerSM.PlayerTransform.position;
            var targetRot = _playerSM.StackPointTransform.rotation;
            var targetPos = _playerSM.StackPointTransform.position;
            var offset = Vector3.forward * 2;
            playerPos = Vector3.MoveTowards(playerPos, targetPos - offset,
                Time.deltaTime * _playerSM.Player.PlayerSpeed);
            playerPos.y = 0;
            _playerSM.PlayerTransform.position = playerPos;
            _playerSM.PlayerTransform.DORotate(new Vector3(targetRot.x, targetRot.y, targetRot.z), .3f);
        }

        IEnumerator DecreaseTapBarIE(int val, float delay)
        {
            while (_playerSM.TapBar.BarVal >= 0)
            {
                var tapBarVal = _playerSM.TapBar.BarVal;
                tapBarVal -= tapBarVal == 0 ? 0 : val;
                _playerSM.TapBar.SetVal(tapBarVal);
                yield return new WaitForSeconds(delay);
            }
        }

        private void SwitchCloseCam()
        {
            _playerSM.CameraAnim.Play("PlayerCloseCam");
        }

        private IEnumerator KickRoutineIE()
        {
            yield return new WaitUntil(() => IsKickable());
            TapUISetActive(false);
            SwitchCloseCam();
            _playerSM.PlayerAnim.SetTrigger("mmaKickTrig");
        }

        private bool IsKickable()
        {
            var playerPos = _playerSM.PlayerTransform.position;
            var stackPointPos = _playerSM.StackPointTransform.position;
            const int offset = 10;
            return playerPos.z >= stackPointPos.z - offset;
        }

        IEnumerator MoveCubesToStackPointIE()
        {
            var cubeScale = 0;

            foreach (var cube in _playerSM.Player.BagList)
            {
                if (cube != null)
                {
                    cube.transform.parent = _playerSM.FreeCubesTransform;
                    cube.transform.DOMove(new Vector3(_playerSM.StackPointTransform.position.x,
                        _playerSM.StackPointTransform.position.y + cubeScale,
                        _playerSM.StackPointTransform.position.z), .01f);
                    cubeScale++;
                }

                yield return new WaitForSeconds(.01f);
            }

            _playerSM.PlayerAnim.SetTrigger("runTrig");
        }


        public void FixedTick()
        {
        }

        public void Exit()
        {
        }
    }
}