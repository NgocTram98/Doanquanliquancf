using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyQuanCaFe.DTO
{
    class BillInfo
    {
        int id,
            idBill,
            idFood,
            count;

        public BillInfo(int id, int idBill, int idFood, int count)
        {
            this.Id = id;
            this.IdBill = idBill;
            this.IdFood = idFood;
            this.Count = count;
        }
        
        public BillInfo (DataRow row)
        {
            this.Id = (int) row["id"];
            this.idFood = (int)row["idFood"];
            this.idBill = (int)row["idBill"];
            this.count = (int)row["count"];
        }

        public int Id { get => id; set => id = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int Count { get => count; set => count = value; }
    }
}
