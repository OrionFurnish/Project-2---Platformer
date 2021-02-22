using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
    public TextMeshProUGUI scoreText, coinText, timeText;

    private static UIManager instance;

    private void Awake() {
        instance = this;
    }

    public static void SetScore(int score) {
        string text = "MARIO\n";
        string scoreStr = score.ToString();
        int lengthDif = 6-scoreStr.Length;
        for(int i = 0; i < lengthDif; i++) { text += "0"; }
        text += scoreStr;
        instance.scoreText.text = text;
    }

    public static void SetCoins(int coins) {
        string text = "x";
        string coinStr = coins.ToString();
        if(coinStr.Length == 1) { text += "0"; }
        text += coinStr;
        instance.coinText.text = text;
    }

    public static void SetTime(int time) {
        instance.timeText.text = "Time\n" + time.ToString();
    }
}
