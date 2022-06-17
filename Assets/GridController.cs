using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public ButtonController buttonController;
    public Vector2Int index;
    public Vector2 position;
    public bool isFull;
    public int value;
    public GridController topLeft;
    public GridController topRight;
    public GridController botLeft;
    public GridController botRight;
    public GridController top;
    public GridController bot;
    public GridController right;
    public GridController left;
    [SerializeField] GridManager gridManeger;

    public GridController GetNeighbor(KeyCode code)
    {
        if(code == KeyCode.W)
        {
            return top;
        }
        else if(code == KeyCode.S)
        {
            return bot;
        }
        else if(code == KeyCode.D)
        {
            return right;
        }
        else if(code == KeyCode.A)
        {
            return left;
        }
        return null;
    }
    public void SetNeighbor()
    {
        for (int i = 0; i < gridManeger.allGrids.Count; i++)
        {
            if(this.index.y == gridManeger.allGrids[i].index.y)
            {
                if(this.index.x +1 == gridManeger.allGrids[i].index.x)
                {
                    right = gridManeger.allGrids[i];
                }
                else if(this.index.x -1 == gridManeger.allGrids[i].index.x)
                {
                    left = gridManeger.allGrids[i];
                }
            }
            else if(this.index.x == gridManeger.allGrids[i].index.x)
            {
                if(this.index.y +1 == gridManeger.allGrids[i].index.y)
                {
                    bot = gridManeger.allGrids[i];
                }
                else if(this.index.y -1 == gridManeger.allGrids[i].index.y)
                {
                    top = gridManeger.allGrids[i];
                }
            }
            else if(this.index.x+1 == gridManeger.allGrids[i].index.x && this.index.y -1 == gridManeger.allGrids[i].index.y)
            {
                topRight = gridManeger.allGrids[i];
            }
            else if(this.index.x-1 == gridManeger.allGrids[i].index.x && this.index.y -1 == gridManeger.allGrids[i].index.y)
            {
                topLeft = gridManeger.allGrids[i];
            }
            else if(this.index.x-1 == gridManeger.allGrids[i].index.x && this.index.y +1 == gridManeger.allGrids[i].index.y)
            {
                botLeft = gridManeger.allGrids[i];
            }
            else if(this.index.x+1 == gridManeger.allGrids[i].index.x && this.index.y +1 == gridManeger.allGrids[i].index.y)
            {
                botRight = gridManeger.allGrids[i];
            }
        }
    }
    void Start()
    {
        gridManeger = FindObjectOfType<GridManager>();
        position = this.transform.position;
        SetNeighbor();
    }
    void Update()
    {
        if(!isFull)
        {
            value =0;
            buttonController = null;
        }
    }
}
