using UnityEngine;

/// <summary>
/// Rayを飛ばしてヒット判定を行うクラス
/// </summary>
public class RayController : MonoBehaviour
{
    // Rayの長さ
    private readonly float RAY_LENGTH = 100.0f;

    [Header("Rayが当たってほしいレイヤー")]
    [SerializeField] private LayerMask dropAreaLayer = default;

    // Rayが当たった情報を保持するための変数
    private RaycastHit hit = default;

    /// <summary>
    /// Rayがヒットしたかどうか
    /// </summary>
    public bool IsHit { get; private set; } = false;

    /// <summary>
    /// Rayを飛ばす処理
    /// </summary>
    public void LaunchRay(Camera launchCamera, Vector2 localPoint)
    {
        Ray ray = launchCamera.ViewportPointToRay(new Vector3(localPoint.x, localPoint.y, 0));

        // Rayを飛ばし、ヒットしたかどうかを保存する
        IsHit = Physics.Raycast(ray, out hit, RAY_LENGTH, dropAreaLayer);
    }


    /// <summary>
    /// Rayが当たった座標を返す
    /// </summary>
    public Vector3 GetHitPoint()
    {
        return hit.point;
    }
}
