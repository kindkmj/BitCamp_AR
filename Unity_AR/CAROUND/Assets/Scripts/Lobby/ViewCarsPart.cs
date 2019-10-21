using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewCarsPart : MonoBehaviour
{
    public Image[] CarImages = new Image[4];
    public Text ImgText;
    private Object[] CarsPart;
    public Dictionary<string,Object[]> CarResources = new Dictionary<string, Object[]>();
    // Start is called before the first frame update
    void Start()
    {
        InitDictionary("DerbyCarImages", "CarImages/DerbyCars");
        InitDictionary("F1CarImages", "CarImages/F1Cars");
        InitDictionary("OffRoadTruckImages", "CarImages/OffRoadTrucks");
        InitDictionary("RacingTruckImages", "CarImages/RacingTrucks");
        InitDictionary("StreetCarImages", "CarImages/StreetCars");
        InitDictionary("SuperSportsCarImages", "CarImages/SuperSportsCars");

        InitDictionary("DerbyCars", "Cars/DerbyCars");
        InitDictionary("F1Cars", "Cars/F1Cars");
        InitDictionary("OffRoadTrucks", "Cars/OffRoadTrucks");
        InitDictionary("RacingTrucks", "Cars/RacingTrucks");
        InitDictionary("StreetCars", "Cars/StreetCars");
        InitDictionary("SuperSportsCars", "Cars/SuperSportsCars");


    }

    public void InitCarSprites()
    {
        int j = 1;
        foreach (var carResource in CarResources)
        {
            if (carResource.Key == ImgText.text)
            {
                for (int i = 0; i < CarImages.Length; i++)
                {
                    //뒤쪽 배열의 값은 홀수만 입력되어야 하므로 임시적으로 해당 방식을 사용함.
                    CarImages[i].sprite = carResource.Value[j] as Sprite;
                    j+=2;  
                }
            }
        }
    }

   
    void InitDictionary(string CarName,string Path)
    {
        CarsPart = Resources.LoadAll(Path);
        CarResources.Add(CarName, CarsPart);
        CarsPart.Initialize();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
