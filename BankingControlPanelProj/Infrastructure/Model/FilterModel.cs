namespace BankingControlPanelProj.Infrastructure.Model
{
    public class FilterModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? key { get; set; }
    }
}
