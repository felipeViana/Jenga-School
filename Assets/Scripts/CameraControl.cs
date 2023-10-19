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
            transform.LookAt(selectedStack.transform.position);

            if (Input.GetMouseButtonDown(0))
            {
                mouseDownPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 directionToMove = Input.mousePosition - mouseDownPosition;
                transform.Translate(directionToMove * speed * Time.deltaTime);
            }

            if (transform.position.y < 0.5f)
            {
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            }
            else if (transform.position.y > 20f)
            {
                transform.position = new Vector3(transform.position.x, 20f, transform.position.z);
            }
        }
    }
}
