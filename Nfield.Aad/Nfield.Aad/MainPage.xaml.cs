using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Nfield.Aad.Models;
using Nfield.Aad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nfield.Aad
{
    public partial class MainPage : ContentPage
    {
        public SignInViewModel Auth { get; set; }
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public async Task Nfield_Api_SignIn()
        {
            if (TestServer.SelectedIndex == -1 || string.IsNullOrEmpty(DomainName.Text))
            {
                await DisplayAlert("Error", "Select server and supply domain", "okay");
                return;
            }

            Auth = new SignInViewModel(TestServer.Items[TestServer.SelectedIndex]);
            BindingContext = Auth;
        }

        private async Task Navigate_On_Success()
        {
            try
            {
                if (Auth == null || Auth.AuthResult.IsNotCompleted)
                {
                    return;
                }

                if (Auth.AuthResult.IsFaulted)
                {
                    await DisplayAlert("Error", "Something went wrong", "Ok");
                    return;
                }

                if (Auth.AuthResult.IsSuccessfullyCompleted)
                {
                    var result = Auth.AuthResult.Result;
                    var authDetails = new AuthModel
                    {
                        Domain = DomainName.Text,
                        Token = result.AccessToken,
                        Server = TestServer.Items[TestServer.SelectedIndex]
                    };

                    await Navigation.PushAsync(new SurveysListPage(authDetails));
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Access Denied", "User Name, Domain or Password is Incorrect", "OK");
            }
        }
    }
}
