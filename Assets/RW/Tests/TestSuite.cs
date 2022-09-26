using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
            MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

    [UnityTest]
    public IEnumerator AsteroidsMoveDown()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        float initialYPos = asteroid.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Less(asteroid.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LoseHealthOnAsteroidCollision()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = game.GetShip().transform.position;
        int prevhealth = game.health;

        yield return new WaitForSeconds(2.0f);

        Assert.Less(game.GetInstanceHealth(), prevhealth);
    }

    [UnityTest]
    public IEnumerator NewGameRestartsGame()
    {
        game.isGameOver = true;
        game.NewGame();

        Assert.False(game.isGameOver);
        yield return null;
    }

    [UnityTest]
    public IEnumerator LaserMovesUp()
    {
        GameObject laser = game.GetShip().SpawnLaser();

        float initialYPos = laser.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        Assert.Greater(laser.transform.position.y, initialYPos);
    }

    [UnityTest]
    public IEnumerator LaserDestroysAsteroid()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator DestroyedAsteroidRaisesScore()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(game.score, 1);
    }

    [UnityTest]
    public IEnumerator ScoreSetToZeroNewGame()
    {
        game.isGameOver = true;
        game.NewGame();

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(game.score, 0);
    }

    [UnityTest]
    public IEnumerator ShipCanMoveLeft()
    {
        float prevXPos = game.GetShip().transform.position.x;
        game.GetShip().MoveLeft();

        yield return new WaitForSeconds(0.1f);

        Assert.Greater(prevXPos, game.GetShip().transform.position.x);
    }


    [UnityTest]
    public IEnumerator ShipCanMoveRight()
    {
        float prevXPos = game.GetShip().transform.position.x;
        game.GetShip().MoveRight();

        yield return new WaitForSeconds(0.1f);

        Assert.Less(prevXPos, game.GetShip().transform.position.x);
    }

    [UnityTest]
    public IEnumerator PlayerHealthDecreases()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = new Vector3(3.0f,3.0f);
        int prevhealth = game.health;

        yield return new WaitForSeconds(2.0f);

        Assert.Less(game.GetInstanceHealth(), prevhealth);
     }

    [UnityTest]
    public IEnumerator  checkZeroHealth()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
            asteroid.transform.position = new Vector3(3.0f, asteroid.transform.position.y);

            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2.0f);

        Assert.AreEqual(game.GetInstanceHealth(), 0);
    }
}

