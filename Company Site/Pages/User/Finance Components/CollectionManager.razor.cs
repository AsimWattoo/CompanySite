using Company_Site.Base;
using Company_Site.Data;
using Company_Site.Interfaces;
using Company_Site.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Company_Site.Pages.User.Finance_Components
{
    public partial class CollectionManager : BaseAddPage<CollectionEntry, int>, ITable<CollectionEntry, int>
    {
        #region Private Members

        public Dictionary<string, Func<CollectionEntry, string>> Headers { get; set; } = new Dictionary<string, Func<CollectionEntry, string>>()
        {
            ["Trust Code"] = (CollectionEntry e) => e.TrustCode,
            ["Borrower"] = (CollectionEntry e) => e.Borrower.ToString(),
            ["Trust Name"] = (CollectionEntry e) => e.Trust_Name,
            ["Borrower Name"] = (CollectionEntry e) => e.BorrowerName,
            ["Amount"] = (CollectionEntry e) => e.CreditAmount.ToString(),
            ["Credit Date"] = (CollectionEntry e) => e.CreditDate.ToString("dd-MMM-yyyy"),
            ["Source"] = (CollectionEntry e) => e.Source,
        };

        /// <summary>
        /// The list of borrowers to show
        /// </summary>
        private List<Account> Borrowers { get; set; } = new List<Account>();

        public bool DeleteRelatedEntries { get; set; } = false;

        /// <summary>
        /// The list fo debt profiles for the borrower code
        /// </summary>
        private List<DebtProfileModel> DebtProfiles { get; set; } = new List<DebtProfileModel>();

        #endregion

        #region Collection Sub Entries Table

        /// <summary>
        /// Headers for the relation table header
        /// </summary>
        private Dictionary<string, Func<CollectionSubEntryViewModel, string>> RelationTableHeaders { get; set; } = new Dictionary<string, Func<CollectionSubEntryViewModel, string>>()
        {
            ["Trust Code"] = t => t.TrustCode,
            ["Trust Name"] = t => t.TrustName == null ? "" : t.TrustName,
            ["Holder Name"] = t => t.HolderName == null ? "" : t.HolderName,
            ["Share"] = t => t.Share.ToString(),
            ["Amount"] = t => t.Amount.ToString()
        };

        private List<CollectionSubEntryViewModel> TrustRelationModelEnteries { get; set; } = new List<CollectionSubEntryViewModel>();

        private Dictionary<string, TableInput<CollectionSubEntryViewModel>> RelationModelEditableFields { get; set; } = new Dictionary<string, TableInput<CollectionSubEntryViewModel>>()
        {
            ["Trust Code"] = null,
            ["Trust Name"] = null,
            ["Holder Name"] = null,
            ["Share"] = new(t => nameof(t.Share), 0),
            ["Amount"] = null,
        };

        #endregion

        #region Overriden Methods

        protected override void Setup()
        {
            _dbSet = _dbContext.Collections;
            Borrowers = _dbContext.Accounts.ToList();
        }

        #endregion

        #region Private Methods

        private int GetRelationId(CollectionSubEntryViewModel model) => model.Id;

        /// <summary>
        /// Gets and sets the name of the borrower based on id
        /// </summary>
        /// <param name="value"></param>
        private void BorrowerChanged(object code)
        {
            if(code is int value)
            {
                NewEntry.Borrower = value;
                List<TrustRelationModel> relationModels = _dbContext.TrustRelations.Where(f => f.BorrowerCode == NewEntry.Borrower).ToList();
                Account? account = _dbContext.Accounts.Where(f => f.BorrowerCode == NewEntry.Borrower).FirstOrDefault();

                if (account == null)
                {
                    return;
                }
                NewEntry.BorrowerName = account.Company;
                DebtProfiles = _dbContext.DebtProfiles.Where(f => f.BorrowerCode == value).ToList();
                TrustRelationModelEnteries.Clear();
                foreach (TrustRelationModel relationModel in relationModels)
                {
                    Trust? t = _dbContext.Trusts.Where(f => f.TrustCode == f.TrustCode).FirstOrDefault();
                    if (t == null)
                        continue;
                    TrustRelationModelEnteries.Add(new CollectionSubEntryViewModel()
                    {
                        TrustCode = relationModel.TrustCode,
                        TrustName = t.Trust_Name,
                        Amount = 0,
                        Share = 0,
                        HolderName = t.SRHolder,
                        BorrowerCode = NewEntry.Borrower,
                    });
                }
            }
        }

        /// <summary>
        /// Fires when the save command is initiated
        /// </summary>
        /// <returns></returns>-
        protected override bool SaveSetup()
        {
            if (!ShouldAdd)
            {
                List<CollectionAdjustmentModel> collectionAdjustments = _dbContext.CollectionAdjustments.Where(f => f.CombinedCollectionId == NewEntry.CollectionId).ToList();
                _dbContext.CollectionAdjustments.RemoveRange(collectionAdjustments);
                _dbContext.SaveChanges();
            }

            if (ShouldAdd)
            {
                try
                {
                    //Getting New Collection Id for the related fields
                    CollectionEntry? cEntry = _dbContext.Collections.OrderByDescending(f => f.CollectionId).FirstOrDefault();
                    List<CollectionEntry> collections = new List<CollectionEntry>();
                    int collectionId = 1;

                    if (cEntry != null)
                        collectionId = cEntry.CollectionId.Value + 1;

                    //Storing the collections
                    foreach (CollectionSubEntryViewModel subEntry in TrustRelationModelEnteries)
                    {
                        CollectionEntry entry = NewEntry.Clone();
                        entry.TrustCode = subEntry.TrustCode;
                        entry.Trust_Name = subEntry.TrustName;
                        entry.CreditAmount = (NewEntry.CreditAmount * subEntry.Share) / 100;
                        entry.CollectionId = collectionId;
                        collections.Add(entry);
                        _dbContext.Collections.Add(entry);
                    }
                    _dbContext.SaveChanges();

                    AddAdjustments(collections);

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                List<CollectionEntry> collections = new List<CollectionEntry>();
                foreach(CollectionSubEntryViewModel subEntry in TrustRelationModelEnteries)
                {
                    CollectionEntry entry = _dbContext.Collections.Where(f => f.Id == subEntry.Id).First();
                    entry.Borrower = NewEntry.Borrower;
                    entry.BorrowerName = NewEntry.BorrowerName;
                    entry.KYC = NewEntry.KYC;
                    entry.Source = NewEntry.Source;
                    entry.AdjustToward = NewEntry.AdjustToward;
                    entry.CollectionId = subEntry.CollectionId;
                    entry.CreditDate = NewEntry.CreditDate;
                    entry.Distribution_Id = NewEntry.Distribution_Id;
                    entry.NoneSeller = NewEntry.NoneSeller;
                    entry.NoneSellerShare = NewEntry.NoneSellerShare;
                    entry.TypeOfRecovery = NewEntry.TypeOfRecovery;
                    entry.Adjustment = NewEntry.Adjustment;
                    entry.CreditAmount = (NewEntry.CreditAmount * subEntry.Share) / 100;
                    _dbContext.Collections.Update(entry);
                    collections.Add(entry);
                }
                _dbContext.SaveChanges();
                AddAdjustments(collections);
                return true;
            }
        }

        /// <summary>
        /// Generates adjustments based on choosen option
        /// </summary>
        /// <param name="collections"></param>
        private void AddAdjustments(List<CollectionEntry> collections)
        {
            //Setting up

            if (NewEntry.Adjustment == "prop")
            {
                double totalPos = DebtProfiles.Sum(f => f.POS);
                collections.ForEach(c =>
                {
                    DebtProfiles.ForEach(f =>
                    {
                        double proportion = f.POS / totalPos;
                        double amount = proportion * c.CreditAmount;
                        _dbContext.CollectionAdjustments.Add(new CollectionAdjustmentModel()
                        {
                            AccountNumber = f.AccountNumber,
                            Amount = amount,
                            BorrowerCode = c.Borrower,
                            CollectionId = c.Id,
                            CombinedCollectionId = c.CollectionId,
                            TrustCode = c.TrustCode,
                        });
                    });
                });
            }
            else
            {
                collections.ForEach(c =>
                {
                    _dbContext.CollectionAdjustments.Add(new CollectionAdjustmentModel()
                    {
                        AccountNumber = int.Parse(NewEntry.Adjustment),
                        Amount = c.CreditAmount,
                        BorrowerCode = c.Borrower,
                        CollectionId = c.Id,
                        CombinedCollectionId = c.CollectionId,
                        TrustCode = c.TrustCode,
                    });
                });
            }
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Gets the expense id
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int GetId(CollectionEntry e) => e.Id;

		public override List<CollectionEntry> DeleteRecord(int id)
		{
            if (DeleteRelatedEntries)
            {
                CollectionEntry entry = _dbContext.Collections.Where(f => f.Id == id).First();
                List<CollectionEntry> relatedCollections = _dbContext.Collections.Where(f => f.CollectionId == entry.CollectionId).ToList();
                relatedCollections.ForEach(e => _dbContext.Collections.Remove(e));
                _dbContext.SaveChanges();
                Enteries = _dbContext.Collections.ToList();
                StateHasChanged();
                return Enteries;
            }
            else
            {
				return base.DeleteRecord(id);
			}
		}

		/// <summary>
		/// Searches the records
		/// </summary>
		/// <param name="users"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public List<CollectionEntry> Search(List<CollectionEntry> expenseEnteries, string text)
        {
            text = text.ToLower();
            return expenseEnteries.Where(e => e.TrustCode.Equals(text) || e.Source.Equals(text) || e.Trust_Name.Equals(text) || e.Borrower.Equals(text) || e.BorrowerName.Equals(text)).ToList();
        }

        public override void OnEdit(int id)
        {
            List<CollectionEntry> collections = _dbContext.Collections.ToList();
            CollectionEntry entry = collections.Where(t => t.Id == id).First();
            List<CollectionEntry> relatedEnteries = collections.Where(f => f.CollectionId == entry.CollectionId).ToList();
            DebtProfiles = _dbContext.DebtProfiles.Where(f => f.BorrowerCode == entry.Borrower).ToList();
            double totalAmount = relatedEnteries.Sum(f => f.CreditAmount);
            Trust t = _dbContext.Trusts.Where(t => t.TrustCode == NewEntry.TrustCode).First();
            NewEntry = entry.Clone();
            TrustRelationModelEnteries.Clear();
            relatedEnteries.ForEach(e =>
            {
                TrustRelationModelEnteries.Add(new CollectionSubEntryViewModel(NewEntry.Borrower, NewEntry.BorrowerName, e, t, totalAmount));
            });
			NewEntry.CreditAmount = totalAmount;
		}

		protected override void SaveResetup()
        {
            NewEntry = new CollectionEntry();
            TrustRelationModelEnteries = new List<CollectionSubEntryViewModel>();
        }

        protected override void OnDelete(CollectionEntry item)
        {
            List<CollectionAdjustmentModel> collectionAdjustments = _dbContext.CollectionAdjustments.Where(f => f.CombinedCollectionId == item.CollectionId).ToList();
            _dbContext.CollectionAdjustments.RemoveRange(collectionAdjustments);
        }

        #endregion
    }
}
