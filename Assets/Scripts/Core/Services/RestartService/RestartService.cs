using System.Collections.Generic;
using Views;
using Zenject;

namespace Core
{
    /// <summary>
    /// Service for object reloading. Objects should self-register with this service under an abstraction
    /// </summary>
    public class RestartService : IInitializable, ILateDisposable
    {
        private readonly IInputService _inputService;
        private readonly HUB _hub;
        private readonly List<IRestartable> _restartables = new();

        public RestartService(IInputService inputService, HUB hub)
        {
            _inputService = inputService;
            _hub = hub;
        }

        //Invoke the 'Restart' method for all registered objects
        private void Restart()
        {
            foreach (var restartable in _restartables)
            {
                restartable.Restart();
            }
        }

        //Add an object under an abstraction
        public void AddRestartable(IRestartable restartable) => _restartables.Add(restartable);
        
        //Subscribe on restart input
        public void Initialize()
        {
            _inputService.OnRestart += Restart;
            _hub.RestartButton.OnRestart += Restart;
        }

        //Unsubscribe on restart input
        public void LateDispose()
        {
            _inputService.OnRestart -= Restart;
            _hub.RestartButton.OnRestart -= Restart;
        }
    }
}