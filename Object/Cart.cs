using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSharp_Basic.Object
{
    /// <summary>
    /// Class Cart
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Cart Id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// CustomerID
        /// </summary>
        public Guid UserId { get; set; }
    }
}