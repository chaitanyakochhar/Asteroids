using UnityEngine;
using System.Collections.Generic;


public abstract class Getter : MonoBehaviour
{
    public abstract T GetDataFromDictionary<T>(string key);
    public abstract void Display();

}
