using System;
using System.Threading.Tasks;
using UnityEngine;

public class TALGameDataManager : MonoBehaviour
{
    public event Action OnTALGameDataUpdated;
    public async Task LoadTALGameData()
    {
        try
        {
            //LOAD
            await Task.Delay(1000);
            OnTALGameDataUpdated?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }
}
