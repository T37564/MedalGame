using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// ゲームオーバー条件を判定するクラス
/// </summary>
public class GameOverController : MonoBehaviour
{
    // ゲームオーバーになるFPSの値
    private const float GAME_OVER_FPS_LIMIT = 30.0F;

    [Header("FPS管理クラス参照用")]
    [SerializeField] private FPSManager fpsManager = null;

    [Header("UIManager管理クラス参照用")]
    [SerializeField] private UIManager uiManager = null;

    [Header("ゲームオーバー時のUI管理クラス参照用")]
    [SerializeField] private GameOverUIController gameOverUIController = null;

    // ゲームオーバー処理を実行済みかのフラグ
    private bool isGameOverProcessed = false;

    /// <summary>
    /// ゲームオーバー判定を行う
    /// </summary>
    private void Update()
    {
        if (!fpsManager.IsInitialized)
        {
            return;
        }

        if (fpsManager.CurrentFps < GAME_OVER_FPS_LIMIT && !isGameOverProcessed)
        {
            isGameOverProcessed = true;

            // ゲーム終了時のUI遷移処理
            uiManager.ShowGameOverUI();

            // ゲーム時間を停止させる
            Time.timeScale = 0;
        }
    }
}
