using TMPro;
using UnityEngine;

/// <summary>
/// ゲーム中のUI更新処理などを行うクラス
/// </summary>
public class GameUIController : MonoBehaviour
{
    [Header("メダル枚数表示用テキスト")]
    [SerializeField] private TMP_Text medalCount = null;

    [Header("現在の使用メダル枚数表示用テキスト")]
    [SerializeField] private TMP_Text activeMedalCount = null;

    [Header("ゲーム中のUI")]
    [SerializeField] private Canvas gameCanvas = null;

    [Header("LaunchCameraUI")]
    [SerializeField] private Canvas launchCameraUI = null;

    [Header("メダルプールマネージャー")]
    [SerializeField] private MedalPoolManager medalPoolManager = null;

    /// <summary>
    /// 現在の使用メダル枚数を表示するUIの更新
    /// </summary>
    private void Update()
    {
        activeMedalCount.text = $"Active:{medalPoolManager.ActiveMedalCount}";
    }


    /// <summary>
    /// コインの枚数を表示するUIの更新
    /// </summary>
    public void UpdateMedalCountUI(int currentMedal)
    {
        medalCount.text = currentMedal.ToString();
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
