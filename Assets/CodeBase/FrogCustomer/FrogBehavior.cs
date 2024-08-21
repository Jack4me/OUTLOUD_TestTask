using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase {
    public class FrogBehavior : MonoBehaviour {
        private Transform _currentTarget;
        private FrogTrigger _frogTrigger;
        private BezierCurve _bezierCurve;
        private OrderManager _orderManager;
        private Image _image;
        public float moveSpeed = 5f;
        public Sprite[] sprites;
        public bool hasOrder = false;
        public string currentOrder;
        
        private void Awake() {
             _image = GetComponentInChildren<Image>();
             
              sprites = Resources.LoadAll<Sprite>("RecipeIcon");
              _image.sprite = null;
              _image.color = Color.clear; 
        }

        public void Initialize(FrogTrigger trigger) {
            _frogTrigger = trigger;
        }

        private void Start() {
            
            _bezierCurve = GetComponent<BezierCurve>();
            _orderManager = FindObjectOfType<OrderManager>();
        }

        public void GenerateOrder() {
            if (_orderManager != null) {
                currentOrder = _orderManager.GetRandomRecipe();
                if (currentOrder == "Пончики") {
                    _image.sprite = sprites[1];
                    _image.color = Color.white; 
                }  
                if (currentOrder == "Коктейль") {
                    _image.sprite = sprites[0];
                    _image.color = Color.white; 
                } 
                hasOrder = true;
            }
        }

        public void StartMove(Transform target) {
            _currentTarget = target;
            StartCoroutine(MoveToTarget(target));
        }

        private IEnumerator MoveToTarget(Transform target) {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = target.position;

            float journeyLength = Vector3.Distance(startPosition, endPosition);
            float startTime = Time.time;

            while (Vector3.Distance(transform.position, endPosition) > 0.1f) {
                float distanceCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distanceCovered / journeyLength;

                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

                yield return null;
            }


            transform.position = endPosition;
           
        }
        private Transform targetPosition;
        public void OrderDone() {
            
            _image.color = Color.clear;
            LeaveSellPlace();

        }

        private void LeaveSellPlace() {
            
            targetPosition = _bezierCurve.point3.transform;

            _frogTrigger.FreeTransform(_currentTarget.GetComponent<TargetTransform>());
            StartCoroutine(MoveToTarget(targetPosition));
        }


        
        
    }
}