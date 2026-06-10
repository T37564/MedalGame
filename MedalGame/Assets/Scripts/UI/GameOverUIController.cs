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
    // カウントダウン時に使用する最大時間
    private readonly int MAX_COUNT_DOWN_TIME = 5;

    // スコアテキストの拡大倍率
    private readonly float SCALE_RATE = 1.2f;

    // 拡大縮小アニメーションの時間
    private readonly float SCALE_DURATION = 0.5f;

    // カウントダウンさせる時間
    private readonly float COUNT_DOWN_TIME = 1.0f;

    // 読み込むシーン名（タイトルシーン）
    private readonly string LOAD_SCENE_NAME = "GameScene";


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
        // scoreTextが存在する場合のみアニメーションを停止する
        if (scoreText != null)
        {
            scoreText.rectTransform.DOKill();
        }
    }

    /// <summary>
    /// ゲームオーバーUIを表示し、スコアテキストのアニメーションを開始する
    /// </summary>
    public void ChangeGameOverUIState(bool isDisplay)
    {
        // スコアテキストの拡大縮小アニメーションを開始する
        scoreText.rectTransform.DOScale(SCALE_RATE, SCALE_DURATION).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);

        gameOverCanvas.gameObject.SetActive(isDisplay);

        // UIを5秒間表示させ、シーン遷移
        StartCoroutine(CountDownUI());
    }

    /// <summary>
    /// カウントダウンを行い、終了後にゲームをリロードする
    /// </summary>
    private IEnumerator CountDownUI()
    {
        // // 5から1までカウントダウンを行う
        for (int i = MAX_COUNT_DOWN_TIME; 0 < i; i--)
        {
            countDownText.text = i.ToString();

            // fillAmountの値をリセットする
            countDownImage.fillAmount = 1.0f;

            float timer = 0f;

            // 円形ゲージを徐々に減少させる
            while (timer < COUNT_DOWN_TIME)
            {
                // ゲーム内の時間を止めているのでunscaledDeltaTime使用
                timer += Time.unscaledDeltaTime;

                countDownImage.fillAmount = 1.0f - timer;

                yield return null;
            }
        }

        countDownText.text = "0";

        // ゲーム時間を元に戻す
        Time.timeScale = 1;

        // ゲームシーンを再読み込みする
        SceneManager.LoadScene(LOAD_SCENE_NAME);
    }
}