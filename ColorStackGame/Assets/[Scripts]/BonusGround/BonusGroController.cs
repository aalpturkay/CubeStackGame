using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Player.PlayerSM;
using TMPro;
using UnityEngine;

namespace BonusGround
{
    public class BonusGroController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro upText;
        [SerializeField] private TextMeshPro downText;
        [SerializeField] private Material activeMaterial;
        [SerializeField] private Material passiveMaterial;
        [SerializeField] private float bonus;
        [SerializeField] private string bonusText;
        [SerializeField] private CinemachineVirtualCamera virtualCam;
        [SerializeField] private Animator cameraAnim;
        [SerializeField] private PlayerSM playerSM;
        private int _collisionCount = 0;
        [SerializeField] private List<ParticleSystem> confetties;
        [SerializeField] private ParticleSystem bubbleParticle;
        private BonusManager bonusManager;


        private void Start()
        {
            bonusManager = transform.parent.parent.GetComponent<BonusManager>();
            InitializeTexts(bonusText);
        }

        private void InitializeTexts(string txt)
        {
            upText.SetText($"x{txt}");
            downText.SetText($"x{txt}");
        }

        private void Update()
        {
            //StartCoroutine(LastRout());
        }

        private void OnCollisionEnter(Collision other)
        {
            _collisionCount += 1;
            ChangeColor(activeMaterial);
            SwitchText();
            //follow this bonusground.
            if (_collisionCount == 1)
            {
                bonusManager.BonusAreas.Add(transform);
                SwitchCamera();
                PlayConfetties(confetties);
                StartCoroutine(FinalRoutineIE());
            }
        }

        IEnumerator FinalRoutineIE()
        {
            yield return new WaitForSeconds(3f);
            if (IsLast())
            {
                StartCoroutine(ChangeColorIE(activeMaterial, passiveMaterial));
                bubbleParticle.Play();
                bonusManager.BonusTextString = bonusText;
                bonusManager.BonusVal = bonus;
                playerSM.ChangeState(playerSM.ResultState);
            }
        }

        IEnumerator ChangeColorIE(Material activeMat, Material passiveMat)
        {
            for (int i = 0; i < 5; i++)
            {
                ChangeColor(passiveMat);
                yield return new WaitForSeconds(.1f);
                ChangeColor(activeMat);
                yield return new WaitForSeconds(.1f);
            }
        }

        private bool IsLast()
        {
            return bonusManager.BonusAreas[bonusManager.BonusAreas.Count - 1] == transform;
        }

        private void SwitchCamera()
        {
            virtualCam.Follow = transform;
            cameraAnim.Play("ResultCam");
        }

        private void SwitchText()
        {
            upText.gameObject.SetActive(false);
            downText.gameObject.SetActive(true);
        }

        private void PlayConfetties(List<ParticleSystem> confettiParticles)
        {
            foreach (var confettiParticle in confettiParticles)
            {
                confettiParticle.Play();
            }
        }

        private void ChangeColor(Material material)
        {
            transform.GetComponent<Renderer>().material = material;
        }
    }
}