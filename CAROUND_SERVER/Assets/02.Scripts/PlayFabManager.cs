using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.DataModels;
using UnityEngine.UI;
using EntityKey = PlayFab.ClientModels.EntityKey;

public class PlayFabManager : MonoBehaviour
{
    [Header("Login")]
    public InputField EmailInput, PasswordInput, UsernameInput;
    public Text LogText;
    public string UserNickname;
    private string StatText;
    //private Text StatText;
    private RoomInformation roomInformation;

    StringBuilder SelectCarName = new StringBuilder();
    void Start()
    {
        roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
    }
    public void Login()
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text };
        // 플레이팹에서 회원정보 중 username을 빼와서 UserNickname에 저장
        // 이후 roomInformation 클래스에서 사용 예정(방 만들때)
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { print("로그인 성공"); roomInformation.Connect(); }, (error) => print("로그인 실패"));

    }

    public void Register()
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) =>{print("회원가입 성공");}, (error) => print("회원가입 실패"));
    }

    public void AddMoney()
    {
        //request - 청구서 , VirtualCurrency = 통화의 단위(플레이펩에서 설정), Amount : 더하고 싶은 양
        var request = new AddUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 50 };
        PlayFabClientAPI.AddUserVirtualCurrency(request, (result) => print("돈 얻기 성공! 현재 돈 : " + result.Balance), (error) => print("돈 얻기 실패"));
    }

    public void SubtractMoney()
    {
        var request = new SubtractUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 50 };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, (result) => print("돈 빼기 성공! 현재 돈 : " + result.Balance), (error) => print("돈 빼기 실패"));
    }

    public void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
        {
            print("현재금액 : " + result.VirtualCurrency["GD"]);
            for (int i = 0; i < result.Inventory.Count; i++)
            {
                var Inven = result.Inventory[i];
                print(Inven.DisplayName + " / " + Inven.UnitCurrency + " / " + Inven.UnitPrice + " / " + Inven.Expiration.ToString());
            }
        },
        (error) => print("인벤토리 불러오기실패"));
    }

    public void GetCatalogItem()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest() { CatalogVersion = "Main" }, (result) =>
        {
            for (int i = 0; i < result.Catalog.Count; i++)
            {
                var Catalog = result.Catalog[i];
                print(Catalog.ItemId + " / " + Catalog.DisplayName + " / " + Catalog.Description + " / " +
                      Catalog.VirtualCurrencyPrices["GD"] + " / " + Catalog.Consumable.UsageCount);
            }
        },
            (error) => print("상점 불러오기 실패"));
    }

    public void PurchaseItem()
    {
        Debug.Log(SelectCarName);
        var request = new PurchaseItemRequest() { CatalogVersion = "Main", ItemId = SelectCarName.ToString(), VirtualCurrency = "GD", Price = 20 };
        PlayFabClientAPI.PurchaseItem(request, (result) => print("아이템 구입 성공"), (error) => print("아이템 구입 실패"));
    }


    public void GetCarTypes(string text)
    {
        SelectCarName.Clear();
        SelectCarName.Append(text);
    }

    public void SetData()
    {
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { "A", "AA" }, { "B", "BB" } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공"), (error) => print("데이터 저장 실패"));
    }

    //public void GetData()
    //{
    //    var request = new GetUserDataRequest() { PlayFabId = myID };
    //    PlayFabClientAPI.GetUserData(request, (result) =>
    //    { foreach (var eachData in result.Data) LogText.text += eachData.Key + " : " + eachData.Value.Value + "\n"; }, (error) => print("데이터 불러오기 실패"));
    //}
}
