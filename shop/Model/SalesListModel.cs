using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Model
{
    public class SalesListModel
    {
        public int id {  get; set; }
        public string datetime {  get; set; }
        public string user {  get; set; }
        public int price { get; set; }
        public string status { get; set; }
    }
}
