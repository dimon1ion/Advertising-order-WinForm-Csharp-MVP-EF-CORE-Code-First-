using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Course_Work_Advertising_order_WinForm_Csharp.DbObjects
{
    public class AdType
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Type { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

}
