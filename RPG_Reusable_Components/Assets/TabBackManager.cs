using UnityEngine;

public class TabBackManager : Singleton<TabBackManager>
{
    [SerializeField] private GameObject tabBack;
    public bool currentlyOpen = true;
    
    public void Toggle()
    {
        currentlyOpen = !currentlyOpen;
        tabBack.SetActive(currentlyOpen);
    }
    
}
