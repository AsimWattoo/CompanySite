using Company_Site.Data;

using Microsoft.AspNetCore.Routing.Constraints;

namespace Company_Site.ViewModels
{
    public class DistributionViewModel
    {

        public string TrustCode { get; set; }

        public string TrustName { get; set;}

        public string TrustSetupDate { get; set; }

        public double TrustAge { get; set; }

        public Trust Trust { get; set; } = new Trust();

        public string IssuerName => Trust.SRIssuer;

        public string HolderName => Trust.SRHolder;

        public double IssuerSharePercentage => Trust.IssuerShare;

        public double HolderSharePercentage => Trust.HolderShare;

        public double TotalSharePercentage => HolderSharePercentage + IssuerSharePercentage;

        public double SRIssued => Trust.SRIssued;

        public double IssuerShare => (SRIssued * IssuerSharePercentage) / 100;

        public double HolderShare => (SRIssued * HolderSharePercentage) / 100;

        public double IssuerUpsideSharePercentage => Trust.IssuerUpsideShare;

        public double HolderUpsideSharePercentage => Trust.HolderUpsideShare;

        public double TotalUpsideSharePercentage => IssuerUpsideSharePercentage + HolderUpsideSharePercentage;

        public double IssuerUpsideShares => (SRIssued * IssuerUpsideSharePercentage) / 100;

        public double HolderUpsideShares => (SRIssued * HolderUpsideSharePercentage) / 100;

        public double IssuerSRValue => (IssuerSharePercentage / 100) * TotalSRValue;

        public double HolderSRValue => (HolderSharePercentage / 100) * TotalSRValue;

        public double TotalSRValue { get; set; } = 0;

        public double IssuerAmount => IssuerShare * IssuerSRValue;

        public double HolderAmount => HolderShare * HolderSRValue;

        public double TotalAmount => IssuerAmount + HolderAmount;

        public double IssuerUpsideSRValue => (IssuerUpsideSharePercentage / 100) * TotalUpsideSRValue;

        public double HolderUpsideSRValue => (HolderUpsideSharePercentage / 100) * TotalUpsideSRValue;

        public double TotalUpsideSRValue { get; set; } = 0;

        public double IssuerUpsideAmount => IssuerUpsideShares * IssuerUpsideSRValue;

        public double HolderUpsideAmount => HolderUpsideShares * HolderUpsideSRValue;

        public double TotalUpsideAmount => IssuerUpsideAmount + HolderUpsideAmount;

        public double IssuerOpeningFVSRValue => (IssuerSharePercentage * 1000) / 100;

        public double HolderOpeningFVSRValue => (HolderSharePercentage * 1000) / 100;

        public double TotalOpeningFVSRValue => IssuerOpeningFVSRValue + HolderOpeningFVSRValue;

        public double IssuerOpeningDistributionValue => IssuerOpeningFVSRValue * IssuerShare;

        public double HolderOpenningDistributionValue => HolderOpeningFVSRValue * HolderShare;

        public double TotalOpeningDistributionValue => IssuerOpeningDistributionValue + HolderOpenningDistributionValue;

        public double IssuerClosingFVSRValue => IssuerOpeningFVSRValue - IssuerSRValue + IssuerUpsideSRValue;

        public double HolderClosingFVSRValue => HolderOpeningFVSRValue - HolderSRValue + HolderUpsideSRValue;

        public double TotalClosingFVSRValue => IssuerClosingFVSRValue + HolderClosingFVSRValue;

        public double IssuerClosingDistributionValue => IssuerClosingFVSRValue * IssuerUpsideShares;

        public double HolderClosingDistributionValue => HolderClosingFVSRValue * HolderUpsideShares;

        public double TotalClosingDistributionValue => IssuerClosingDistributionValue + HolderClosingDistributionValue;

        /// <summary>
        /// The opening balance for the trust
        /// Will be equal to last closing balance for the trust. If no entry for the trust then 0
        /// </summary>
        public double OpeningBalance { get; set; } = 0;

        /// <summary>
        /// The total collections for the trust
        /// </summary>
        public double TotalCollection { get; set; } = 0;

        public double TotalBalance => OpeningBalance + TotalCollection;

        public double GST_Rate { get; set; } = 30;

        public double ResolutionFeePercentage
        {
            get
            {
                if (TrustAge <= 1)
                    return Trust.R_Year1;
                else if (TrustAge > 1 && TrustAge <= 2)
                    return Trust.R_Year2;
                else if (TrustAge > 2 && TrustAge <= 3)
                    return Trust.R_Year3;
                else if (TrustAge > 3 && TrustAge <= 4)
                    return Trust.R_Year4;
                else if (TrustAge > 4 && TrustAge <= 5)
                    return Trust.R_Year5;
                else if (TrustAge > 5 && TrustAge <= 6)
                    return Trust.R_Year6;
                else if (TrustAge > 6 && TrustAge <= 7)
                    return Trust.R_Year7;
                else if (TrustAge > 7 && TrustAge <= 8)
                    return Trust.R_Year8;
                else
                    return Trust.R_Year9;
            }
        }

        public double TrusteeshipFeePercentage
        {
            get
            {
                if (TrustAge <= 1)
                    return Trust.T_Year1;
                else if (TrustAge > 1 && TrustAge <= 2)
                    return Trust.T_Year2;
                else if (TrustAge > 2 && TrustAge <= 3)
                    return Trust.T_Year3;
                else if (TrustAge > 3 && TrustAge <= 4)
                    return Trust.T_Year4;
                else if (TrustAge > 4 && TrustAge <= 5)
                    return Trust.T_Year5;
                else if (TrustAge > 5 && TrustAge <= 6)
                    return Trust.T_Year6;
                else if (TrustAge > 6 && TrustAge <= 7)
                    return Trust.T_Year7;
                else if (TrustAge > 7 && TrustAge <= 8)
                    return Trust.T_Year8;
                else
                    return Trust.T_Year9;
            }
        }

        public double CollectionFeePercentage => Trust.CollectionFee;

        public double TrusteeshipFee
        {
            get
            {
                double intermediate_value = (TrusteeshipFeePercentage * TotalCollection) / 100;
                return intermediate_value + (intermediate_value * GST_Rate) / 100;
            }
        }

        public double ResolutionFee
        {
            get
            {
                double intermediate_value = (ResolutionFeePercentage * TotalCollection) / 100;
                return intermediate_value + (intermediate_value * GST_Rate) / 100;
            }
        }

        public double CollectionFee
        {
            get
            {
                double intermediate_value = (CollectionFeePercentage * TotalCollection) / 100;
                return intermediate_value + (intermediate_value * GST_Rate) / 100;
            }
        }

        public double TotalOtherExpenses { get; set; }

        public double InterestOnExpenses { get; set; }

        public double OtherAdjustments { get; set; }

        public double TotalExpenses => TrusteeshipFee + CollectionFee + ResolutionFee + TotalOtherExpenses + InterestOnExpenses + OtherAdjustments;

        public double Balance => TotalBalance - TotalExpenses;

        public double LessAdvancedTDS { get; set; }

        public double TDS => (Balance * GST_Rate) / 100;

        public double AmountAvailable => Math.Round((Balance - TDS - LessAdvancedTDS) / SRIssued,2);

        public double NewSRValue
        {
            get
            {
                double value = TotalOpeningFVSRValue - TotalSRValue - TotalUpsideSRValue;
                return value < 1 ? 1 : value;
            }
        }

        public double LessProvision { get; set; }

        public double ClosingBalance => (TotalSRValue + TotalUpsideSRValue) * SRIssued - LessProvision;

        public string? TrustDescription => Trust.TrustDescription;

        public string? TermsAndConditions => Trust.TermsAndConditions;
    }
}
