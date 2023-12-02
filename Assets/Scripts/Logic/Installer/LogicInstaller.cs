using Dish;
using GameItem;
using UnityEngine;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Zenject installer logic objects and all data
    /// </summary>
    public class LogicInstaller : MonoInstaller
    {
        [SerializeField]
        private IngredientContainer _ingredientContainer;

        [SerializeField]
        private IngredientItemsData _ingredientItemsData;

        [SerializeField]
        private ContainerOfDishData _containerOfDishData;
        
        [SerializeField]
        private Cauldron _cauldron;
        
        public override void InstallBindings()
        {
            Container.Bind<IngredientContainer>().FromInstance(_ingredientContainer).AsSingle().Lazy();
            Container.Bind<IngredientItemsData>().FromInstance(_ingredientItemsData).AsSingle().Lazy();
            Container.Bind<ContainerOfDishData>().FromInstance(_containerOfDishData).AsSingle().Lazy();
            Container.Bind<Cauldron>().FromInstance(_cauldron).AsSingle().Lazy();
            Container.Bind<DishIdentifier>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<BestDish>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<LastDish>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<CounterWorthDish>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<IngredientItemMovement>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<IngredientItemSpawner>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<InputInteractDetector>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<CookedHandler>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<IngredientItemWorth>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<InteractHandler>().AsSingle().Lazy();
            
            Container.BindInterfacesTo<IngredientHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DishHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DishCombinations>().AsSingle().NonLazy();
        }
    }
}