using System;
using BaseState;
using BonusGround;
using DG.Tweening;
using Player.PlayerStates;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace Player.PlayerSM
{
    public class PlayerSM : StateMachine
    {
        private PlayerIdleState _idleState;
        private PlayerRunState _runState;
        private PlayerFinishState _finishState;
        private PlayerResultState _resultState;

        [SerializeField] private Animator playerAnim;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Player player;
        [SerializeField] private TextMeshProUGUI stackCounterText;
        [SerializeField] private Transform stackPointTransform;
        [SerializeField] private Transform bagTransform;
        [SerializeField] private Animator cameraAnim;
        [SerializeField] private TextMeshProUGUI bonusText;
        [SerializeField] private BonusManager bonusMngr;
        [SerializeField] private Image finishPopUpImage;
        [SerializeField] private TextMeshProUGUI finishPopUpText;
        [SerializeField] private Button nextButton;
        [SerializeField] private ParticleSystem playerHitParticle;
        [SerializeField] private GameObject slideHand;
        [SerializeField] private CustomBar tapBar;
        [SerializeField] private TextMeshProUGUI tapText;
        [SerializeField] private Transform finishLineTransform;
        [SerializeField] private GameObject levelBar;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Transform freeCubesTransform;
        public PlayerIdleState IdleState => _idleState;

        public PlayerRunState RunState => _runState;

        public PlayerFinishState FinishState => _finishState;

        public PlayerResultState ResultState => _resultState;
        public Animator PlayerAnim => playerAnim;

        public Transform PlayerTransform => playerTransform;

        public Player Player => player;

        public TextMeshProUGUI StackCounterText => stackCounterText;

        public Transform StackPointTransform => stackPointTransform;

        public Transform BagTransform => bagTransform;

        public Animator CameraAnim => cameraAnim;

        public TextMeshProUGUI BonusText => bonusText;

        public BonusManager BonusMngr => bonusMngr;

        public Image FinishPopUpImage => finishPopUpImage;

        public TextMeshProUGUI FinishPopUpText => finishPopUpText;

        public Button NextButton => nextButton;

        public GameObject SlideHand => slideHand;

        public CustomBar TapBar => tapBar;

        public TextMeshProUGUI TapText => tapText;

        public Transform FinishLineTransform => finishLineTransform;

        public GameObject LevelBar => levelBar;

        public TextMeshProUGUI LevelText => levelText;

        public Transform FreeCubesTransform => freeCubesTransform;

        private void Awake()
        {
            _idleState = new PlayerIdleState(this);
            _runState = new PlayerRunState(this);
            _finishState = new PlayerFinishState(this);
            _resultState = new PlayerResultState(this);
        }

        private void Start()
        {
            ChangeState(_idleState);
        }

        private void AddForceToCubes(float force)
        {
            for (int i = 0; i < player.BagList.Count; i++)
            {
                player.BagList[i].GetComponent<Collider>().enabled = true;
                player.BagList[i].GetComponent<Rigidbody>().isKinematic = false;
                player.BagList[i].GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
            }
        }

        public void PlayHitParticle()
        {
            var force = 1000f * (tapBar.BarVal / 100);
            playerHitParticle.Play();
            AddForceToCubes(1000f);
        }

        public void SlowMotion()
        {
            Time.timeScale = .5f;
            Time.fixedDeltaTime = .02f * Time.timeScale;
        }

        public void NormalMotion()
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = .02f * Time.timeScale;
        }
    }
}