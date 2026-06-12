using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// メダルをオブジェクトプールで管理するクラス
/// </summary>
public class MedalPoolManager : MonoBehaviour
{
    // メダルの初期数
    private readonly int DEFAULT_POOL_SIZE = 300;

    // メダルの最大数
    private readonly int MAX_POOL_SIZE = 400;


    [Header("複製するメダル")]
    [SerializeField] private GameObject medalPrefab = null;

    [Header("複製したメダルを入れる親オブジェクト")]
    [SerializeField] private Transform medalContainer = null;

    // Unity公式オブジェクトプール
    private ObjectPool<GameObject> medalPool = null;


    /// <summary>
    /// 使用中のメダル数を取得するプロパティ
    /// </summary>
    public int ActiveMedalCount => medalPool.CountActive;


    /// <summary>
    /// オブジェクトプールの初期化
    /// </summary>
    private void Awake()
    {
        medalPool = new ObjectPool<GameObject>(
           CreateMedal,         // 新しく作る
           OnTakeMedal,         // 使用開始
           OnReturnMedal,       // 戻す
           OnDestroyMedal,      // 削除
           true,                // 同じオブジェクトへの重複返却を防ぐ
           DEFAULT_POOL_SIZE,   // 初期数
           MAX_POOL_SIZE        // 最大数
         );

        // ObjectPoolはGet→Releaseを繰り返しても1個しか生成されないため、
        // 一度すべて取得してからまとめて返却し、初期数分のメダルを事前生成する
        List<GameObject> rentedMedals = new();

        // 初期数分のメダルを生成
        for (int i = 0; i < DEFAULT_POOL_SIZE; i++)
        {
            rentedMedals.Add(medalPool.Get());
        }

        // 生成したメダルをプールへ戻す
        foreach (GameObject medal in rentedMedals)
        {
            medalPool.Release(medal);
        }
    }


    /// <summary>
    /// メダルを複製する処理
    /// </summary>
    private GameObject CreateMedal()
    {
        return Instantiate(medalPrefab, medalContainer);
    }

    /// <summary>
    /// メダルを取り出し表示する処理
    /// </summary>
    private void OnTakeMedal(GameObject medal)
    {
        medal.SetActive(true);
    }


    /// <summary>
    /// メダルをプールに戻して非表示にする処理
    /// </summary>
    private void OnReturnMedal(GameObject medal)
    {
        medal.SetActive(false);
    }

    /// <summary>
    /// メダルを削除する処理
    /// </summary>
    private void OnDestroyMedal(GameObject medal)
    {
        Destroy(medal);
    }


    /// <summary>
    /// メダルをプールから取得する処理
    /// </summary>
    public GameObject GetMedal()
    {
        return medalPool.Get();
    }


    /// <summary>
    /// メダルをプールへ返却する処理
    /// </summary>
    public void ReturnMedal(GameObject medal)
    {
        medalPool.Release(medal);
    }
}
