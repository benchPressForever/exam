using UnityEngine;
using UnityEngine.EventSystems;

public class ClickReceiver : MonoBehaviour , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 worldPos = eventData.pointerCurrentRaycast.worldPosition;
        Debug.Log($"Позиция в мире: {worldPos}");
    }

   
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
}
