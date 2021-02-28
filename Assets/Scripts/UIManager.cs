using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public TextMeshProUGUI scoreText, coinText, timeText, endText, restartButtonText;
    public Image blackScreen, restartButtonImage;
    public float fadeTime;

    private void Start() {
        blackScreen.gameObject.SetActive(false);
        blackScreen.color = new Color(0f, 0f, 0f, 0f);
    }

    public void SetScore(int score) {
        string text = "MARIO\n";
        string scoreStr = score.ToString();
        int lengthDif = 6-scoreStr.Length;
        for(int i = 0; i < lengthDif; i++) { text += "0"; }
        text += scoreStr;
        scoreText.text = text;
    }

    public void SetCoins(int coins) {
        string text = "x";
        string coinStr = coins.ToString();
        if(coinStr.Length == 1) { text += "0"; }
        text += coinStr;
        coinText.text = text;
    }

    public void SetTime(int time) {
        timeText.text = "Time\n" + time.ToString();
    }

    public void SetEnd(string endText) {
        this.endText.text = endText;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut() {
        blackScreen.gameObject.SetActive(true);
        float startTime = Time.time;
        float completeness = 0;
        while(completeness < 1) {
            float elapsedTime = Time.time-startTime;
            completeness = elapsedTime / fadeTime;
            blackScreen.color = SetColorAlpha(blackScreen.color, completeness);
            endText.color = SetColorAlpha(endText.color, completeness);
            restartButtonImage.color = SetColorAlpha(restartButtonImage.color, completeness);
            restartButtonText.color = SetColorAlpha(restartButtonText.color, completeness);
            yield return null;
        }
    }

    public IEnumerator FadeIn() {
        float startTime = Time.time;
        float completeness = 0;
        while (completeness < 1) {
            float elapsedTime = Time.time - startTime;
            completeness = elapsedTime / fadeTime;
            blackScreen.color = SetColorAlpha(blackScreen.color, 1f-completeness);
            endText.color = SetColorAlpha(endText.color, 1f-completeness);
            restartButtonImage.color = SetColorAlpha(restartButtonImage.color, 1f-completeness);
            restartButtonText.color = SetColorAlpha(restartButtonText.color, 1f-completeness);
            yield return null;
        }
        blackScreen.gameObject.SetActive(false);
    }

    private Color SetColorAlpha(Color color, float newAlpha) {
        return new Color(color.r, color.g, color.b, newAlpha);
    }
}
