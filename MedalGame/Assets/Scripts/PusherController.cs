using UnityEngine;

/// <summary>
/// プッシャーを特定位置へ往復移動させる処理
/// </summary>
public class PusherController : MonoBehaviour
{
    // プッシャーの速度
    private const float MOVE_SPEED = 8.0f;

    // 到着判定差
    private const float ARRIVAL_JUDGMENT_DIFFERENCE = 0.01f;



    [Header("プッシャー本体")]
    [SerializeField] private Transform upperSection = null;

    [Header("プッシャー開始位置")]
    [SerializeField] private Transform startPoint = null;

    [Header("プッシャー終了位置")]
    [SerializeField] private Transform endPoint = null;



    // 現在向かっている目的地
    private Transform currentTarget = null;


    /// <summary>
    /// プッシャーを移動させるための目的地を入れる
    /// </summary>
    private void Start()
    {
        currentTarget = endPoint;
    }


    /// <summary>
    /// プッシャーを往復移動させる
    /// </summary>
    private void Update()
    {
        PusherMovement(currentTarget);

        // 現在位置と目的地の距離を測定
        // Distance(位置A, 位置B)
        if (Vector3.Distance(
            upperSection.position,
            currentTarget.position) < ARRIVAL_JUDGMENT_DIFFERENCE)
        {
            // 行き先切り替え
            currentTarget = currentTarget == endPoint ? startPoint : endPoint;
        }
    }


    /// <summary>
    /// 特定のポジションにプッシャーを移動させる処理
    /// </summary>
    private void PusherMovement(Transform movePoint)
    {
        // 一定速度で移動させるためMoveTowardsを使用
        // MoveTowards(現在位置, 目的地, 移動量);
        upperSection.position =
            Vector3.MoveTowards(
                upperSection.position,
                movePoint.position,
                MOVE_SPEED * Time.deltaTime
                );
    }
}
