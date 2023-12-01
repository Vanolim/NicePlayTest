using Core;
using Views;
using Zenject;

namespace Dish
{
    /// <summary>
    /// Object for determining and processing the best dish based on worth.
    /// </summary>
    public class BestDish : IInitializable, IRestartable
    {
        private readonly SaveLoadService _saveLoadService;
        private readonly RestartService _restartService;
        private readonly HUB _hub;
        private int _currentBestDishWorthValue;
        
        private const string _defaultViewValue = "None";

        public BestDish(SaveLoadService saveLoadService, HUB hub, RestartService restartService)
        {
            _saveLoadService = saveLoadService;
            _restartService = restartService;
            _hub = hub;
        }
        
        //Save the data of the best dish
        private void SaveDish(string dishViewValue)
        {
            _saveLoadService.SaveBestDishViewValue(dishViewValue);
            _saveLoadService.SaveBestDishWorthValue(_currentBestDishWorthValue);
        }

        //Detecting and processing the best dish
        public void SetDish(CookedDish cookedDish)
        {
            if(cookedDish.Worth >= _currentBestDishWorthValue)
            {
                _currentBestDishWorthValue = cookedDish.Worth;
                string dishViewValue = cookedDish.GetDishViewValue();
                SaveDish(dishViewValue);
                _hub.BestDishView.SetValue(dishViewValue);
            }
        }

        public void Initialize()
        {
            _currentBestDishWorthValue = _saveLoadService.GetBestDishWorthValue();
            _hub.BestDishView.SetValue(_saveLoadService.GetBestDishViewValue());
            _restartService.AddRestartable(this);
        }

        //Remove the best dish
        public void Restart()
        {
            _currentBestDishWorthValue = 0;
            _hub.BestDishView.SetValue(_defaultViewValue);
        }
    }
}