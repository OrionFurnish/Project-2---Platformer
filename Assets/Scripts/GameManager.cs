using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    static int coins, time, score;

    private void Start() {
        StartLevel();
    }

    void StartLevel() {
        StopAllCoroutines();
        time = 400;
        score = 0;
        coins = 0;
        StartCoroutine(GameClock());
    }

    IEnumerator GameClock() {
        UIManager.SetTime(time);
        while (time > 0) {
            yield return new WaitForSeconds(1);
            time--;
            UIManager.SetTime(time);
        }
        // Time Out
        Debug.Log("Game Over");
    }

    public static void AddCoin() {
        coins++;
        UIManager.SetCoins(coins);
    }

    public static void AddScore(int addedScore) {
        score += addedScore;
        UIManager.SetScore(score);
    }
}
