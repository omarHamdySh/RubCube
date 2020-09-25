using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelSegment : MonoBehaviour
{
    //public ColorLineChangerSpawner ColorLineChangerSpawner;
    public List<Lane> lanes;
    public List<CoinLine> CoinLines;
    private GameplayColor tempColor;
    public delegate void LevelSegmentEvent(LevelSegment segment);
    public LevelSegmentEvent OnColorChangerLineCreated;

    public void Init()
    {
        InitLanes();
        //createRandomColoredLanes();
        //StartCoroutine(CreateValidatedColorLineChangerRandomly());
        //CreateRandomCoinLines();
    }

    public void InitLanes()
    {
        foreach (Lane lane in lanes)
        {
            lane.Init();
        }
    }



    public void createRandomColoredLanes()
    {
        for (int i = 0; i < lanes.Count; i++)
        {
            lanes[i].laneColor = RandomizeGamePlayColor();
            bool isRedundant = true;

            if (i == 1)
            {
                while (isRedundant)
                {
                    if (lanes[i - 1].laneColor != lanes[i].laneColor)
                        isRedundant = false;
                    else
                        lanes[i].laneColor = RandomizeGamePlayColor();
                }
            }
            else if (i == 2)
            {
                while (isRedundant)
                {
                    if (lanes[i - 1].laneColor != lanes[i].laneColor && lanes[i - 2].laneColor != lanes[i].laneColor)
                        isRedundant = false;
                    else
                        lanes[i].laneColor = RandomizeGamePlayColor();
                }
            }

            lanes[i].Init();
        }
    }

    public GameplayColor RandomizeGamePlayColor()
    {
        return (GameplayColor)Random.Range(0, 3);
    }

    public bool ValidateColorDistinction(List<GameplayColor> list)
    {
        return (list.Distinct().Count() == list.Count());
    }

    public bool validateColor()
    {
        bool valid = false;

        foreach (Lane lane in lanes)
        {
            if (tempColor == lane.laneColor)
            {
                valid = true;
            }
        }

        return valid;
    }

    private void CreateRandomCoinLines()
    {
        foreach (var CoinLine in CoinLines)
        {
            CoinLine.Init();
        }
    }

}
