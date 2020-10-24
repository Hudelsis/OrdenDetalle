using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace OrdenDetalle.Entidades
{
    public class Suplidores
    {
        [Key]
        public int SuplidorId { get; set; }
        public string Nombres { get; set; }
    }
}