using UnityEngine;

public class Resource : MonoBehaviour
{


    private bool _isChecked = false;

    public bool CheckResources()
    {
        return _isChecked;
    }

    public void Checked()
    {
        _isChecked = true;
    }


}
