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
    
    public partial class rating
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rating()
        {
            this.game = new HashSet<GameEntity>();
        }
    
        public int id { get; set; }
        public Nullable<int> idade { get; set; }
        public int id_regiao { get; set; }
        public string name { get; set; }
    
        public virtual region region { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameEntity> game { get; set; }
    }
}
