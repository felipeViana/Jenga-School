using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject[] Stacks;

    private int selectedStack = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("init");
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSelectedStack();
        


        
    }

    private void ChangeSelectedStack()
    {
        if (Input.GetKeyUp("right"))
        {
            selectedStack++;
            if (selectedStack >= Stacks.Length)
            {
                selectedStack = 0;
            }
            MoveCameraToStack();
        }
        else if (Input.GetKeyUp("left"))
        {
            selectedStack--;
            if (selectedStack < 0)
            {
                selectedStack = Stacks.Length - 1;
            }

            MoveCameraToStack();
        }
    }

    private void MoveCameraToStack()
    {
        Camera.main.transform.position = Stacks[selectedStack].transform.position + Vector3.back * 10 + Vector3.up * 5;
    }
}
