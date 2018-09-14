using Newtonsoft.Json;
using Nfield.Aad.Helpers;
using Nfield.Aad.Models;
using Nfield.Aad.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nfield.Aad.ViewModels
{
    public class GetSurveysViewModel : INotifyPropertyChanged
    {
        private readonly IRestCall _rest;

        public NotifyTaskCompletion<List<SurveyDetailsModel>> _surveys { get; set; }

        public NotifyTaskCompletion<List<SurveyDetailsModel>> Surveys
        {
            get
            {
                return _surveys;
            }
            set
            {
                if (_surveys != value)
                {
                    _surveys = value;
                    OnPropertyChanged("Surveys");
                }
            }
        }

        public GetSurveysViewModel(AuthModel auth)
        {
            _rest = DependencyService.Get<IRestCall>();
            _surveys = new NotifyTaskCompletion<List<SurveyDetailsModel>>(GetSurveysAsync(auth));
        }

        private async Task<List<SurveyDetailsModel>> GetSurveysAsync(AuthModel auth)
        {
            var requestUri = string.Empty;
            switch (auth.Server.ToLower())
            {
                case "beta":
                    requestUri = "https://api.nfieldbeta.com/v1/surveys";
                    break;
                case "blue":
                    requestUri = "https://blue-api.niposoftware-dev.com/v1/Surveys";
                    break;
                case "rc":
                    requestUri = "https://rc-api.niposoftware-dev.com/v1/Surveys";
                    break;
                default:
                    break;
            };

            var surveyList = await _rest.GetAsync(requestUri, auth);
            return JsonConvert.DeserializeObject<List<SurveyDetailsModel>>(surveyList);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
