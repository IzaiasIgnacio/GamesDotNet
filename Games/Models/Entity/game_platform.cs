//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Games.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class game_platform
    {
        public int id { get; set; }
        public int id_game { get; set; }
        public int id_platform { get; set; }
        public int id_status { get; set; }
        public Nullable<System.DateTime> release_date { get; set; }
        public Nullable<int> id_region { get; set; }
        public Nullable<int> id_rating { get; set; }
        public Nullable<int> metacritic { get; set; }
        public Nullable<decimal> preco { get; set; }
        public Nullable<int> formato { get; set; }
        public Nullable<decimal> tamanho { get; set; }
        public Nullable<int> id_store { get; set; }
    
        public virtual GameEntity game { get; set; }
        public virtual platform platform { get; set; }
        public virtual status status { get; set; }
        public virtual rating rating { get; set; }
        public virtual region region { get; set; }
        public virtual store store { get; set; }
    }
}
