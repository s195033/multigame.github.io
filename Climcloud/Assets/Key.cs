using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{ 
    // �e��ID��Ԃ��v���p�e�B
    public int Id { get; private set; }

    // �����e���ǂ�����ID�Ŕ��肷�郁�\�b�h
    public bool Equals(int id) => id == Id;
}
