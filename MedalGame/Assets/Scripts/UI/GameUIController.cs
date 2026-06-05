using TMPro;
using UnityEngine;

/// <summary>
/// ゲーム中のUI更新処理などを行うクラス
/// </summary>
public class GameUIController : MonoBehaviour
{
    [Header("メダル枚数表示用テキスト")]
    [SerializeField] private TMP_Text medalCount = null;

    [Header("ゲーム中のUI")]
    [SerializeField] private GameObject gameUI = null;

    [Header("LaunchCameraUI")]
    [SerializeField] private GameObject launchCameraUI = null;

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
        gameUI.SetActive(isDisplay);
    }

    /// <summary>
    /// ミニカメラのUIの表示状態を変更する
    /// </summary>
    public void ChangeLaunchCameraUIState(bool isDisplay)
    {
        launchCameraUI.SetActive(isDisplay);
    }
}
