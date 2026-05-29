using UnityEngine;

/// <summary>
/// Ray制御クラス
/// </summary>
public class RayController : MonoBehaviour
{
    // Rayの長さ
    private const float RAY_LENGTH = 100.0f;

    [Header("当たってほしいレイヤー")]
    [SerializeField] private LayerMask dropAreaLayer = default;

    // Rayが当たった情報を保持
    private RaycastHit hit = default;

    // Rayが当たったかどうかをほかクラスに知らせるためのフラグ
    public bool isHit { get; private set; } = false;

    /// <summary>
    /// Rayを飛ばす処理
    /// </summary>
    public void RayLaunch(Vector2 rayPosition, Camera launchCamera)
    {
        Ray ray = launchCamera.ScreenPointToRay(rayPosition);

        // Rayが当たったオブジェクトの情報を格納
        if (Physics.Raycast(ray, out hit, RAY_LENGTH, dropAreaLayer))
        {
            isHit = true;
        }
        else
        {
            isHit = false;
        }
    }


    /// <summary>
    /// Rayが当たった座標を返す
    /// </summary>
    public Vector3 GetHitPoint()
    {
        return hit.point;
    }
}
