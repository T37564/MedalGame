using TMPro;
using UnityEngine;

/// <summary>
/// FPSを表示するクラス
/// </summary>
public class FPSDisplay : MonoBehaviour
{
    // FPS警告表示の時の値
    private const float FPS_CAUTION = 60.0f;

    // ゲームオーバー時の値
    private const float FPS_DANGER = 30.0f;


    [Header("FPS表示テキスト")]
    [SerializeField] private TMP_Text fpsText = null;

    [Header("FPS管理クラス参照用")]
    [SerializeField] private FPSManager fpsManager = null;

    // 文字変更時に使用するオレンジ色
    private Color orange = new Color(1f, 0.5f, 0f);

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
            fpsText.color = orange;
        }
        else
        {
            fpsText.color = Color.yellow;
        }

        fpsText.text = $"FPS : {Mathf.RoundToInt(fpsManager.CurrentFps)}";
    }
}
