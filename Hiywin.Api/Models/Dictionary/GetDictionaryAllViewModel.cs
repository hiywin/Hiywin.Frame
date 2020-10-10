namespace Hiywin.Api.Models.Dictionary
{
    public class GetDictionaryAllViewModel
    {
        public string App { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        public string ParentNo { get; set; }
        public bool? IsDelete { get; set; }
    }
}
