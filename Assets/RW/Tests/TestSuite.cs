using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    private Game game;

    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));

        game = gameGameObject.GetComponent<Game>();

        GameObject asteroid = game.GetSpawner().SpawnAsteroid();

        float initialYPos = asteroid.transform.position.y;

        yield return new WaitForSeconds(0.1f);

        Assert.Less(asteroid.transform.position.y, initialYPos);

        Object.Destroy(game.gameObject);
    }
}
