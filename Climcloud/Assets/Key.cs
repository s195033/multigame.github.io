using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{ 
    // 弾のIDを返すプロパティ
    public int Id { get; private set; }

    // 同じ弾かどうかをIDで判定するメソッド
    public bool Equals(int id) => id == Id;
}
