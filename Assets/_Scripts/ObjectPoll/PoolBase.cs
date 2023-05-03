using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// A simple base class to simplify object pooling in Unity 2021.
/// Derive from this class, call InitPool and you can Get and Release to your hearts content.
/// If you enjoyed the video or this script, make sure you give me a like on YT and let me know what you thought :)
/// </summary>
/// <typeparam name="T">A MonoBehaviour object you'd like to perform pooling on.</typeparam>
public abstract class PoolBase<T> : MonoBehaviour where T : MonoBehaviour {
    private T _prefab;
    private List<ObjectPool<T>> _pools = new List<ObjectPool<T>>();

    private List<ObjectPool<T>> Pools {
        get {
            if (_pools == null) throw new InvalidOperationException("You need to call InitPool before using it.");
            return _pools;
        }
        set => _pools = value;
    }

    protected void InitPool(T prefab, int initial = 100, int max = 120, bool collectionChecks = false) {
        _prefab = prefab;
        Pools.Add(new ObjectPool<T>(
            CreateSetup,
            GetSetup,
            ReleaseSetup,
            DestroySetup,
            collectionChecks,
            initial,
            max));
    }

    #region Overrides
    protected virtual T CreateSetup() => Instantiate(_prefab);
    protected virtual void GetSetup(T obj) => obj.gameObject.SetActive(true);
    protected virtual void ReleaseSetup(T obj) => obj.gameObject.SetActive(false);
    protected virtual void DestroySetup(T obj) => Destroy(obj);
    #endregion

    #region Getters
    public virtual T Get(int i) => Pools[i].Get();
    public void Release(T obj, int i) {
        obj.transform.parent = null;
        Pools[i].Release(obj);
    }
    #endregion
}