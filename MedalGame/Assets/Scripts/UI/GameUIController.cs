using TMPro;
using UnityEngine;

/// <summary>
/// ゲーム中のUIの更新や表示切り替えを管理するクラス
/// </summary>
public class GameUIController : MonoBehaviour
{
    // 使用中のメダル枚数表示に使用する先頭文字
    private readonly string ACTIVE_MEDAL_TEXT = "ActiveMedal";


    [Header("メダル枚数表示用Text")]
    [SerializeField] private TMP_Text medalCountText = null;

    [Header("現在の使用メダル枚数表示用Text")]
    [SerializeField] private TMP_Text activeMedalCountText = null;

    [Header("ゲーム中のUI")]
    [SerializeField] private Canvas gameCanvas = null;

    [Header("LaunchCameraUI")]
    [SerializeField] private Canvas launchCameraUI = null;

    [Header("メダルプールマネージャー参照用")]
    [SerializeField] private MedalPoolManager medalPoolManager = null;

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
}
