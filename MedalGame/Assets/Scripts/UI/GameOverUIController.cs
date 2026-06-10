using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ゲームオーバーUIの管理クラス
/// </summary>
public class GameOverUIController : MonoBehaviour
{
    // スコアテキストの拡大倍率
    private const float SCALE_RATE = 1.2f;

    // 拡大縮小アニメーションの時間
    private const float SCALE_DURATION = 0.5f;

    // FillAmountの最大値
    private const float MAX_FILL_AMOUNT = 1.0f;

    // カウントダウンさせる時間
    private const float COUNT_DOWN_TIME = 1.0f;

    // カウントダウン時に使用する最大時間
    private const int MAX_COUNT_DOWN_TIME = 5;



    [Header("ゲットしたメダルの枚数を入れているText")]
    [SerializeField] private TMP_Text scoreText = null;

    [Header("ゲームオーバー時のUI")]
    [SerializeField] private Canvas gameOverCanvas = null;

    [Header("カウントダウン時に使用するText")]
    [SerializeField] private TMP_Text countDownText = null;

    [Header("カウントダウン時に使用するImage")]
    [SerializeField] private Image countDownImage = null;

    /// <summary>
    /// スコアテキストのアニメーションを停止する
    /// </summary>
    private void OnDisable()
    {
        if (scoreText != null)
            scoreText.rectTransform.DOKill();
    }

    /// <summary>
    /// ゲームオーバーUIを表示し、スコアテキストのアニメーションを開始する
    /// </summary>
    public void ChangeGameOverUIState(bool isDisplay)
    {
        // ScoreTextの拡大縮小を繰り返す
        scoreText.rectTransform.DOScale(SCALE_RATE, SCALE_DURATION).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        gameOverCanvas.gameObject.SetActive(isDisplay);

        StartCoroutine(CountDownUI());
    }

    /// <summary>
    /// カウントダウンを行い、終了後にゲームをリロードする
    /// </summary>
    private IEnumerator CountDownUI()
    {
        // カウントダウン開始処理
        for (int i = MAX_COUNT_DOWN_TIME; 0 < i; i--)
        {
            countDownText.text = i.ToString();

            countDownImage.fillAmount = MAX_FILL_AMOUNT;

            float timer = 0f;

            // FillAmountを減らす処理
            while (timer < COUNT_DOWN_TIME)
            {
                // ゲーム内の時間を止めているのでunscaledDeltaTime使用
                timer += Time.unscaledDeltaTime;

                countDownImage.fillAmount = MAX_FILL_AMOUNT - timer;

                yield return null;
            }
        }

        countDownText.text = "0";

        // ゲーム時間を元に戻す
        Time.timeScale = 1;

        // ゲームシーンを再読み込みする
        SceneManager.LoadScene("GameScene");
    }
}