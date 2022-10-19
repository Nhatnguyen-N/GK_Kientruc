using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussi
{
    public class Student
    {
        public long BNid { get; set; }
        public string tenBN { get; set; }
        public string diaChi { get; set; }
        public string SDT { get; set; }
        public DateTime DOB { get; set; }
        public bool gioitinh { get; set; } 
        public Student():this (0, "no-name", "no-address", "000000",false ,new DateTime())
        {

        }
        public Student(long id, string fname, string diachi, string sdt,bool gt, DateTime dob)
        {
            BNid = id; tenBN = fname; DOB = dob; diaChi = diachi; SDT = sdt;gioitinh = gt;
        }
        public override string ToString()
        {
            return BNid + "-" + tenBN + "-" + diaChi + "-" +SDT+ "-"+gioitinh+"-" + DOB;
        }
    }
}
