namespace PlayCat.DataService.Response
{
    public class BaseResult
    {
        public bool Ok { get; set; } = true;
        public bool ShowInfo { get; set; } = true;
        public string Info { get; set; }
    }
}
