using UnityEngine;

/// <summary>
/// アプリケーションの設定を行うクラス
/// </summary>
public class ApplicationSettings : MonoBehaviour
{
    private readonly int GAME_FRAME_RATE = 120;

    /// <summary>
    /// 垂直同期と最大フレームレートの設定を行う
    /// </summary>
    private void Awake()
    {
        // 垂直同期を無効化
        QualitySettings.vSyncCount = 0;

        // 最大フレームレートを120FPSに設定
        Application.targetFrameRate = GAME_FRAME_RATE;
    }
}
