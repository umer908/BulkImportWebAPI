namespace Application.Domain
{
    public class InsurancePolicy
    {
        public int OperationId { get; set; }
        public int Gender { get; set; }
        public string FullName { get; set; }
        public string IdentityNumber { get; set; }
        public string Operation { get; set; }
        public DateTime PolicyIssueDate { get; set; }
        public DateTime PolicyExpiryDate { get; set; }
        public string Class { get; set; }
        public int BeneficiaryTypeId { get; set; }
        public int NetworkId { get; set; }
        public decimal MaxLimit { get; set; }
        public decimal DeductibleRate { get; set; }
        public string InsuranceNumber { get; set; }
        public DateTime AcceptedDate { get; set; }
        public string UnifiedNationalNumber { get; set; }
        public string PolicyHolderName { get; set; }
        public int InsuranceCompanyNumber { get; set; }
        public string InsuranceCompanyName { get; set; }
        public string InsuranceCompanyNameAR { get; set; }
        public int RelationshipTypeId { get; set; }
        public string MainInsuranceNumber { get; set; }
        public string PolicyNumber { get; set; }
    }
}
