using UnityEngine;

/// <summary>
/// 現在の所持メダル枚数を管理するクラス
/// </summary>
public class MedalManager : MonoBehaviour
{
    /// <summary>
    /// 現在のメダルの枚数を保持するプロパティ
    /// </summary>
    public int CurrentMedalCount { get; private set; }

    /// <summary>
    /// メダルを加算,UIの更新
    /// </summary>
    public void AddMedal()
    {
        CurrentMedalCount++;

        UIManager.Instance.GameUIController.UpdateMedalCountUI(CurrentMedalCount);
    }
}
