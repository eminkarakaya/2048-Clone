using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    public Text scoreText;
    public int score;
    public static ButtonManager instance {get; private set;}
    private void Awake() {
        instance = this;
    }
    [SerializeField] List<ButtonController> buttonControllers;
    [SerializeField] bool isInAnimation;
    bool isFinish;
    
    public List<ButtonController> allNumbers;    
    [SerializeField] List<ButtonController> prefabs;
    
    //[SerializeField] GridManager gridManeger;
    List<GridController> allGrids;
    public List<GridController> templist;
    
    private void Start() {
        allGrids = GridManager.instance.allGrids;
        SetTempList();
    }
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.W) && !isInAnimation && !isFinish)
        {
            CheckButtons(KeyCode.W);
            Movement(KeyCode.W);
            CheckFinish();
        }
        else if(Input.GetKeyDown(KeyCode.A)&& !isInAnimation)
        {
            CheckButtons(KeyCode.A);
            Movement(KeyCode.A);
            CheckFinish();
            
        }
        else if(Input.GetKeyDown(KeyCode.D)&& !isInAnimation)
        {
            CheckButtons(KeyCode.D);
            Movement(KeyCode.D);   
            CheckFinish();
        }
        else if(Input.GetKeyDown(KeyCode.S)&& !isInAnimation)
        {
            CheckButtons(KeyCode.S);
            Movement(KeyCode.S);
            CheckFinish();
        }
    }
    public void Home()
    {
        GameManager.instance.Home();
    }
    public void Retry()
    {
        GameManager.instance.Retry();
    }
    // public List<ButtonController> CheckButtons(KeyCode code)
    void Win()
    {
        isFinish = true;
    }
    void Lose()
    {
        isFinish = true;
    }
    void CheckFinish()
    {
        for (int i = 0; i < allNumbers.Count; i++)
        {
            if(allNumbers[i].value == 2048)
            {
                Win();
            }
        }
    }
    void CheckButtons(KeyCode code)
    {
        if(code == KeyCode.W)
        {

            List<ButtonController> buttonControllers = new List<ButtonController>();
            for (int i = 0; i <= GridManager.instance.kacakac ; i++)
            {
                for (int j = 0; j < allNumbers.Count; j++)
                {
                    if(allNumbers[j].index.y == i)
                    {
                        buttonControllers.Add(allNumbers[j]);
                    }
                    
                }
            }
            allNumbers = buttonControllers;
        }
        else if(code == KeyCode.A)
        {
            List<ButtonController> buttonControllers = new List<ButtonController>();
            for (int i = 0; i <= GridManager.instance.kacakac; i++)
            {
                for (int j = 0; j < allNumbers.Count; j++)
                {
                    if(allNumbers[j].index.x == i)
                    {
                        buttonControllers.Add(allNumbers[j]);
                    }
                    
                }
            }
            allNumbers = buttonControllers;
        }
        else if(code == KeyCode.S)
        {
            List<ButtonController> buttonControllers = new List<ButtonController>();
            for (int i = GridManager.instance.kacakac; i >= 0; i--)
            {
                for (int j = 0; j < allNumbers.Count; j++)
                {
                    if(allNumbers[j].index.y == i)
                    {
                        buttonControllers.Add(allNumbers[j]);
                    }
                }
            }
            allNumbers = buttonControllers;
        }
        else if(code == KeyCode.D)
        {
            List<ButtonController> buttonControllers = new List<ButtonController>();
            for (int i = GridManager.instance.kacakac; i >= 0; i--)
            {
                for (int j = 0; j < allNumbers.Count; j++)
                {
                    if(allNumbers[j].index.x == i)
                    {
                        buttonControllers.Add(allNumbers[j]);
                    }
                        
                }
            }
            allNumbers = buttonControllers;
        }
    }
    void SetTempList()
    {
        for (int i = 0; i <  GridManager.instance.allGrids.Count; i++)
        {
            if(! GridManager.instance.allGrids[i].isFull)
            {
                if(!templist.Contains(GridManager.instance.allGrids[i]))
                {
                    templist.Add( GridManager.instance.allGrids[i]);
                }
            }
            else
            {
                if(templist.Contains( GridManager.instance.allGrids[i]))
                {
                    templist.Remove( GridManager.instance.allGrids[i]);
                }
            }
        }
    }
    IEnumerator IsAnimation()
    {
        yield return new WaitForSeconds(0.25f);
        isInAnimation = false;
    }
   
    void Movement(KeyCode code)
    {
        if(Input.GetKeyDown(code))
        {
            bool didMove = false;
            isInAnimation = true;
            try
            {
            for (int i = 0; i < allNumbers.Count; i++)
            {
                while(allNumbers[i].gridController.GetNeighbor(code) != null && allNumbers[i] != null)
                {
                    if(!allNumbers[i].gridController.GetNeighbor(code).isFull)//|| allNumbers[i].gridController.top.buttonController.value != allNumbers[i].gridController.buttonController.value))
                    {
                        allNumbers[i].gridController.isFull = false;
                        allNumbers[i].transform.DOMove(allNumbers[i].gridController.GetNeighbor(code).transform.position,0.3f);
                        //allNumbers[i].index = allNumbers[i].gridController.top.index;
                        allNumbers[i].gridController = allNumbers[i].gridController.GetNeighbor(code);
                        allNumbers[i].gridController.buttonController = allNumbers[i];    
                        didMove = true;
                    }
                    else
                    {
                            if(allNumbers[i].gridController.GetNeighbor(code).isFull && allNumbers[i].value == allNumbers[i].gridController.GetNeighbor(code).buttonController.value)
                            {
                                allNumbers[i].gridController.GetNeighbor(code).buttonController.level ++;
                                allNumbers[i].gridController.GetNeighbor(code).buttonController.spriteRenderer = sprites[allNumbers[i].gridController.GetNeighbor(code).buttonController.level];
                                allNumbers[i].gridController.GetNeighbor(code).buttonController.value*=2;
                                score += allNumbers[i].gridController.GetNeighbor(code).buttonController.value;
                                scoreText.text = score.ToString();
                                allNumbers[i].gridController.GetNeighbor(code).buttonController.GetComponentInChildren<TMPro.TextMeshPro>().text = allNumbers[i].gridController.GetNeighbor(code).buttonController.value.ToString();
                                allNumbers[i].gridController.isFull = false;
                                allNumbers[i].gridController.buttonController = null;
                                // allNumbers[i].transform.DOMove(allNumbers[i].gridController.GetNeighbor(code).transform.position,0.5f);
                                allNumbers[i].gameObject.SetActive(false);
                                didMove = true;
                                buttonControllers.Remove(allNumbers[i]);
                                allNumbers.Remove(allNumbers[i]);
                                
                            }
                            else if(allNumbers[i].gridController.GetNeighbor(code).isFull)
                            {
                                break;
                            }
                        }
                    }

                StartCoroutine(IsAnimation());
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                StartCoroutine(IsAnimation());
                CreateRandomNumber();
                didMove = false;
            }                        
            if(didMove)
            {
                CreateRandomNumber();
            }
        }
    }
    public void CreateRandomNumber()
    {
        
        bool qwewqe = false;
        IEnumerator qwe()
        {
        yield return new WaitForSeconds(.2f);
        SetTempList();
        Vector2Int index;
        Vector2 newPos;
        System.Random random = new System.Random();
        int rndIndex = random.Next(0,templist.Count); 
        index = templist[rndIndex].index;
        for (int i = 0; i <templist.Count; i++)
        {
            if(templist[i].index == index)
            {
                newPos = templist[i].transform.position;
                ButtonController buttonController = Instantiate(prefabs[(int)Random.Range(0,prefabs.Count)],newPos,Quaternion.identity);
                buttonController.transform.localScale = new Vector2(GridManager.instance.scale,GridManager.instance.scale);
                allNumbers.Add(buttonController);
                buttonControllers.Add(buttonController);
                allNumbers[allNumbers.Count-1].gridController = templist[i];
                allNumbers[allNumbers.Count-1].gridController.buttonController = allNumbers[allNumbers.Count-1];
                qwewqe = true;
            }
        }    
        if(!qwewqe)
        {
            Lose();
        }
    }
    StartCoroutine(qwe());
    }
   
}
