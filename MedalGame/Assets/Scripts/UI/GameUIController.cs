using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム中のUIの更新や表示切り替えを管理するクラス
/// </summary>
public class GameUIController : MonoBehaviour
{
    // 使用中のメダル枚数表示に使用する先頭文字
    private readonly string ACTIVE_MEDAL_TEXT = "ActiveMedal";


    [Header("メダルゲット枚数表示用Text")]
    [SerializeField] private TMP_Text medalCountText = null;

    [Header("現在の使用メダル枚数表示用Text")]
    [SerializeField] private TMP_Text activeMedalCountText = null;

    [Header("カウントダウン時に使用するText")]
    [SerializeField] private TMP_Text countDownText = null;

    [Header("カウントダウン時に使用するImage")]
    [SerializeField] private Image countDownImage = null;

    [Header("ゲーム中のUI")]
    [SerializeField] private Canvas gameCanvas = null;

    [Header("LaunchCameraUI")]
    [SerializeField] private Canvas launchCameraUI = null;

    [Header("メダルプールマネージャー参照用")]
    [SerializeField] private MedalPoolManager medalPoolManager = null;

    // 実行中のゲームオーバーカウントダウンコルーチン
    private Coroutine gameOverCountDownCoroutine = null;

    /// <summary>
    /// カウントダウンUIを初期状態では非表示にする
    /// </summary>
    private void Start()
    {
        countDownImage.enabled = false;
        countDownText.enabled = false;
    }

    /// <summary>
    /// 現在の使用メダル枚数を表示するUIの更新
    /// </summary>
    private void Update()
    {
        activeMedalCountText.text = $"{ACTIVE_MEDAL_TEXT} {medalPoolManager.ActiveMedalCount}";
    }


    /// <summary>
    /// メダル枚数表示を更新する
    /// </summary>
    public void UpdateMedalCountUI(int currentMedal)
    {
        medalCountText.text = currentMedal.ToString();
    }



    /// <summary>
    /// ゲーム中のUIの表示状態を変更する
    /// </summary>
    public void ChangeGameUIState(bool isDisplay)
    {
        gameCanvas.gameObject.SetActive(isDisplay);
    }

    /// <summary>
    /// ミニカメラのUIの表示状態を変更する
    /// </summary>
    public void ChangeLaunchCameraUIState(bool isDisplay)
    {
        launchCameraUI.gameObject.SetActive(isDisplay);
    }

    /// <summary>
    /// ゲームオーバーカウントダウンを開始する
    /// </summary>
    public void StartGameOverCountDown()
    {
        // 二重起動防止
        if (gameOverCountDownCoroutine != null) return;

        // カウントダウンUIを表示する
        countDownImage.enabled = true;
        countDownText.enabled = true;

        // ゲームオーバーカウントダウンを開始する
        gameOverCountDownCoroutine = StartCoroutine(UIManager.Instance.CountDownUIController.StartCountDown(countDownText, countDownImage));
    }

    /// <summary>
    /// ゲームオーバーカウントダウンを停止する
    /// </summary>
    public void StopGameOverCountDown()
    {
        if (gameOverCountDownCoroutine == null) return;
        // カウントダウンUIを非表示にする
        countDownImage.enabled = false;
        countDownText.enabled = false;

        // カウントダウンコルーチンの停止
        StopCoroutine(gameOverCountDownCoroutine);
        // コルーチン参照をクリアする
        gameOverCountDownCoroutine = null;

        // カウントダウンの状態をリセットする
        UIManager.Instance.CountDownUIController.ResetCountDown();
    }
}
