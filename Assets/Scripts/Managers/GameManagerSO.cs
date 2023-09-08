using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
public enum GameState {
    MainMenu,
    Playing,
    Paused,
    GameOver
}

[CreateAssetMenu(fileName = "Game Manager", menuName = "ScriptableObjects/Managers/GameManager")]
public class GameManagerSO : ScriptableObject {
    [SerializeField] private GameObject overlayPrefab;
    private OverlayManager overlayManager;
    public GameState CurrentGameState { get; private set; }

    private void CreateOverlayManager() {
        if (overlayManager == null) {
            overlayManager = Instantiate(overlayPrefab).GetComponent<OverlayManager>();
            DontDestroyOnLoad(overlayManager.gameObject);
        }
    }

    public async UniTask UpdateGameState(GameState newGameState) {
        // We have to create this the first time
        if (!overlayManager) {
            CreateOverlayManager();
            await UniTask.Delay(TimeSpan.FromMilliseconds(10), ignoreTimeScale: false);
        }

        CurrentGameState = newGameState;

        switch (newGameState) {
            case GameState.MainMenu:
                break;
            case GameState.Playing:
                ChangeToPlaying();
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }
    }

    private async UniTask ChangeToPlaying() {
        await overlayManager.FadeIn();
        await SceneManager.LoadSceneAsync("Another Scene");
        await overlayManager.FadeOut();
    }
}
