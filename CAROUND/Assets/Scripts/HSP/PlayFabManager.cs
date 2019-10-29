﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.DataModels;
using UnityEngine.UI;
using EntityKey = PlayFab.ClientModels.EntityKey;

public class PlayFabManager : MonoBehaviour
{
    private PanelOnOff panelonoff;
    [Header("Login")]
    public InputField LoginUsernameInput, LoginPasswordInput;

    [Header("Register")]
    public InputField RegisterUsernameInput, RegisterPasswordInput, RegisterPasswordReInput, RegisterNicknameInput,RegisterEmailInput ;
    public Text RegisterstateText,RegisterPasswordReInputState;
    private bool PasswordCheck = false;

    public Text LogText;
    public string UserNickname;
   // public List<string> UserNickname = new List<string>();
    private string StatText;
    private string teststring;
    public List<string> item = new List<string>();
    //private Text StatText;
    private RoomInformation roomInformation;
    [Header("FindID")]
    public InputField  FindIDEmailInput;

    public Text ShowID;
    private string myID;
    public List<UserDataRecord> userDataRecords;

    [Header("FindPassword")] public InputField FindPasswordUsernameInput,
        FindPasswordEmailInput,
        FindPasswordResetInput,
        FindPasswordResetReInput;

    private GameObject ResetPassword;

    StringBuilder SelectCarName = new StringBuilder();
    void Start()
    {
        roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        panelonoff = GameObject.Find("PanelOnOff").GetComponent<PanelOnOff>();
        ResetPassword = GameObject.Find("ResetPassword");

    }
    public void Login()
    {
        var request = new LoginWithPlayFabRequest {Username = LoginUsernameInput.text, Password = LoginPasswordInput.text};
        //var request = new LoginWithEmailAddressRequest { Username = LoginUsernameInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess,(error) => print("로그인 실패"));


    }
    private void OnLoginSuccess(LoginResult result)
    {
        myID = result.PlayFabId;
        print("로그인 성공");
        //var request = new GetAccountInfoRequest { Email = EmailInput.text };
        var request = new GetAccountInfoRequest { Username = LoginUsernameInput.text };

        PlayFabClientAPI.GetAccountInfo(request, GetAccountSuccess,(error) => print("실패"));

    }

    private void GetAccountSuccess(GetAccountInfoResult result)
    {
        print("Account를 정상적으로 받아옴");
        //UserNickname = result.AccountInfo.Username;
        UserNickname = result.AccountInfo.TitleInfo.DisplayName;

        Debug.Log(UserNickname);
        roomInformation.Connect();
    }

    //public void av()
    //{
    //    var request3 = new GetAccountInfoRequest { Email = EmailInput.text };
    //    PlayFabClientAPI.GetAccountInfo(request3, (result) => teststring = result.AccountInfo.TitleInfo.DisplayName, (error) => print("실패"));
    //    //        request.
    //    Debug.Log(teststring);
    //}
    public void Register()
    {
        var request = new RegisterPlayFabUserRequest
            { Email = RegisterEmailInput.text, Password = RegisterPasswordInput.text, Username = RegisterUsernameInput.text,DisplayName = RegisterNicknameInput.text};

        if (RegisterPasswordInput.text == RegisterPasswordReInput.text)
        {
            RegisterPasswordReInputState.text = "";
            PasswordCheck = true;
        }
        else
        {
            RegisterPasswordReInputState.text = "비밀번호 불일치";
        }

        if (PasswordCheck == true)
        {
            PlayFabClientAPI.RegisterPlayFabUser(request, (result) => RegisterstateText.text = "회원가입 성공",
                (error) => RegisterstateText.text = "회원가입 실패");
            PasswordCheck = false;
        }
    }

   
    public void FindId()
    {
        // var request1 = new LoginWithPlayFabRequest { Username = FindIDEmailInput.text };
        // PlayFabClientAPI.LoginWithPlayFab(request1, (result)=>{print("관리자 로그인성공");}, (error) => print("관리자 로그인 실패"));

        //var request = new GetAccountInfoRequest { Username = FindIDEmailInput.text };
        //PlayFabClientAPI.GetAccountInfo(request, (result) => ShowID.text = result.AccountInfo.TitleInfo.DisplayName, (error) => print("실패"));

        var t = new SendAccountRecoveryEmailRequest { Email = "hsp7424@gmail.com", TitleId = "BAC36" };
        PlayFabClientAPI.SendAccountRecoveryEmail(t,rrr,(error)=>print(""));
        
       // var tt = new User//
    }

    public void rrr(SendAccountRecoveryEmailResult request)
    {
        Debug.Log(request);
    }
    public void FindPassword()
    {
        var request = new SendAccountRecoveryEmailRequest { Email = FindPasswordEmailInput.text, TitleId = "BAC36" };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, (result)=> print("성공"), (error) => print("실패"));

    }

    public void SendAccountRecoveryEmail()
    {
    }
    public void ChangePassword()
    {

        var request = new AddUsernamePasswordRequest { Password = FindPasswordResetInput.text };
        if (PasswordCheck == true)
        {
            PlayFabClientAPI.AddUsernamePassword(request, (result) => { }, (error) => { });
            PasswordCheck = false;
        }
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

                item.Add(result.Inventory[i].DisplayName);
                print(Inven.DisplayName + " / " + Inven.UnitCurrency + " / " + Inven.UnitPrice + " / " +
                      Inven.Expiration.ToString());
            }

            for (int i = 0; i < item.Count; i++)
            {
                Debug.Log(item[i]);
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

    public void GetData()
    {
        var request = new GetUserDataRequest() { PlayFabId = myID };
        PlayFabClientAPI.GetUserData(request, (result) =>
        {
            for (int i = 0; i < result.Data.Count; i++)
            {
                userDataRecords = result.Data.Values.ToList();
            }
            foreach (var eachData in result.Data) LogText.text += eachData.Key + " : " + eachData.Value.Value + "\n";
        }, (error) => print("데이터 불러오기 실패"));
    }
    public void LoginPanelOn()
    {
        panelonoff.PanelOn("Login");
        LoginUsernameInput.text = "";
        LoginPasswordInput.text = "";
    }

    public void RegisterPanelOn()
    {
        panelonoff.PanelOn("Register");
    }

    public void AppStartPanelOn()
    {
        panelonoff.PanelOn("AppStart");
        
    }

    public void FindIdPanelOn()
    {
        panelonoff.PanelOn("FindID");

    }

    public void FindPasswordPanelOn()
    {
        panelonoff.PanelOn("FindPassword");
        ResetPassword.SetActive(false);

    }

    void wer()
    {
        var request = new GetUserDataRequest { };

        var te = new AddGenericIDRequest();

       

        var ww = new GetPlayerProfileRequest();


    }
}
