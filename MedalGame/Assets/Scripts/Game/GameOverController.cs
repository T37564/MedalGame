using UnityEngine;

/// <summary>
/// ゲームオーバー条件を判定するクラス
/// </summary>
public class GameOverController : MonoBehaviour
{
    // ゲームオーバー判定時使用するFPSの値
    private readonly float GAME_OVER_FPS_LIMIT = 70.0f;


    [Header("FPS管理クラス参照用")]
    [SerializeField] private FPSManager fpsManager = null;

    [Header("ゲームオーバー時のUI管理クラス参照用")]
    [SerializeField] private GameOverUIController gameOverUIController = null;

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
            // ゲームオーバー処理の重複実行を防ぐ
            isGameOverProcessed = true;

            // ゲームオーバーUIを表示する
            UIManager.Instance.ShowGameOverUI();

            // ゲーム時間を停止させる
            Time.timeScale = 0.0f;
        }
    }
}
