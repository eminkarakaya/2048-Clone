using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance {get; private set;}
    private void Awake() {
        instance = this;
    }
    public float scale;
    public GridController prefabGrid;
    public List<GridController> allGrids;
    public Vector2 firstPos;
    public Vector2 currPos;
    public int kacakac;
    public float distance;    
    public Vector2Int index = new Vector2Int(0,0);
   
    public void CreateGrid()
    {
        GameObject parent = new GameObject("Parent");
        for (int i = 0; i < kacakac; i++)
        {
            for (int j = 1; j < kacakac; j++)
            {
                index.x ++;
                currPos.x += distance;
                GridController gridController;
                gridController = Instantiate(prefabGrid,currPos,Quaternion.identity,parent.transform);
                gridController.transform.localScale = new Vector2(scale,scale);
                gridController.name = "Cube " + index;
                gridController.index = index;
                
                allGrids.Add(gridController);
            }
            index.x = 0;
            currPos.x = firstPos.x;

            GridController gridController1;
            gridController1 = Instantiate(prefabGrid,currPos,Quaternion.identity,parent.transform);
            gridController1.transform.localScale = new Vector2(scale,scale);
            gridController1.index = index;
            gridController1.name = "Cube " + index;
            index.y ++;
            currPos.y -= distance;
            allGrids.Add(gridController1);
        }
        ButtonManager.instance.CreateRandomNumber();
    }
    private void Start() 
    {
        CreateGrid();
        ButtonManager.instance.CreateRandomNumber();
    }
}
