using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlankGoal
{
    public int numberNeeded;
    public int numberCollected;
    public Sprite goalSprite;
    public string matchValue;
}

public class GoalManager : MonoBehaviour
{
    public BlankGoal[] levelGoals;
    public GameObject goalPrefab;
    public GameObject goalIntroParent;
    public GameObject goalGameParent;
    // Start is called before the first frame update
    void Start()
    {
        SetupIntroGoals();
    }

    void SetupIntroGoals()
    {
        for(int i = 0; i < levelGoals.Length; i++)
        {
            //Create a new goal panel at the goalIntroParent Position
            GameObject goal = Instantiate(goalPrefab, goalIntroParent.transfom.position, Quaternion.identity);
            goal.transfom.SetParent(goalIntroParent.transfom);
            //Set the image and text of the goal
            GoalPanel panel = goal.GetComponent < GoalPanel();
            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thisString = "0/" + levelGoals[i].numberNeeded;
            //Create a new goal panel at the goalGameParent Position
            GameObject gameGoal = Instantiate(goalPrefab, goalGameParent.transfom.position, Quaternion.identity);
            gameGoal.transfom.SetParent(goalGameParent.transfom);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
