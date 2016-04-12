using UnityEngine;
using FixingISSGame;

[System.Serializable]
public class Phase
{
    public GameObject[] EvaluationObjects;
    public GameObject[] ToolsToUse;

    public bool EvaluatePhase()
    {
        bool final = true;
        foreach(GameObject obj in EvaluationObjects)
        {
            switch(obj.tag)
            {
                case "Paintable":
                    {
                        Debug.Log("paintable found");
                        final &= obj.GetComponent<ColorMesh>().Evaluate();
                        break;
                    }
                case "Instrument":
                    {
                        Debug.Log("instrument found");
                        final &= obj.GetComponent<Item>().Evaluate();
                        break;
                    }
            }
        }

        return final;
    }


}
