using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public int level = 0;
    private Sprite _spriteRenderer;
    public Sprite spriteRenderer{
        get => _spriteRenderer;
        set{
            _spriteRenderer = value;
            GetComponent<SpriteRenderer>().sprite = value;
        }
    }
    public int value;

    [SerializeField] private GridController _thisGridController;
    public GridController gridController {get => _thisGridController; set{
        _thisGridController = value;
        if(_thisGridController!=null)
        {
        _thisGridController.isFull = true;
        index = _thisGridController.index;
        }
    }}
    public Vector2Int index;
    
    void Update()
    {
        if(_thisGridController!= null)
            _thisGridController.value = this.value;
    }
}
