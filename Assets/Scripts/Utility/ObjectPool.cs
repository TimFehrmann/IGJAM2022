using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public sealed class ObjectPool<T> where T : MonoBehaviour
    {
        private IList<T> pool = new List<T>();
        private T[] instantiatePrefab;

        /// <summary>
        /// Use if more than 1 Type of prefab is needed
        /// </summary>
        /// <param name="startSize">Amount of startinstances of every prefab</param>
        /// <param name="prefabs"></param>
        public ObjectPool(int startSize, T[] prefabs)
        {
            this.instantiatePrefab = prefabs;

            for (int i = 0; i < this.instantiatePrefab.Length; i++)
                for (int k = 0; k < startSize; k++)
                    SpawnPrefab(i);
        }

        /// <summary>
        /// Use if only 1 Type of prefab is used
        /// </summary>
        /// <param name="size"></param>
        /// <param name="prefab"></param>
        public ObjectPool(int size, T prefab)
        {
            this.instantiatePrefab = new T[1];
            this.instantiatePrefab[0] = prefab;

            for (int i = 0; i < size; i++)
                SpawnPrefab(0);
        }

        private void SpawnPrefab(int id)
        {
            T obj = GameObject.Instantiate<T>(instantiatePrefab[id]);
            pool.Add(obj);
            obj.gameObject.SetActive(false);
        }

        /// <summary>
        /// Returns always the last object added to pool, use when only 1 Type of prefab is used
        /// </summary>
        /// <returns></returns>
        public T GetObject()
        {
            if (pool.Count <= 0)
                SpawnPrefab(0);

            T result = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);

            return result;
        }

        /// <summary>
        /// Returns a random object from the pool, use when using more than 1 Type of prefab
        /// </summary>
        /// <returns></returns>
        public T GetRandomObject()
        {
            int rnd = Random.Range(0, instantiatePrefab.Length);
            if (pool.Count <= 0)
                SpawnPrefab(rnd);

            rnd = Random.Range(0, pool.Count);
            T result = pool[rnd];
            pool.RemoveAt(rnd);
            return result;
        }

        public void AddToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }
}