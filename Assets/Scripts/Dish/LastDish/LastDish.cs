using Core;
using Views;
using Zenject;

namespace Dish
{
    /// <summary>
    /// Object for processing the last dish
    /// </summary>
    public class LastDish : IInitializable, IRestartable
    {
        private readonly SaveLoadService _saveLoadService;
        private readonly RestartService _restartService;
        private readonly HUB _hub;

        private const string _defaultViewValue = "None";

        public LastDish(SaveLoadService saveLoadService, HUB hub, RestartService restartService)
        {
            _saveLoadService = saveLoadService;
            _restartService = restartService;
            _hub = hub;
        }

        //Processing the last dish
        public void SetDish(CookedDish cookedDish)
        {
            string dishViewValue = cookedDish.GetDishViewValue();
            _hub.LastDishView.SetValue(cookedDish.GetDishViewValue());
            _saveLoadService.SaveLasDishViewValue(dishViewValue);
        }
        
        public void Initialize()
        {
            _hub.LastDishView.SetValue(_saveLoadService.GetLastDishViewValue());
            _restartService.AddRestartable(this);
        }
        
        //Remove the last dish
        public void Restart()
        {
            _hub.LastDishView.SetValue(_defaultViewValue);
        }
    }
}