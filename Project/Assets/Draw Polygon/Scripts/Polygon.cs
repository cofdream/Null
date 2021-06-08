using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DA.Draw
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Polygon
        : MonoBehaviour
    {
        public int SideCount;
        public float radiusLength = 1;

        private Mesh mesh;

        [Range(0, 1)]
        public float[] Proxy;

        public bool isRandom;

        void Awake()
        {
            mesh = GetComponent<MeshFilter>().mesh;

            Proxy = new float[SideCount];
            for (int i = 0; i < SideCount; i++)
            {
                if (isRandom)
                {
                    Proxy[i] = Random.Range(0, 1f);
                }
                else
                {
                    Proxy[i] = 1;
                }
            }

            DrawPolygon();
        }

        public void Update()
        {
#if UNITY_EDITOR
            if (isDrawPolygon)
            {
                isDrawPolygon = false;
                DrawPolygon();
            }
#endif
        }

        public void DrawPolygon()
        {

            var vertuces = new Vector3[SideCount + 1];

            vertuces[0] = Vector3.zero;

            float degree = 2 * Mathf.PI / SideCount;

            for (int i = 0; i < SideCount; i++)
            {
                vertuces[i + 1] = new Vector3(Mathf.Sin(degree * i), Mathf.Cos(degree * i), 0) * (radiusLength * Proxy[i]);
            }

            mesh.vertices = vertuces;
#if UNITY_EDITOR
            this.vertuces = vertuces;
#endif


            int length = SideCount * 3;
            var triangles = new int[length];

            for (int i = 0, j = 1; i < length; i += 3, j++)
            {
                triangles[i] = 0;
                triangles[i + 1] = j;
                triangles[i + 2] = j + 1;
            }

            triangles[length - 1] = 1;

            mesh.triangles = triangles;
        }


#if UNITY_EDITOR
        private Vector3[] vertuces;
        public bool isDrawPolygon = false;
        private void OnDrawGizmos()
        {
            if (vertuces == null)
            {
                return;
            }
            Gizmos.color = Color.black;
            for (int i = 0; i < vertuces.Length; i++)
            {
                Gizmos.DrawSphere(vertuces[i], 0.1f);
            }
        }
#endif
    }
}