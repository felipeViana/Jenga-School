using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    private Material originalMaterial;
    private SchoolConcept concept = null;

    void Start()
    {
        originalMaterial = this.gameObject.GetComponent<MeshRenderer>().material;
        GameController.Instance.SetActiveInfoText(false);
    }

    public SchoolConcept GetConcept()
    {
        return concept;
    }

    public void SetConcept(SchoolConcept newValue)
    {
        concept = newValue;
    }

    private void OnMouseOver()
    {
        Material highlightMaterial = GameController.Instance.GetHighlightMaterial();
        this.gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
        if (Input.GetMouseButtonUp(1))
        {
            GameController.Instance.SetActiveInfoText(true);
            GameController.Instance.SetBlockInfoText(concept.ToString());
        }
    }

    private void OnMouseExit()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
    }
}
