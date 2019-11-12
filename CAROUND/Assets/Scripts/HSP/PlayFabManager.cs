using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.DataModels;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.UIElements;
using EntityKey = PlayFab.ClientModels.EntityKey;
//Version 1.0
public class PlayFabManager : InitRoomScene
{
    private PanelOnOff panelonoff;
    [Header("Login")]
    public InputField LoginUsernameInput, LoginPasswordInput;

    private Text LoginCheckText;

    [Header("Register")]
    public InputField RegisterUsernameInput, RegisterPasswordInput, RegisterPasswordReInput, RegisterNicknameInput,RegisterEmailInput ;
    public Text RegisterstateText,RegisterPasswordReInputState;
    private bool PasswordCheck = false;
    public Text IdCheckText,PasswordCheckText,NickNameCheckText,EmailCheckText;

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

    [Header("FindPassword")]
    public InputField FindPasswordUsernameInput,FindPasswordEmailInput,FindPasswordResetInput,FindPasswordResetReInput;
    private Text SendEmailCheckText;

    private GameObject ResetPassword;

    StringBuilder SelectCarName = new StringBuilder();
    void Start()
    {
        roomInformation = GameObject.Find("RoomManager").GetComponent<RoomInformation>();
        panelonoff = GameObject.Find("PanelOnOff").GetComponent<PanelOnOff>();
        ResetPassword = GameObject.Find("ResetPassword");
        LoginCheckText = GameObject.Find("LoginCheck").GetComponent<Text>();
        IdCheckText = GameObject.Find("IdCheckText").GetComponent<Text>();
        PasswordCheckText = GameObject.Find("PasswordCheckText").GetComponent<Text>();
        NickNameCheckText = GameObject.Find("NickNameCheckText").GetComponent<Text>();
        EmailCheckText = GameObject.Find("EmailCheckText").GetComponent<Text>();
        SendEmailCheckText = GameObject.Find("SendEmailCheckText").GetComponent<Text>();
    }
    public void Login()
    {
        LoginCheckText.text = "";
        var request = new LoginWithPlayFabRequest {Username = LoginUsernameInput.text, Password = LoginPasswordInput.text};
        //var request = new LoginWithEmailAddressRequest { Username = LoginUsernameInput.text, Password = PasswordInput.text };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess,(error) => LoginCheckText.text="아이디 혹은 비밀번호가 일치하지 않습니다.");


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

        try
        {
             RegisterCheck();
        }
        catch (Exception e)
        {
            throw;
        }

        if (PasswordCheck == true)
        {
            PlayFabClientAPI.RegisterPlayFabUser(request, (result) => RegisterstateText.text = "회원가입 성공",
                (error) => RegisterstateText.text = "회원가입 실패");
            PasswordCheck = false;
        }
    }
    

    void RegisterCheck()
    {
        if (!RegisterEmailInput.text.Contains("@"))
        {
            EmailCheckText.color = Color.red;
            EmailCheckText.text = "이메일 형식이 맞지 않음";
        }

        else
        {
            EmailCheckText.color = Color.black;
            EmailCheckText.text = "";
        }

        if (RegisterPasswordInput.text.Length < 6 || RegisterPasswordInput.text.Length > 10)
        {
            PasswordCheckText.color = Color.red;
            PasswordCheckText.text = "비밀번호는 6~10자리만 가능";
        }
        else
        {
            PasswordCheckText.color = Color.black;
            PasswordCheckText.text = "";
        }

        if (RegisterUsernameInput.text.Length < 3 || RegisterUsernameInput.text.Length > 10)
        {
            IdCheckText.color = Color.red;
            IdCheckText.text = "아이디는 3~10자리 영어숫자만 가능";
            
        }
        
        else
        {
            IdCheckText.color = Color.black;
            IdCheckText.text = "ss";
        }
        foreach (char ch in RegisterUsernameInput.text)
        {
            IdCheckText.color = Color.red;
            if (!(0x61 <= ch && ch <= 0x7A) && !(0x41 <= ch && ch <= 0x5A) && !(0x30 <= ch && ch <= 0x39))
                IdCheckText.text = "아이디는 3~10자리 영어숫자만 가능";
        }

        if (RegisterNicknameInput.text.Length < 3 || RegisterNicknameInput.text.Length > 5)
        {
            NickNameCheckText.color = Color.red;
            NickNameCheckText.text = "닉네임은 3~5자리만 가능";
        }
        else
        {
            NickNameCheckText.color = Color.black;
            NickNameCheckText.text = "";
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
        PlayFabClientAPI.SendAccountRecoveryEmail(request, (result)=> SendEmailCheckText.text="이메일 전송완료", (error) => SendEmailCheckText.text = "이메일 전송실패");

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
        LoginCheckText.text = "";
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
        //ResetPassword.SetActive(false);
    }
    public void LobbyPanelOn()
    {
        panelonoff.PanelOn("Lobby");
    }

    public void InputTextInit()
    {
        RegisterUsernameInput.text = "";
        RegisterPasswordInput.text = "";
        RegisterPasswordReInput.text = "";
        RegisterNicknameInput.text = "";
        RegisterEmailInput.text = "";

    }
    public void PasswordInputTextInit()
    {
    }
    public void PasswordReInputTextInit()
    {
    }
    public void NickNameInputTextInit()
    {
    }

    public void EmailInputTextInit()
    {
        FindPasswordEmailInput.text="";
    }
}
