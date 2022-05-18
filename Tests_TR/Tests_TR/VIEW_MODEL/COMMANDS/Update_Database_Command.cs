namespace Tests_TR
{
    public partial class PagesViewModel
    {
        private static RelayCommand Update_Database_Command()
        {
            return new(cap => unit_Of_Work.SaveChangesAsync());
        }
    }
}
