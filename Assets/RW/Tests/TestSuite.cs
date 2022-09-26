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

    [UnityTest]
    public IEnumerator shipMoveUp()
    {
        float prevYPos = game.GetShip().transform.position.y;
        game.GetShip().MoveUp();

        yield return new WaitForSeconds(0.1f);

        Assert.Less(prevYPos, game.GetShip().transform.position.y);
    }

    [UnityTest]
    public IEnumerator shipCanMoveDown()
    {
        float prevYPos = game.GetShip().transform.position.y;
        game.GetShip().MoveDown();

        yield return new WaitForSeconds(0.1f);

        Assert.Greater(prevYPos, game.GetShip().transform.position.y);
    }

    [UnityTest]
    public IEnumerator shipStop()
    {
        game.GetShip().setPositionYForUpMovement();
        float prevYPos = game.GetShip().transform.position.y;
        game.GetShip().MoveUp();
        float afterYpos = game.GetShip().transform.position.y;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(prevYPos, afterYpos);
    }

    [UnityTest]
    public IEnumerator shipStopMovingDown()
    {
        game.GetShip().setYPositionForDownMovement();
        float prevYPos = game.GetShip().transform.position.y;
        game.GetShip().MoveDown();
        float afterYpos = game.GetShip().transform.position.y;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(prevYPos, afterYpos);
    }

    [UnityTest]
    public IEnumerator asteroidsCanDropOrbs()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
            asteroid.transform.position = Vector3.zero;
            GameObject laser = game.GetShip().SpawnLaser();
            laser.transform.position = Vector3.zero;

            yield return new WaitForSeconds(0.5f);
        }

        Assert.AreEqual(game.healthOrbSpawned, true);
    }

    [UnityTest]
    public IEnumerator increaseHealthOnOrbPickup()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject asteroid = game.GetSpawner().SpawnAsteroid();
            asteroid.transform.position = Vector3.zero;
            GameObject laser = game.GetShip().SpawnLaser();
            laser.transform.position = Vector3.zero;

            yield return new WaitForSeconds(0.5f);
        }

        game.GetShip().transform.position = Vector3.zero;


        yield return new WaitForSeconds(0.1f);


    }

}

