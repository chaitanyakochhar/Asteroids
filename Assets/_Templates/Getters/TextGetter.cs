using UnityEngine;
using UnityEngine.UI;

public class TextGetter : MonoBehaviour
{
    public string key;
    public string prefix;
    public string suffix;
    
    private GameObject dataManager;
    private PlayerDataManager p;
    private string value;
    private bool test; 

    public void Start()
    {
        dataManager = GameObject.Find("Data Manager");
        if(dataManager!=null)
        {
            p = dataManager.GetComponent<PlayerDataManager>();
            if(p!=null)
            {
               test = p.GetData<string>(p.textCaptured, key, out value);
            }
        }
        UpdateText();

    }

    public void UpdateText()
    {
        if(test && GetComponent<Text>()!=null)
        {
            GetComponent<Text>().text = prefix +" "+ value +" "+ suffix;
        }
    }
}
