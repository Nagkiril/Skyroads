using UnityEngine;
using Skyroad.Obstacles;
using Skyroad.Obstacles.Pooling;
using Skyroad.Obstacles.Timers;
using Skyroad.Environment.Road;
using Skyroad.Environment.Road.Pooling;
using Skyroad.Environment.Level;
using Skyroad.Player;
using Skyroad.Score;
using Skyroad.UI.Start;
using Skyroad.UI.GameOver;
using Zenject;



//We might as well split main installer into multiple different parts, so that they can be edited separately, but I think it's redundant for the scope of the prototype
public class MainInstaller : MonoInstaller
{
    //We could also add some more complex logic to take full advantage of DI, but this is already good enough for our scope
    [SerializeField] private RoadSegmentPool _segmentPool;
    [SerializeField] private ObstaclesPool _obstaclesPool;
    [SerializeField] private InfiniteRoad _road;
    [SerializeField] private ScoreObstacleTimer _timer;
    [SerializeField] private ScoreManager _score;
    [SerializeField] private PlayerShip _player;
    [SerializeField] private PlayerScoreSizeSetter _scoreSetter;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameOverScreen _gameOverUI;
    [SerializeField] private StartScreen _startUI;
    [SerializeField] private ObstaclePassageCounter _passageCounter;
    [SerializeField] private LevelPlaytimeTimer _playtimer;


    public override void InstallBindings()
    {
        //These splits vividly tell us which way we could split installer if we wanted to
        Container.Bind<IRoadSegmentSpawner>().FromInstance(_segmentPool);
        Container.Bind<IRoadWidthProvider>().FromInstance(_road);

        Container.Bind<IObstacleSpawner>().FromInstance(_obstaclesPool);
        Container.Bind<IObstacleTimer>().FromInstance(_timer);
        Container.Bind<IObstacleSpray>().FromInstance(_road);
        Container.Bind<IObstaclePassProvider>().FromInstance(_passageCounter);

        Container.Bind<IScoreProvider>().FromInstance(_score);
        Container.Bind<IHighscoreProvider>().FromInstance(_score);
        Container.Bind<IScoreTickSizeProvider>().FromInstance(_scoreSetter);

        Container.Bind<IPlayer>().FromInstance(_player);

        Container.Bind<ILevelEventProvider>().FromInstance(_levelManager);
        Container.Bind<ILevelBeginCommand>().FromInstance(_startUI);
        Container.Bind<ILevelRestartCommand>().FromInstance(_gameOverUI);
        Container.Bind<IPlaytimeProvider>().FromInstance(_playtimer);
    }
}