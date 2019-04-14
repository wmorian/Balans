using System;

namespace Balans.Models
{
    /// <summary>
    /// Wrapper the Amount's information.
    /// </summary>
    public class Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Amount { get; set; }

        public DateTime CreateTime { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}