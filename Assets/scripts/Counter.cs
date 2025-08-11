using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] Text _text;

    private int _counters = 0;
  

    public void CountersAdd()
    {
        _counters++;
        _text.text = "Resources : " + _counters.ToString(); 
    }
}
