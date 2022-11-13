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

        public double IssuerSRValue => IssuerSharePercentage / 100;

        public double HolderSRValue => HolderSharePercentage / 100;

        public double TotalSRValue => IssuerSRValue + HolderSRValue;

        public double IssuerAmount => IssuerShare * IssuerSRValue;

        public double HolderAmount => HolderShare * HolderSRValue;

        public double IssuerUpsideSRValue => (IssuerUpsideSharePercentage * 2) / 100;

        public double HolderUpsideSRValue => (HolderUpsideSharePercentage * 2) / 100;

        public double TotalUpsideSRValue => IssuerUpsideSRValue + HolderUpsideSRValue;

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
    }
}
