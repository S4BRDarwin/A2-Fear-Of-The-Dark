using UnityEngine;
using System;

[CreateAssetMenu(fileName ="ViewChangeSO")]
public class ViewChangeSO : ScriptableObject
{
    
    public event Action ChangeViewIndoor;
    public event Action ChangeViewOutdoor;

    public void RaiseEventChangeViewIndoor()
    {
        ChangeViewIndoor?.Invoke();
    }

    public void RaiseEventChangeViewOutdoor()
    {
        ChangeViewOutdoor?.Invoke();
    }

}
