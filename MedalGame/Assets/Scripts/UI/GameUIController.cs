using TMPro;
using UnityEngine;

/// <summary>
/// ゲーム中のUI更新処理などを行うクラス
/// </summary>
public class GameUIController : MonoBehaviour
{
    [Header("メダル枚数表示用テキスト")]
    [SerializeField] private TMP_Text medalCount = null;

    [Header("現在のメダル枚数表示用テキスト")]
    [SerializeField] private TMP_Text activeMedalCount = null;
    [Header("現在の未使用メダル枚数表示用テキスト")]
    [SerializeField] private TMP_Text inactiveMedalCount = null;
    [Header("メダルの総数を表示するテキスト")]
    [SerializeField] private TMP_Text totalMedalCount = null;

    [Header("ゲーム中のUI")]
    [SerializeField] private GameObject gameUI = null;

    [Header("LaunchCameraUI")]
    [SerializeField] private GameObject launchCameraUI = null;

    [Header("メダルプールマネージャー")]
    [SerializeField] private MedalPoolManager medalPoolManager = null;

    /// <summary>
    /// 
    /// 現在のメダル枚数、未使用のメダル枚数、総メダル枚数を表示するUIの更新
    /// </summary>
    private void Update()
    {
        activeMedalCount.text = $"Active:{medalPoolManager.ActiveMedalCount}";
        inactiveMedalCount.text = $"Inactive:{medalPoolManager.InactiveMedalCount}";
        totalMedalCount.text = $"Total:{medalPoolManager.TotalMedalCount}";
    }

    private void Start()
    {
        Time.timeScale = 0.1f;
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
