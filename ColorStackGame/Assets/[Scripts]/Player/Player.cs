using System;
using System.Collections;
using System.Collections.Generic;
using ColorSpace;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ColorType.Colors playerColorType;
        [SerializeField] private float playerSpeed;
        [SerializeField] private Renderer playerRenderer;
        [SerializeField] private Transform finishLineTransform;
        [SerializeField] private List<GameObject> bagList;
        [SerializeField] private CustomBar levelBar;

        private float _maxDistance;
        private Queue<GameObject> _bagQueue = new Queue<GameObject>();

        public Queue<GameObject> BagQueue
        {
            get => _bagQueue;
            set => _bagQueue = value;
        }
        public List<GameObject> BagList
        {
            get => bagList;
            set => bagList = value;
        }

        private void Start()
        {
            _maxDistance = Vector3.Distance(transform.position, finishLineTransform.position);
        }

        private void Update()
        {
            SetLevelBar();
        }

        public ColorType.Colors PlayerColorType
        {
            get => playerColorType;
            set => playerColorType = value;
        }

        public Renderer PlayerRenderer
        {
            get => playerRenderer;
            set => playerRenderer = value;
        }

        public float PlayerSpeed => playerSpeed;

        public void AddPlayerSpeed(float val)
        {
            playerSpeed += val;
        }


        private void SetLevelBar()
        {
            if (transform.position.z <= finishLineTransform.position.z)
            {
                var playerPos = transform.position;
                playerPos.x = 0;
                var newDis = Vector3.Distance(playerPos, finishLineTransform.position);
                var barVal = Mathf.InverseLerp(_maxDistance, 0, newDis);
                levelBar.SetVal(barVal);
            }
        }
    }
}