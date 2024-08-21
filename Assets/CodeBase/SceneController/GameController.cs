using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.SceneController {
    public class GameController : MonoBehaviour {

        public Text timerText;
        public GameObject looseGamePanel;
        public GameObject winGamePanel;
        public FrogSpawner frogSpawner;
private int coins = 0;
        private float timer = 60f;
        private bool gameActive = true;
        public LootCounter lootCounter;

        private void Awake() {
            Time.timeScale = 1;
            looseGamePanel.SetActive(false);
            winGamePanel.SetActive(false);


        }

        private void Update() {
            if (gameActive) {
                timer -= Time.deltaTime; 

                if (timer <= 0) {
                    timer = 0;
                    EndGame(false); 
                }

                UpdateTimerUI(); 
            }

            if (lootCounter.Collected >= 1000) {
                EndGame(true);
            }
        }

       

        void EndGame(bool success) {
            gameActive = false;
               Time.timeScale = 0;
            if (!success) {
                
            looseGamePanel.SetActive(true);
            }
            else {
                winGamePanel.SetActive(true);
            }
        }

        public void RestartGame() {
         
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
         
           
           
        }
        private void UpdateTimerUI() {
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }
}