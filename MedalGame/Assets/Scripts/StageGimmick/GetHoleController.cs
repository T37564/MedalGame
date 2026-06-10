using UnityEngine;

/// <summary>
/// メダルがゲットホールに入った際の処理を行うクラス
/// </summary>
public class GetHoleController : MonoBehaviour
{
    // 衝突したときに反応してほしいタグ名
    private readonly string MEDAL_TAG = "Medal";

    [Header("メダルマネージャー 参照用")]
    [SerializeField] private MedalManager medalManager = null;

    /// <summary>
    /// メダルがホールへ入った際の処理
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // メダルか確認する
        if (other.gameObject.CompareTag(MEDAL_TAG))
        {
            // メダル枚数を増やす
            medalManager.AddMedal();

            // メダルの制御クラスを取得する
            MedalController medalController = other.gameObject.GetComponent<MedalController>();

            // MedalControllerが取得できた場合はメダルを回収する
            if (medalController != null)
            {
                medalController.ReturnMedal();
            }
        }
    }
}
