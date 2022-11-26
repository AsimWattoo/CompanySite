using Company_Site.Base;

namespace Company_Site.Data
{
    public class ResolutionStatusModel : BaseModel
    {
        public int BorrowerCode { get; set; }

        public string? Current_Progress { get; set; }

        public string? RBI_Reporting { get; set; }

        /// <summary>
        /// The id of the user who wrote the memo
        /// </summary>
        public string? Rating_Reporting { get; set; }

        public string? Case_Update { get; set; }

        public string? Important_information { get; set; }

        public string? Challenges { get; set; }


        public string? Recommended_Strategy { get; set; }

        public string? Next_steps { get; set; }

        public string? Resolution_plan { get; set; }

        public string? Restructuring_plan { get; set; }
    }
}
