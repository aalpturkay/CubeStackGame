using System;
using System.Collections;
using System.Collections.Generic;
using Collectable;
using ColorSpace;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private Transform bag;

        [SerializeField] private Player player;
        [SerializeField] private TextMeshProUGUI oneTextPref;
        [SerializeField] private Canvas canvas;
        [SerializeField] private TextMeshProUGUI stackCounterText;

        [SerializeField] private Material playerRedMat;
        [SerializeField] private Material playerGreenMat;
        [SerializeField] private Material playerBlueMat;


        private Transform _stackedCube;
        private HingeJoint _oldCubeJoint;

        private float _speedAmount = .2f;
        private int _stackCount = 0;

        private GameObject _cubeIndicator;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                //StackCube(other.gameObject);
                FillBag(other.gameObject);
            }

            if (other.CompareTag("ColorChanger"))
            {
                ChangePlayerColor(other.gameObject);
            }
        }

        private void FillBag(GameObject other)
        {
            var cubeIndicator = other.transform.GetChild(0).gameObject;
            other.GetComponent<Collider>().enabled = false;
            if (other.GetComponent<Collectable.Collectable>().CollectableColor == player.PlayerColorType)
            {
                StartCoroutine(ShowIndicatorIE(cubeIndicator));
                IncreaseStackCounter(1);
                InstantiateText();
                player.AddPlayerSpeed(_speedAmount);
                if (player.BagList.Count == 0)
                {
                    _stackedCube = other.transform;
                    player.BagList.Add(other);
                    _stackedCube.position = bag.position;
                    _stackedCube.rotation = bag.rotation;
                    _stackedCube.SetParent(bag);
                }
                else
                {
                    for (var i = 0; i < player.BagList.Count; i++)
                    {
                        if (player.BagList[i] != null)
                            player.BagList[i].transform.position += Vector3.up * 1.1f;
                    }

                    other.transform.position = bag.position;
                    other.transform.rotation = bag.rotation;
                    other.transform.SetParent(bag);
                    player.BagList.Add(other);
                }
            }
            else
            {
                Destroy(other);
                if (player.BagList.Count > 0 || bag.childCount > 0)
                {
                    player.AddPlayerSpeed(-_speedAmount);
                    player.BagList.Remove(player.BagList[player.BagList.Count - 1]);

                    Destroy(bag.GetChild(bag.childCount - 1).gameObject);
                    for (int i = 0; i < player.BagList.Count; i++)
                    {
                        if (player.BagList[i] != null)
                        {
                            player.BagList[i].transform.position -= Vector3.up * 1;
                        }
                    }
                }
            }
        }

        

        void SetJoints()
        {
            if (player.BagList.Count <= 3) return;
            for (int i = player.BagList.Count - 1; i < 0; i--)
            {
                var pre = player.BagList[i - 1];
                player.BagList[i].GetComponent<FixedJoint>().connectedBody = pre.GetComponent<Rigidbody>();
            }
        }

        private void ChangePlayerColor(GameObject other)
        {
            var colorChanger = other.GetComponent<ColorChanger.ColorChanger>().ChangerColorType;
            switch (colorChanger)
            {
                case ColorType.Colors.Red:
                    SetPlayerColor(playerRedMat);
                    player.PlayerColorType = ColorType.Colors.Red;
                    break;
                case ColorType.Colors.Green:
                    SetPlayerColor(playerGreenMat);
                    player.PlayerColorType = ColorType.Colors.Green;
                    break;
                case ColorType.Colors.Blue:
                    SetPlayerColor(playerBlueMat);
                    player.PlayerColorType = ColorType.Colors.Blue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetPlayerColor(Material material)
        {
            player.PlayerRenderer.material = material;
        }


        IEnumerator ShowIndicatorIE(GameObject indicator)
        {
            indicator.SetActive(true);
            indicator.transform.DOScale(new Vector3(1.7f, 1.7f, 1), .8f);
            indicator.transform.GetComponent<Renderer>().material.DOFade(0f, 1f);
            //indicator.GetComponent<Material>().color = Color.blue;
            yield return new WaitForSeconds(1f);
            if (indicator == null) yield break;
            indicator.SetActive(false);
        }

        private void IncreaseStackCounter(int val)
        {
            _stackCount += val;
            stackCounterText.SetText($"{_stackCount}");
        }

        private void InstantiateText()
        {
            Instantiate(oneTextPref, canvas.transform);
        }


        private void ConnectJoints(Joint joint, Rigidbody rb)
        {
            joint.connectedBody = rb;
        }
    }
}