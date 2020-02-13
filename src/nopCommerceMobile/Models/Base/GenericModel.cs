namespace nopCommerceMobile.Models.Base
{
    public class GenericModel<T>
    {
        public bool IsSuccessStatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
