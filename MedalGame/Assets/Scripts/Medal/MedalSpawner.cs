using UnityEngine;

/// <summary>
/// オブジェクトプールからメダルを取り出し初期配置する処理
/// </summary>
public class MedalSpawner : MonoBehaviour
{
    [Header("メダルプール管理クラス")]
    [SerializeField] MedalPoolManager medalPoolManager = null;

    [Header("メダルを最初に配置するときに使用する位置")]
    [SerializeField] private Transform medalSpawnerPosition = null;



    /// <summary>
    /// メダル生成処理
    /// </summary>
    private void Start()
    {
        for (int i = 0; i < 50; i++)
        {
            // メダルを取り出す
            GameObject rentedMedal = medalPoolManager.GetMedal();

            // メダルのRigidbodyを取り出す
            Rigidbody medalRigidBody = rentedMedal.GetComponent<Rigidbody>();

            // 生成位置を決定
            Vector3 generationPosition =
                medalSpawnerPosition.position +
                new Vector3(
                    Random.Range(-20.0f, 20.0f),
                    i * 0.05f,// 少しずつ高さを上げて重なりを防ぐ
                    Random.Range(-20.0f, 20.0f)
                    );

            // Rigidbody側の位置を変更する
            // Rigidbody.positionを使用して、物理演算による位置補正の不整合を防ぐ
            medalRigidBody.position = generationPosition;
        }
    }
}
