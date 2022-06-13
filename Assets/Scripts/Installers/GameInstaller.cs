using GameLogics.FieldLogics;
using Services.Cockroach;
using Services.Game;
using Services.Stats;
using Services.UI;
using Services.Updater;
using Settings;
using UnityEngine;
using Views;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _settings;
    [SerializeField]
    private GameObject _playerPrefab;
    public override void InstallBindings()
    {
        Container.Bind<SettingsContainer>().FromComponentOn(_settings).AsSingle();
        Container.Bind<UpdateService>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<GameStateService>().AsSingle();
        Container.Bind<GameLoopManager>().AsSingle().NonLazy();
        Container.Bind<UIService>().AsSingle().NonLazy();
        
        Container.Bind<PlayerController>().AsSingle();
        Container.Bind<PlayerView>().FromComponentInNewPrefab(_playerPrefab).AsSingle().NonLazy();
        
        Container.BindInterfacesTo<StatsService>().AsSingle();
        Container.Bind<ICockroachManager>().To<CockroachManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
}