using System;
using System.Threading.Tasks;
using UnityEngine;

public class TALUserDataManager : MonoBehaviour
{
    public event Action OnTALUserDataUpdated;
    public async Task LoadTALUserData()
    {
        try
        {
            //LOAD
            await Task.Delay(1000);
            OnTALUserDataUpdated?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }
}
