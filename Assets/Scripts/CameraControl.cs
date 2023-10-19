using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private Vector3 mouseDownPosition;
    
    void Update()
    {
        ControlCamera();
    }
    private void ControlCamera()
    {
        GameObject selectedStack = GameController.Instance.GetComponent<StackSelection>().GetSelectedStack();
        if (selectedStack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 directionToMove = (Input.mousePosition - mouseDownPosition).normalized;

                if (directionToMove.x > 0)
                {
                    transform.Rotate(speed * Time.deltaTime * Vector3.down);
                }
                else if (directionToMove.x < 0)
                {
                    transform.Rotate(speed * Time.deltaTime * Vector3.up);
                }
            }
        }
    }
}
