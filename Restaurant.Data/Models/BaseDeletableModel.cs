using System;

namespace Restaurant.Data.Models
{
    public class BaseDeletableModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime ModifiedOn_17118018 { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;
    }
}
