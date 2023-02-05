﻿using Company_Site.Data;
using Company_Site.DB;
using Company_Site.Helpers;

using Microsoft.AspNetCore.Components;

namespace Company_Site.Pages.User.Account_Pages
{
    public partial class ResolutionStatus
    {
        #region Public Members

        /// <summary>
        /// The id of the logged in user
        /// </summary>
        [CascadingParameter(Name = "UserId")]
        public string UserId { get; set; }

        #endregion

        #region Private Members

        /// <summary>
        /// The resolution model
        /// </summary>
        private ResolutionStatusModel Model { get; set; }

        /// <summary>
        /// The logged in user
        /// </summary>
        private Data.User User;

        #endregion

        #region Inject Members

        [Inject]
        private ApplicationState _applicationState { get; set;  }

        [Inject]
        private ApplicationDbContext _dbContext { get; set; }

        #endregion

        #region Overriden Methods

        protected override void OnInitialized()
        {
            Model = _dbContext.ResolutionStatusModels.Where(f => f.BorrowerCode == _applicationState.BorrowerCode).FirstOrDefault();
            if (Model == null)
            {
                Model = new()
                {
                    BorrowerCode = _applicationState.BorrowerCode,
                };
                _dbContext.ResolutionStatusModels.Add(Model);
                _dbContext.SaveChanges();
            }
            User = _dbContext.Users.Where(f => f.Id == UserId).First();
        }

        #endregion

        #region Private Methods

        private void CurrentProgressValueChanged(string value)
        {
            Model.Current_Progress = value;
            Model.Current_Progress_Modification_Date = DateTime.Now;
            Model.Current_Progress_Modified_By = $"{User.FirstName} {User.LastName}";
            Update();
        }

        private void RBIReportingValueChanged(string value)
        {
            Model.RBI_Reporting = value;
            Model.RBI_Reporting_Modification_Date = DateTime.Now;
            Model.RBI_Reporting_Modified_By = $"{User.FirstName} {User.LastName}";
            Update();
        }

        private void RatingReportingValueChanged(string value)
        {
            Model.Rating_Reporting = value;
            Model.Rating_Reporting_Modification_Date = DateTime.Now;
            Model.Rating_Reporting_Modified_By = $"{User.FirstName} {User.LastName}";
            Update();
        }

        private void CaseUpdateValueChanged(string value)
        {
            Model.Case_Update = value;
            Model.Case_Update_Modification_Date = DateTime.Now;
            Model.Case_Update_Modified_By = $"{User.FirstName} {User.LastName}";
            Update();
        }

        private void ImportantInformationValueChanged(string value)
        {
            Model.Important_information = value;
            Model.Important_Information_Modification_Date = DateTime.Now;
            Model.Important_Information_Modified_By = $"{User.FirstName} {User.LastName}";
            Update();
        }

        private void ChallangesValueChanged(string value)
        {
            Model.Challenges = value;
            Model.Challenges_Modification_Date = DateTime.Now;
            Model.Challenges_Modified_By = $"{User.FirstName} {User.LastName}";
            Update();
        }

        private void RecommendStrategyValueChanged(string value)
        {
            Model.Recommended_Strategy = value;
            Model.Recommended_Strategy_Modification_Date = DateTime.Now;
            Model.Recommended_Strategy_Modified_By = $"{User.FirstName} {User.LastName}";
            Update();
        }

        private void NextStepsValueChanged(string value)
        {
            Model.Next_steps = value;
            Model.Next_Steps_Modification_Date = DateTime.Now;
            Model.Next_Steps_Modified_By = $"{User.FirstName} {User.LastName}";
            Update();
        }

        private void ResolutionPlanValueChanged(string value)
        {
            Model.Resolution_plan = value;
            Model.Resolution_Plan_Modification_Date = DateTime.Now;
            Model.Resolution_Plan_Modified_By = $"{User.FirstName} {User.LastName}";
            Update();
        }

        private void RestructuringPlanValueChanged(string value)
        {
            Model.Resolution_plan = value;
            Model.Restructuring_Plan_Modification_Date = DateTime.Now;
            Model.Restructuring_Plan_Modified_By = $"{User.FirstName} {User.LastName}";
            Update();
        }

        private void Update()
        {
            _dbContext.ResolutionStatusModels.Update(Model);
            _dbContext.SaveChanges();
        }

        #endregion

        #region Public Methods

        public int GetId(ResolutionStatusModel t) => t.Id;

        public bool SearchItem(ResolutionStatusModel e)
        {
            return e.RBI_Reporting.Contains(_text) || e.Important_information.Contains(_text) || e.Current_Progress.Contains(_text) || e.Restructuring_plan.Contains(_text) || e.Case_Update.Contains(_text) || e.Challenges.Contains(_text) || e.Next_steps.Contains(_text) || e.Rating_Reporting.Contains(_text) || e.Recommended_Strategy.Contains(_text) || e.Resolution_plan.Contains(_text);
        }

        private string _text;

        public List<ResolutionStatusModel> Search(List<ResolutionStatusModel> enteries, string text)
        {
            _text = text;
            return enteries.Where(SearchItem).ToList();
        }

        #endregion
    }
}
