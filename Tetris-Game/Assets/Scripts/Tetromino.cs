using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TetrominoType
{
    I, J, L, O, S, T, Z
};

public class Tetromino : MonoBehaviour
{
    public TetrominoType Type;

    // [Tooltip("Used for translation")]
    // public GameObject Root;

    //[Tooltip("Used for rotation")]
    //public GameObject Pivot;

    [SerializeField]
    protected GameObject[] cubes = new GameObject[4];

}
