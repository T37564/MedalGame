using UnityEngine;

/// <summary>
/// ゲームオーバー条件を判定するクラス
/// </summary>
public class GameOverController : MonoBehaviour
{
    // ゲームオーバーになるFPS
    private const float GAME_OVER_FPS = 30.0F;

    [Header("FPS管理クラス参照用")]
    [SerializeField] private FPSManager fpsManager = null;

    /// <summary>
    /// ゲームオーバー判定を行う
    /// </summary>
    private void Update()
    {
        if (fpsManager.CurrentFps < GAME_OVER_FPS)
        {
            Debug.Log("ゲームオーバー");
        }
    }
}
