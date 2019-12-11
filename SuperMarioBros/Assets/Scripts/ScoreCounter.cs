using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int score = 0;

    public void ScoreChecker(string objectCount)
    {
        //Coin = 200
        //Goomba Head = 100
        //Brick break = 50
        //Collect mushroom = 1000
        //Break brick as big Mario = 50
        //Goomba hit on then the other with no hitting ground = 100 for first one + 200 for second
        //Turtle = 100
        //Hit shell turtle = 500

        switch (objectCount)
        {
            case "coin":
                score += 200;
                break;
            case "enemy":
                score += 100;
                break;
            case "brick":
                score += 50;
                break;
            case "mushroom":
                score += 1000;
                break;
            case "flower":
                score += 1000;
                break;
            case "star":
                score += 1000;
                break;
            case "enemyCombo":
                score += 300;
                break;
            case "shell":
                score += 500;
                break;
        }
    }
}
