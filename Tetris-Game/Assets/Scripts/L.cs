﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L : Tetromino, ITetromino
{
    //public bool allowedRotation = true; // only the O tetrimino shouldn't be allowed to rotate
    float lastFallTime = 0;
    private float speed;

    TetrominoType ITetromino.GetType()
    {
        return Type;
    }

    void Start()
    {
        speed = GameObject.Find("BoardFrame").GetComponent<GameMaster>().speed;
        if (!IsAValidPosition())
        {
            Debug.Log("Game Over!!");
            Destroy(gameObject);
            FindObjectOfType<GameMaster>().GameOver();
        }
    }

    void Update()
    { // keyboard bindings
        if (!GameMaster.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //if (allowedRotation)
                //{
                transform.Rotate(0, 0, 90);
                if (IsAValidPosition())
                {
                    FindObjectOfType<GridManager>().UpdateGrid(this);
                }
                else
                {
                    transform.Rotate(0, 0, -90);
                }
                //}
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //if (allowedRotation)
                //{
                transform.Rotate(0, 0, -90);
                if (IsAValidPosition())
                {
                    FindObjectOfType<GridManager>().UpdateGrid(this);
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
                //}
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (IsAValidPosition())
                {
                    FindObjectOfType<GridManager>().UpdateGrid(this);
                }
                else
                {
                    transform.position += new Vector3(1, 0, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
                if (IsAValidPosition())
                {
                    FindObjectOfType<GridManager>().UpdateGrid(this);
                }
                else
                {
                    transform.position += new Vector3(-1, 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.Space) || Time.time - lastFallTime >= speed)
            {
                transform.position += new Vector3(0, -1, 0);
                if (IsAValidPosition())
                {
                    FindObjectOfType<GridManager>().UpdateGrid(this);
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);
                    FindObjectOfType<GridManager>().ClearAndUpdateRows();
                    enabled = false;
                    if (FindObjectOfType<GridManager>().IsAboveTheGrid(this))
                    {
                        FindObjectOfType<GameMaster>().GameOver();
                    }
                    FindObjectOfType<GameMaster>().SpawnNewTetromino();
                }
                lastFallTime = Time.time;
            }
        }
    }

    public bool IsAValidPosition()
    {
        foreach (Transform cube in transform)
        {
            Vector2 v = GridManager.RoundPositions(cube.position);
            if (!GridManager.IsWithinBoundaries(v))
            {
                return false;
            }

            if (FindObjectOfType<GridManager>().GetTransformAtGridCoordinate(v) != null
                && FindObjectOfType<GridManager>().GetTransformAtGridCoordinate(v).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
