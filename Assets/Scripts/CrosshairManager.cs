using UnityEngine;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour
{
    public Sprite MouseDefault;
    public Sprite MouseOnTarget;

    public Image MouseImageHolder; 

    void Start()
    {
        MouseImageHolder.sprite = MouseDefault;
    }
    
    public void TargetEnter()
    {
        MouseImageHolder.sprite = MouseOnTarget;
    }

    public void TargetExit()
    {
        MouseImageHolder.sprite = MouseDefault;
    }

    public void Disable()
    {
        MouseImageHolder.gameObject.SetActive(false);
    }

    public void Enable()
    {
        MouseImageHolder.gameObject.SetActive(true);
    }
}
