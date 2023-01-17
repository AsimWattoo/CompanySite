using Company_Site.Base;

namespace Company_Site.Data
{
    public class ResolutionStatusModel : BaseModel<int>
    {
        public int BorrowerCode { get; set; }

        public string? Current_Progress { get; set; }
        public DateTime Current_Progress_Modification_Date { get; set; }
        public string? Current_Progress_Modified_By { get; set; }

        public string? RBI_Reporting { get; set; }
        public DateTime RBI_Reporting_Modification_Date { get; set; }
        public string? RBI_Reporting_Modified_By { get; set; }

        public string? Rating_Reporting { get; set; }
        public DateTime Rating_Reporting_Modification_Date { get; set; }
        public string? Rating_Reporting_Modified_By { get; set; }

        public string? Case_Update { get; set; }
        public DateTime Case_Update_Modification_Date { get; set; }
        public string? Case_Update_Modified_By { get; set; }

        public string? Important_information { get; set; }
        public DateTime Important_Information_Modification_Date { get; set; }
        public string? Important_Information_Modified_By { get; set; }

        public string? Challenges { get; set; }
        public DateTime Challenges_Modification_Date { get; set; }
        public string? Challenges_Modified_By { get; set; }

        public string? Recommended_Strategy { get; set; }
        public DateTime Recommended_Strategy_Modification_Date { get; set; }
        public string? Recommended_Strategy_Modified_By { get; set; }

        public string? Next_steps { get; set; }
        public DateTime Next_Steps_Modification_Date { get; set; }
        public string? Next_Steps_Modified_By { get; set; }

        public string? Resolution_plan { get; set; }
        public DateTime Resolution_Plan_Modification_Date { get; set; }
        public string? Resolution_Plan_Modified_By { get; set; }

        public string? Restructuring_plan { get; set; }
        public DateTime Restructuring_Plan_Modification_Date { get; set; }
        public string? Restructuring_Plan_Modified_By { get; set; }
    }
}
