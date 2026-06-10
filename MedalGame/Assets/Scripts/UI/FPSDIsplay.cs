using TMPro;
using UnityEngine;

/// <summary>
/// FPSを表示するクラス
/// </summary>
public class FPSDisplay : MonoBehaviour
{
    // FPSを表示するときの先頭文字
    private readonly string FPS_TEXT = "FPS:";

    // FPS警告表示時の値
    private readonly float FPS_CAUTION = 90.0f;

    // ゲームオーバー時の値
    private readonly float FPS_DANGER = 70.0f;

    // FPS警告表示時の文字色
    private readonly Color ORANGE = new Color(1.0f, 0.5f, 0.0f);



    [Header("FPS表示Text")]
    [SerializeField] private TMP_Text fpsText = null;

    [Header("FPS管理クラス参照用")]
    [SerializeField] private FPSManager fpsManager = null;



    /// <summary>
    /// FPSの表示を更新する
    /// </summary>
    private void Update()
    {
        // FPSに応じて文字色を変更
        if (fpsManager.CurrentFps <= FPS_DANGER)
        {
            fpsText.color = Color.red;
        }
        else if (fpsManager.CurrentFps <= FPS_CAUTION)
        {
            fpsText.color = ORANGE;
        }
        else
        {
            fpsText.color = Color.yellow;
        }

        // 現在のFPSを表示
        fpsText.text = $"{FPS_TEXT} {Mathf.RoundToInt(fpsManager.CurrentFps)}";
    }
}
