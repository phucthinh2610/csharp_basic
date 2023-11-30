using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// UserID
        /// </summary>
        public Guid UserId { get; set; }
    }
}