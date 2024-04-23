using System.ComponentModel.DataAnnotations;

namespace Hotel.ATR.Portal.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string PathToImg {  get; set; }
        public string PathToImgHover { get; set; }
    }
}
