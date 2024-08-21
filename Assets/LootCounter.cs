using CodeBase;
using TMPro;
using UnityEngine;

public class LootCounter : MonoBehaviour {
    public TextMeshProUGUI CounterText;
    public Player player;
    public int Collected;

    private void Start() {
        UpdateCounterText();
        player.orderDone += UpdateCounterText;
    }

    public void AddCoins(int coins) {
        Collected += coins;
    }
    private void OnDestroy() {
        if ( player.orderDone != null) {
            player.orderDone -= UpdateCounterText;
        }
    }
    public void UpdateCounterText() {
       
        CounterText.text = $"{Collected}";
        //  CounterText.text = $"{100}";
    }
}