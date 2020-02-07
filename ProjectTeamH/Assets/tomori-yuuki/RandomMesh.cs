﻿using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class RandomMesh : MonoBehaviour
{
        const string k_MeshName = "[Generated]RainMesh"; // メッシュの名前
    [SerializeField, Header("雨粒の数")]
    int m_TriangleCount = 500; // 三角形の個数
    [SerializeField, Header("雨粒の大きさ")]
    float m_TriangleScale = 0.3f; // 三角形の大きさ
    [SerializeField, Header("雨全体のバラつき")]
    Vector3 m_TriangleRange = new Vector3(4f, 4f, 4f); // メッシュの大きさ
    [SerializeField, HideInInspector]
    string m_OnCreateText = "メッシュ情報がありません\nメッシュを更新してください";

    public int TriangleCount { get { return m_TriangleCount; } }
    public float TriangleScale { get { return m_TriangleScale; } }
    public Vector3 TriangleRange { get { return m_TriangleRange; } }
    public string OnCreateText { get { return m_OnCreateText; } set { m_OnCreateText = value; } }

    /// <summary>
    /// メッシュの新規作成 
    /// </summary>
    public Mesh CreateNewMesh()
    {
        Vector3[] vertices = new Vector3[m_TriangleCount * 3]; // 頂点の座標
        int[] triangles = new int[m_TriangleCount * 3]; // 頂点インデックス
        Vector3[] normals = new Vector3[vertices.Length];
        int pos = 0;
        for (int i = 0; i < m_TriangleCount; i++)
        {
            var v1 = Vector3.Scale(new Vector3(Random.value, Random.value, Random.value) - Vector3.one * 0.5f, m_TriangleRange);
            var v2 = v1 + new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f) * m_TriangleScale;
            var v3 = v1 + new Vector3(Random.value - 0.5f, 0f, Random.value - 0.5f) * m_TriangleScale;

            vertices[pos + 0] = v1;
            vertices[pos + 1] = v2;
            vertices[pos + 2] = v3;
            pos += 3;
        }

        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i] = i;
        }

        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = new Vector3(0f, 1f, 0f);
        }

        //メッシュ生成
        var mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;

        return mesh;
    }
}
