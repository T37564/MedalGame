using UnityEngine;

/// <summary>
/// ゲームオーバー条件を判定するクラス
/// </summary>
public class GameOverController : MonoBehaviour
{
    // ゲームオーバー判定時使用するFPSの値
    private readonly float GAME_OVER_FPS_LIMIT = 90.0f;


    [Header("FPS管理クラス参照用")]
    [SerializeField] private FPSManager fpsManager = null;

    // ゲームオーバー処理を実行済みかのフラグ
    private bool isGameOverProcessed = false;

    /// <summary>
    /// ゲームオーバー判定を行う
    /// </summary>
    private void Update()
    {
        // FPSがまだ計算されていないときは判定を行わない
        if (!fpsManager.IsInitialized) return;

        // FPSが判定値を下回った場合
        if (fpsManager.CurrentFps < GAME_OVER_FPS_LIMIT && !isGameOverProcessed)
        {
            // 5秒のカウントダウン開始
            　UIManager.Instance.GameUIController.StartGameOverCountDown();

            // カウントダウンが完了した場合
            if (UIManager.Instance.CountDownUIController.IsCountDownFinished)
            {
                ExecuteGameOver();
            }
        }
        else
        {
            // カウントダウンの停止
            UIManager.Instance.GameUIController.StopGameOverCountDown();
        }
    }

    /// <summary>
    /// ゲームオーバー時の処理
    /// </summary>
    private void ExecuteGameOver()
    {
        // ゲームオーバー処理の重複実行を防ぐ
        isGameOverProcessed = true;

        // ゲームオーバーUIを表示する
        UIManager.Instance.ShowGameOverUI();

        // ゲーム時間を停止させる
        Time.timeScale = 0.0f;
    }
}
