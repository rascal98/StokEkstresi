using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class STKObject
    {
        [Key]
        public int SiraNo { get; set; }
        public int IslemTur { get; set; }
        public string EvrakNo { get; set; }
        public string Tarih { get; set; }
        public int GirisMiktar { get; set; }
        public int CikisMiktar { get; set; }
        public int Stok { get; set; }
    }
}
