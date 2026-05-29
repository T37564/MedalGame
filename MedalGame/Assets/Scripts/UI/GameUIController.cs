using TMPro;
using UnityEngine;

/// <summary>
/// ゲーム中のUI更新処理などを行うクラス
/// </summary>
public class GameUIController : MonoBehaviour
{
    [Header("メダル枚数表示用テキスト")]
    [SerializeField] private TMP_Text medalCount = null;

    /// <summary>
    /// コインの枚数を表示するUIの更新
    /// </summary>
    public void UpdateMedalCountUI(int currentMedal)
    {
        medalCount.text = currentMedal.ToString();
    }
}
