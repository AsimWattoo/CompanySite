using Company_Site.Enum;
using Company_Site.ViewModels;

using System.ComponentModel.DataAnnotations;

namespace Company_Site.Data
{
    public class Memo
    {

        #region Public Properties

        [Required]
        [Key]
        public string MemoNumber { get; set; }

        [Required]
        public string Service { get; set; }

        /// <summary>
        /// The id of the user who wrote the memo
        /// </summary>
        [Required]
        public int WriterId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool Financial { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public string Periodicity { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required]
        public string Vendor { get; set; }

        public string? Frequency { get; set; }

        [Required]
        public int ToId { get; set; }

        [Required]
        public int ThroughId { get; set; }

        [Required]
        public int FromId { get; set; }

        [Required]
        public DateTime ValidTill { get; set; }

        public string? Text { get; set; }

        [Required]
        public MemoStatus MemoStatus { get; set; } = MemoStatus.Pending;

        /// <summary>
        /// Indicates whether the memo is saved as draft or not
        /// </summary>
        public bool IsDraft { get; set; } = false;

        /// <summary>
        /// The selected Borrower code
        /// </summary>
        [Required]
        public int BorrowerCode { get; set; }

        [Required]
        public string TrustCode { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Copies data from the view model
        /// </summary>
        /// <param name="vm"></param>
        public void FromMemoViewModel(MemoViewModel vm)
        {
            this.Subject = vm.Subject;
            this.Periodicity = vm.Periodicity;
            this.Frequency = vm.Frequency;
            this.Financial = vm.Financial;
            this.Branch = vm.Branch;
            this.Amount = vm.Amount;
            this.Service = vm.Service;
            this.Date = vm.Date;
            this.Department = vm.Department;
            this.Text = vm.Text;
            this.Vendor = vm.Vendor;
            this.ValidTill = vm.ValidTill;
            this.Type = vm.Type;
            this.BorrowerCode = vm.BorrowerCode;
            this.TrustCode = vm.TrustCode;
        }

        /// <summary>
        /// Copies data from the view model
        /// </summary>
        /// <param name="vm"></param>
        public MemoViewModel ToMemoViewModel()
        {
            return new MemoViewModel()
            {
                Subject = this.Subject,
                Amount = Amount,
                Branch = Branch,
				Service = Service,
                Date = Date,
                Department = Department ,
                Financial = Financial ,
                Frequency = Frequency,
                IsDraft = IsDraft,
                MemoStatus = MemoStatus,
                MemoNumber = MemoNumber,
                Text = Text,
                Periodicity = Periodicity,
                Type = Type,
                ValidTill = ValidTill,
                Vendor = Vendor,
                BorrowerCode = BorrowerCode,
                TrustCode = TrustCode,
            };
		}

        #endregion

    }
}
