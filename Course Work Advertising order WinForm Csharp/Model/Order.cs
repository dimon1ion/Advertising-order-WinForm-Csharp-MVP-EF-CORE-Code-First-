using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Course_Work_Advertising_order_WinForm_Csharp.DbObjects
{
    public class Order
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public int AdTypeId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int SocialNetworkId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Text { get; set; }

        public virtual AdType AdType { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual SocialNetwork socialNetwork { get; set; }
    }
}
