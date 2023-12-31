﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Basic.Object
{
    public class Orders
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// userId
        /// </summary> 
        public Guid UserId { get; set; }

        /// <summary>
        /// Order Day
        /// </summary>
        public DateTime OrderDay { get; set; }


        /// <summary>
        /// Total
        /// </summary>
        public decimal TotalAmount { get; set; }
    }

}