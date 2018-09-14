using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Nfield.Aad.Helpers;
using Nfield.Aad.Models;
using Nfield.Aad.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Nfield.Aad.ViewModels
{
    public class SignInViewModel: INotifyPropertyChanged
    {
        public static string AppId = "a2768270-5eb7-4950-847c-db97771c0b04";
        public static string Authority = "https://login.windows.net/niponfieldusers.onmicrosoft.com";
        public static string ReturnUri = "Nfield.Aad://Views/MainPage";
        

        public NotifyTaskCompletion<AuthenticationResult> _authResult { get; set; }

        public NotifyTaskCompletion<AuthenticationResult> AuthResult
        {
            get
            {
                return _authResult;
            }
            set
            {
                if (_authResult != value)
                {
                    _authResult = value;
                    OnPropertyChanged("AuthResult");
                }
            }
        }

        public SignInViewModel(string server)
        {
            var auth = DependencyService.Get<IAuthenticator>();

            var graphResource = GetGraphResource(server);

            _authResult = new NotifyTaskCompletion<AuthenticationResult>(auth.AuthenticateAsync(Authority, graphResource, AppId, ReturnUri));
        }

        private string GetGraphResource(string server)
        {
            if (server.ToLower() == "beta")
            {
                return "108d6c68-8b1d-4b43-b048-9b6b915a6b12";
            };

            return "e32b530d-035b-4dd6-9d62-046e44fe0f93";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
