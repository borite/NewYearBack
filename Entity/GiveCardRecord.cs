//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewYear2020.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class GiveCardRecord
    {
        public int ID { get; set; }
        public string OpenID { get; set; }
        public string ToOpenID { get; set; }
        public int CardID { get; set; }
        public System.DateTime GiveTime { get; set; }
        public string UserName { get; set; }
    
        public virtual CardList CardList { get; set; }
    }
}
