﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class YL_NewYear2020Entities : DbContext
    {
        public YL_NewYear2020Entities()
            : base("name=YL_NewYear2020Entities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CardList> CardList { get; set; }
        public virtual DbSet<Employing> Employing { get; set; }
        public virtual DbSet<ErrorText> ErrorText { get; set; }
        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<GameManager> GameManager { get; set; }
        public virtual DbSet<PrizeList> PrizeList { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<TimeRecord> TimeRecord { get; set; }
        public virtual DbSet<AiQiYi> AiQiYi { get; set; }
        public virtual DbSet<GiveCardRecord> GiveCardRecord { get; set; }
    }
}
