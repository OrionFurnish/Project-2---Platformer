using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    static int coins, time, score;
    public int startTime;
    [HideInInspector] public GameObject player;
    public LevelParserStarter levelParser;
    public UIManager uiManager;

    static GameManager instance;

    private void Start() {
        instance = this;
        StartLevel();
    }

    void StartLevel() {
        StopAllCoroutines();
        time = startTime;
        StartCoroutine(GameClock());
    }

    IEnumerator GameClock() {
        uiManager.SetTime(time);
        while (time > 0) {
            yield return new WaitForSeconds(1);
            time--;
            uiManager.SetTime(time);
        }
        // Time Out
        EndGame("Game Over!");
    }

    public static void AddCoin() {
        coins++;
        AddScore(100);
        instance.uiManager.SetCoins(coins);
    }

    public static void AddScore(int addedScore) {
        score += addedScore;
        instance.uiManager.SetScore(score);
    }

    public static void EndGame(string endText) {
        instance.uiManager.SetEnd(endText);
        Destroy(instance.player);
        instance.StopAllCoroutines();
    }

    public void Restart() {
        levelParser.RefreshParse();
        StartLevel();
        uiManager.StartCoroutine(uiManager.FadeIn());
    }
}
