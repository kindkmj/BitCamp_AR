using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCarsPart : MonoBehaviour
{
    public Image[] CarImages = new Image[4];
    public Text ImgText;
    private Object[] CarsPart;
    public Dictionary<string, Object[]> CarResources = new Dictionary<string, Object[]>();
    private Dictionary<string, Object[]> ImgCarResources = new Dictionary<string, Object[]>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < CarImages.Length; i++)
        {
            CarImages[i].enabled = false;
        }
        InitDictionary("DerbyCarImages", "CarImages/DerbyCars", false);
        InitDictionary("F1CarImages", "CarImages/F1Cars", false);
        InitDictionary("OffRoadTruckImages", "CarImages/OffRoadTrucks", false);
        InitDictionary("RacingTruckImages", "CarImages/RacingTrucks", false);
        InitDictionary("StreetCarImages", "CarImages/StreetCars", false);
        InitDictionary("SuperSportsCarImages", "CarImages/SuperSportsCars", false);

        InitDictionary("DerbyCars", "Cars/DerbyCars", true);
        InitDictionary("F1Cars", "Cars/F1Cars", true);
        InitDictionary("OffRoadTrucks", "Cars/OffRoadTrucks", true);
        InitDictionary("RacingTrucks", "Cars/RacingTrucks", true);
        InitDictionary("StreetCars", "Cars/StreetCars", true);
        InitDictionary("SuperSportsCars", "Cars/SuperSportsCars", true);
    }

    //매개변수이름 변경요망
    public void InitCarSprites(string Sequence)
    {
        if (CarImages[0].enabled != true)
        {
            for (int i = 0; i < CarImages.Length; i++)
            {
                CarImages[i].enabled = true;
            }
        }

        int j = 1;
        foreach (var carResource in ImgCarResources)
        {
            if (carResource.Key.StartsWith(Sequence))
            {
                for (int i = 0; i < CarImages.Length; i++)
                {
                    //뒤쪽 배열의 값은 홀수만 입력되어야 하므로 임시적으로 해당 방식을 사용함.
                    CarImages[i].sprite = carResource.Value[j] as Sprite;
                    j += 2;
                }
            }
        }
    }

    //왜 초기화가 진행되야하는지 적어두기.
    void InitDictionary(string CarName, string Path, bool CheckType)
    {
        if (CheckType)
        {
            CarsPart = Resources.LoadAll(Path);
            CarResources.Add(CarName, CarsPart);
            CarsPart.Initialize();
        }
        else if (!CheckType)
        {
            CarsPart = Resources.LoadAll(Path);
            ImgCarResources.Add(CarName, CarsPart);
            CarsPart.Initialize();
        }
    }
}