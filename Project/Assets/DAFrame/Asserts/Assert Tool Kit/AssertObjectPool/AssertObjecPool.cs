using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using DA.ObjectPool;


namespace Tests
{
    public class AssertObjectPoolItem
    {
        public void Show()
        {

        }
    }

    public class AssertObjecPool
    {
        [Test]
        public void InitPool()
        {
            ObjectPool<AssertObjectPoolItem> itemObjectPool = new ObjectPool<AssertObjectPoolItem>();
            itemObjectPool.Initialize(
                () => new AssertObjectPoolItem(), null, null, null,
                5, 15);

            List<AssertObjectPoolItem> items = new List<AssertObjectPoolItem>(100);
            for (int i = 0; i < 100; i++)
            {
                var poolItem = itemObjectPool.Allocate();
                items.Add(poolItem);
                Assert.IsNotNull(poolItem);
            }
            foreach (var item in items)
            {
                itemObjectPool.Release(item);
            }
            itemObjectPool.ClearPool();
        }

        [UnityTest]
        public IEnumerator InitPoolTimer()
        {
            ObjectPoolTimer<AssertObjectPoolItem> itemObjectPool = new ObjectPoolTimer<AssertObjectPoolItem>();
            itemObjectPool.Initialize(
                () => new AssertObjectPoolItem(), null, null, null,
                5, 5,
                3, 10);

            List<AssertObjectPoolItem> items = new List<AssertObjectPoolItem>(100);
            for (int i = 0; i < 100; i++)
            {
                var poolItem = itemObjectPool.Allocate();
                items.Add(poolItem);
                Assert.IsNotNull(poolItem);
            }
            for (int i = 0; i < 99; i++)
            {
                itemObjectPool.Release(items[i]);
            }

            yield return new WaitForSeconds(8);
            Debug.Log(1);
            yield return new WaitForSeconds(3);
            Debug.Log(1);

            itemObjectPool.Release(items[99]);

            yield return new WaitForSeconds(8);
            Debug.Log(1);
            yield return new WaitForSeconds(3);
            Debug.Log(1);
        }

        [UnityTest]
        public IEnumerator InitPoolTimer2()
        {
            ObjectPoolTimer<AssertObjectPoolItem> itemObjectPool = new ObjectPoolTimer<AssertObjectPoolItem>();
            itemObjectPool.Initialize(() => new AssertObjectPoolItem(), null, null, null,
                                        5, 5,
                                        3, 10);

            List<AssertObjectPoolItem> items = new List<AssertObjectPoolItem>(100);
            for (int i = 0; i < 100; i++)
            {
                var poolItem = itemObjectPool.Allocate();
                items.Add(poolItem);
                Assert.IsNotNull(poolItem);
            }
            for (int i = 0; i < 95; i++)
            {
                itemObjectPool.Release(items[i]);
            }

            yield return new WaitForSeconds(8);
            Debug.Log(1);

            var item = itemObjectPool.Allocate();

            yield return new WaitForSeconds(12);
            Debug.Log(1);

            itemObjectPool.Release(item);

            yield return new WaitForSeconds(8);
            Debug.Log(1);
            yield return new WaitForSeconds(3);
            Debug.Log(1);
        }
    }
}
