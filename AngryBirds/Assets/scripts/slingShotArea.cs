using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class slingShotArea : MonoBehaviour
{
    [SerializeField]private LayerMask SlingShotAreaMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsWithinSlingShotArea() 
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(InputManager.mousePosition);
        if (Physics2D.OverlapPoint(worldPosition, SlingShotAreaMask))
        {
            return true;
        }
        else
        { 
            return false;
        }
    }
}
