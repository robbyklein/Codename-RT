using UnityEngine;

public class GameManagerTesting : MonoBehaviour {
    [SerializeField] private GameManagerSO gm;

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            gm.UpdateGameState(GameState.Playing);
        }
    }
}
