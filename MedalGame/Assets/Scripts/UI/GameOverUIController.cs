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
    private readonly float SCALE_RATE = 1.2f;

    // 拡大縮小アニメーションの時間
    private readonly float SCALE_DURATION = 0.5f;

    // 読み込むシーン名（タイトルシーン）
    private readonly string LOAD_SCENE_NAME = "GameScene";

    [Header("メダルマネージャー参照用")]
    [SerializeField] private MedalManager medalManager = null;

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
    /// ゲームオーバーUIを表示し、リセット処理を開始する
    /// </summary>
    public void ShowGameOverUI()
    {
        // スコアの更新
        scoreText.text = medalManager.CurrentMedalCount.ToString();

        // スコアテキストの拡大縮小アニメーションを開始する
        scoreText.rectTransform.DOScale(SCALE_RATE, SCALE_DURATION).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);

        gameOverCanvas.gameObject.SetActive(true);

        // カウントダウン後のリセット処理を開始する
        StartCoroutine(ResetGame());
    }

    /// <summary>
    /// カウントダウン後にゲームをリセットする
    /// </summary>
    private IEnumerator ResetGame()
    {
        yield return StartCoroutine(UIManager.Instance.CountDownUIController.StartCountDown(countDownText, countDownImage));

        // ゲーム時間を元に戻す
        Time.timeScale = 1;

        // ゲームシーンを再読み込みする
        SceneManager.LoadScene(LOAD_SCENE_NAME);
    }
}