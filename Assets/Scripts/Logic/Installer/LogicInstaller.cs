using Dish;
using GameItem;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Logic
{
    /// <summary>
    /// Zenject installer logic objects all data
    /// </summary>
    public class LogicInstaller : MonoInstaller
    {
        [SerializeField]
        private IngredientContainer _ingredientContainer;

        [SerializeField]
        private IngredientItemsData _ingredientItemsData;

        [FormerlySerializedAs("_dishDatas")]
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
            Container.BindInterfacesAndSelfTo<BestDish>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LastDish>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<CounterWorthDish>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<IngredientMovement>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<IngredientSpawner>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<InputInteractDetector>().AsSingle().Lazy();
            Container.BindInterfacesAndSelfTo<CookedHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<IngredientDataValue>().AsSingle().NonLazy();

            Container.BindInterfacesTo<InteractHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DishHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DishCombinations>().AsSingle().NonLazy();
        }
    }
}