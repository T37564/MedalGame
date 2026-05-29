using UnityEngine;

/// <summary>
/// メダルがゲットホールに入った時の処理
/// </summary>
public class GetHoleController : MonoBehaviour
{
    [Header("メダルマネージャー 参照用")]
    [SerializeField] private MedalManager medalManger = null;

    /// <summary>
    /// メダルがホールへ入った際の処理
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // メダルか確認する
        if (other.gameObject.CompareTag("Medal"))
        {
            // メダル枚数を増やす
            medalManger.AddMedal();

            // メダルの制御クラスを取得する
            MedalController medalController = other.gameObject.GetComponent<MedalController>();

            if (medalController == null) return;

            // メダルを回収する
            medalController.MedalCollection();
        }
    }
}
