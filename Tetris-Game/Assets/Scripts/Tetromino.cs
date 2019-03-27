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

    [SerializeField]
    protected GameObject[] cubes = new GameObject[4];

}
