using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public void TestMyStack()
    {
        Physics.simulationMode = SimulationMode.FixedUpdate;

        EliminateGlassesBlocks();
    }
    public void RestartStacks()
    {
        Physics.simulationMode = SimulationMode.Script;
        RestoreStacks();
    }

    private void EliminateGlassesBlocks()
    {
        GameObject selectedStack = this.GetComponent<StackSelection>().GetSelectedStack();
        selectedStack.GetComponent<Stack>().DestroyGlassesBlocks();
    }

    private void RestoreStacks()
    {
        DestroyAllBlocks();
        RecreateAllBlocks();
    }

    private void DestroyAllBlocks()
    {
        GameObject selectedStack = this.GetComponent<StackSelection>().GetSelectedStack();
        selectedStack.GetComponent<Stack>().DestroyAllBlocks();
    }

    private void RecreateAllBlocks()
    {
        GameObject selectedStack = this.GetComponent<StackSelection>().GetSelectedStack();
        selectedStack.GetComponent<Stack>().CreateBlocks();
    }
}
