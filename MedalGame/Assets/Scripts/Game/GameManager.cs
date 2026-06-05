using UnityEngine;

/// <summary>
/// ゲームの初期設定を行うクラス
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 垂直同期と最大フレームレートの設定を行う
    /// </summary>
    private void Awake()
    {
        //// 垂直同期を無効化
        QualitySettings.vSyncCount = 0;

        // 最大フレームレート設定
        Application.targetFrameRate = 120;
    }
}
