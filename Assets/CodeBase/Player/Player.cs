using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase {
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public List<FrogBehavior> frogs; 
        public Transform donutPlatform; 
        public FryPlate donutAnimator; 
        public CoffeeMachine coffeeAnimator; 
        public Transform cocktailPlatform; 
        public Image progressBar; 
        public Action orderDone;
        public LootCounter lootCounter;
        public Sprite[] sprites;
        public Image orderPlace;
        
        
        private FrogBehavior currentFrog;
        private string currentOrder;
        private bool isPreparingOrder = false;
        private Animator animator;

        private void Start() {
            animator = GetComponent<Animator>();
            animator.SetTrigger("Idle");
            Invoke(nameof(StartRoutine), 3f);
            sprites = Resources.LoadAll<Sprite>("RecipeIcon");
            orderPlace.color = Color.clear;
        
        }

        private void StartRoutine() {
            StartCoroutine(OrderRoutine() );

        }
        private IEnumerator OrderRoutine() {
            while (true) {
                if (!isPreparingOrder) {
                    List<FrogBehavior> currentFrogs = new List<FrogBehavior>(frogs);
                    foreach (var frog in currentFrogs) {
                        if (!frog.hasOrder) {
                            currentFrog = frog;
                            yield return StartCoroutine(MoveToTarget(currentFrog.transform));
                            StartCoroutine(UpdateProgressBar(1));
                            yield return new WaitForSeconds(1f);
                            currentFrog.GenerateOrder();

                            if (currentFrog.hasOrder) {
                                currentOrder = currentFrog.currentOrder;
                                currentFrog.hasOrder = false;
                                isPreparingOrder = true;
                            
                                yield return StartCoroutine(PrepareOrder());
                            }

                            //возвращаем заказ
                            yield return StartCoroutine(MoveToTarget(currentFrog.transform));
                            frogs.Remove(currentFrog);
                            currentFrog.OrderDone();
                            orderPlace.color = Color.clear;

                            lootCounter.AddCoins(100);
                            orderDone?.Invoke();
                        }
                    }
                }
                yield return null;

            }
        }

   
        private IEnumerator UpdateProgressBar(float duration) {
            float elapsedTime = 0f;
            progressBar.fillAmount = 0f;

            while (elapsedTime < duration) {
                elapsedTime += Time.deltaTime;
                progressBar.fillAmount = Mathf.Clamp01(elapsedTime / duration);
                yield return null;
            }

            progressBar.fillAmount = 1f; // Полностью заполняем
            progressBar.fillAmount = 0; 
        }

        private IEnumerator PrepareOrder() {
            Transform targetPlatform = currentOrder == "Пончики" ? donutPlatform : cocktailPlatform;
            float preparationTime = currentOrder == "Пончики" ? 1f : 2f;
            yield return StartCoroutine(MoveToTarget(targetPlatform));
            StartCoroutine(UpdateProgressBar(preparationTime));
            CookingOrderAnimation();
            Debug.Log("Начало ожидания");
            yield return new WaitForSeconds(preparationTime);
            Debug.Log("Конец ожидания");
            donutAnimator.StopFrying();
            coffeeAnimator.StopCoffeeMaking();
            orderPlace.sprite = currentOrder == "Пончики" ? sprites[1] : sprites[0];
            orderPlace.color = Color.white; 

           
            isPreparingOrder = false;
            animator.SetTrigger("Idle");
        
        }

        private void CookingOrderAnimation() {
            if (currentOrder == "Пончики") {
                animator.SetTrigger("CookingLeft");
                donutAnimator.StartFrying();
         
            } else {
                animator.SetTrigger("CookingRight");
                coffeeAnimator.StartCoffeeMaking();
              
            }
        }

        private IEnumerator MoveToTarget(Transform target) {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = target.position;
            float journeyLength = Vector3.Distance(startPosition, endPosition);
            float startTime = Time.time;

            while (Vector3.Distance(transform.position, endPosition) > 0.1f) {
                animator.SetTrigger("Run");
                float distanceCovered = (Time.time - startTime) * moveSpeed;
                float fractionOfJourney = distanceCovered / journeyLength;
                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                yield return null;
            }

            transform.position = endPosition;
        }
    }
}
