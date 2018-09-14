using Nfield.Aad.Models;
using Nfield.Aad.Services;
using Nfield.Aad.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nfield.Aad
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SurveysListPage : ContentPage
	{
        public GetSurveysViewModel SurveysList { get; set; }

        public SurveysListPage (AuthModel auth)
		{
            InitializeComponent ();
            GetSurveys(auth);
        }

        private void GetSurveys(AuthModel auth)
        {
            try
            {
                SurveysList = new GetSurveysViewModel(auth);
                BindingContext = SurveysList;
            }
            catch (Exception)
            {
                DisplayAlert("No Surveys", "Could not retrieve survey information", "OK");
            }
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SurveyList.ItemsSource = string.IsNullOrWhiteSpace(e.NewTextValue)
                ? SurveyList.ItemsSource = SurveysList.Surveys.Result
                : SurveyList.ItemsSource = SurveysList.Surveys
                                           .Result
                                           .Where(n => n.SurveyName.ToLowerInvariant()
                                                   .Contains(e.NewTextValue.ToLowerInvariant()));
        }

        private async Task SignOut()
        {
            var auth = DependencyService.Get<IAuthenticator>();
            auth.UnAuthenticate("https://login.windows.net/niponfieldusers.onmicrosoft.com");
            await Navigation.PushAsync(new MainPage());
        }
    }
}