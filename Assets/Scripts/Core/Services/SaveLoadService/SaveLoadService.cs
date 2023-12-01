using UnityEngine;

namespace Core
{
    /// <summary>
    /// Service for retrieving and storing data in PlayerPrefs
    /// </summary>
    public class SaveLoadService
    {
        public void SaveBestDishViewValue(string value) => PlayerPrefs.SetString("BestDishViewValue", value);
        public void SaveBestDishWorthValue(int value) => PlayerPrefs.SetInt("BestDishWorthValue", value);

        public void SaveLasDishViewValue(string value) => PlayerPrefs.SetString("LastDishViewValue", value);

        public void SaveCounter(int value) => PlayerPrefs.SetInt("CounterValue", value);

        public string GetBestDishViewValue() => PlayerPrefs.GetString("BestDishViewValue", "None");

        public int GetBestDishWorthValue() => PlayerPrefs.GetInt("BestDishWorthValue", 0);

        public string GetLastDishViewValue() => PlayerPrefs.GetString("LastDishViewValue", "None");
        
        public int GetCounterValue() => PlayerPrefs.GetInt("CounterValue", 0);
    }
}