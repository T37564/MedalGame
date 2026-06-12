using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カウントダウンUIを管理するクラス
/// </summary>
public class CountDownUIController : MonoBehaviour
{
    // カウントダウン時に使用する最大時間
    private readonly int MAX_COUNT_DOWN_TIME = 5;

    // カウントダウンさせる時間
    private readonly float COUNT_DOWN_TIME = 1.0f;

    /// <summary>
    /// カウントダウンが終了したことを知らせるフラグ
    /// </summary>
    public bool IsCountDownFinished { get; private set; }


    /// <summary>
    /// カウントダウンUIを更新する
    /// </summary>
    public IEnumerator StartCountDown(TMP_Text countText, Image fillAmountGauge)
    {
        // 5から1までカウントダウンを行う
        for (int i = MAX_COUNT_DOWN_TIME; 0 < i; i--)
        {
            countText.text = i.ToString();

            // fillAmountの値をリセットする
            fillAmountGauge.fillAmount = 1.0f;

            float countDownTimer = 0.0f;

            // 円形ゲージを徐々に減少させる
            while (countDownTimer < COUNT_DOWN_TIME)
            {
                // ゲーム内の時間を止めているのでunscaledDeltaTime使用
                countDownTimer += Time.unscaledDeltaTime;

                fillAmountGauge.fillAmount = 1.0f - countDownTimer;

                yield return null;
            }
        }

        countText.text = "0";

        // カウントダウンが終了したことを知らせる
        IsCountDownFinished = true;
    }

    /// <summary>
    /// カウントダウン状態をリセットする
    /// </summary>
    public void ResetCountDown()
    {
        IsCountDownFinished = false;
    }
}
