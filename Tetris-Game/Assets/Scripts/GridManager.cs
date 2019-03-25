using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // dimensions of the grid/board
    public static int width = 10; // x-coordinate
    public static int height = 20; // y-coordinate

    // grid will represent the area that the tetrominos have to stay within
    public static Transform[,] grid = new Transform[width, height];


    // checks to see if any tetrimino block goes beyond the grid's height
    public bool IsAboveTheGrid(Tetromino tetromino)
    {
        for (int x = 0; x < width; x++)
        {
            foreach (Transform cube in tetromino.transform)
            {
                Vector2 position = RoundPositions(cube.position);
                if (position.y > height - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // updates the grid of what positions in the grid are taken
    // and which aren't
    public void UpdateGrid(Tetromino tetromino)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent == tetromino.transform)
                    {
                        grid[x, y] = null;
                    }
                }
            } // end of inner for-loop
        } // end of outer for-loop
        foreach (Transform cube in tetromino.transform)
        {
            Vector2 position = RoundPositions(cube.position);
            if (position.y < height)
            {
                grid[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)] = cube;
            }
        }
    }

    // gets the Transform at a specific x,y coordinates in the grid
    public Transform GetTransformAtGridCoordinate(Vector2 position)
    {
        if (position.y >= height)
        {
            return null;
        }
        else
        {
            return grid[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)];
        }
    }

    // in case the positions/coordinates aren't integers
    // having it in rounded, whole, numbers will make
    // transformations uniform (or close to uniform) 
    public static Vector2 RoundPositions(Vector2 position)
    {
        return new Vector2(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
    }

    // checks to see if a certain coordinate is within the
    // grid boundaries.
    public static bool IsWithinBoundaries(Vector2 position)
    {
        if ((Mathf.RoundToInt(position.x) >= 0) && (Mathf.RoundToInt(position.x) < width))
        {
            if (Mathf.RoundToInt(position.y) >= 0 && Mathf.RoundToInt(position.y) < 21)
            {
                return true;
            }
        }
        return false;
    }

    // checks if a row is full
    public bool IsFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            // if returned a null instead of a Transform then not full
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        // increment everytime a full row is encountered
        GameMaster.numOfRows++;
        GameMaster.lines++;
        return true;
    }

    // deletes a full row (helper function to ClearAndUpdateRows())
    public void ClearRow(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    // deletes full rows and moves the row above downwards
    // y = row index
    public void ClearAndUpdateRows()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsFull(y))
            {
                ClearRow(y);
                MoveAllRowsDown(y + 1);
                --y;
            }
        }
    }

    // moves remaining rows down after a full row is cleared
    public static void MoveRowDown(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // moves all the rows down
    public static void MoveAllRowsDown(int y)
    {
        for (int i = y; i < height; i++)
        {
            MoveRowDown(i);
        }
    }
}
