//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Recruitment.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CANDIDATE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CANDIDATE()
        {
            this.SELECTION_HISTORY = new HashSet<SELECTION_HISTORY>();
            this.SKILLs = new HashSet<SKILL>();
        }
    
        public string CANDIDATE_ID { get; set; }
        public string NAMA_LENGKAP { get; set; }
        public string NAMA_PANGGILAN { get; set; }
        public string KD_JENIS_KELAMIN { get; set; }
        public string TEMPAT_LAHIR { get; set; }
        public System.DateTime TANGGAL_LAHIR { get; set; }
        public string AGAMA { get; set; }
        public string STATUS_PERKAWINAN { get; set; }
        public string ALAMAT_RUMAH { get; set; }
        public string ALAMAT_ORTU { get; set; }
        public string TELP_RUMAH { get; set; }
        public string EMAIL { get; set; }
        public string NOHP { get; set; }
        public string NO_KTP { get; set; }
        public int IS_DELETED { get; set; }
        public string FOTO { get; set; }
        public string USER_CREATE { get; set; }
        public System.DateTime DATETIME_CREATE { get; set; }
        public string USER_UPDATE { get; set; }
        public System.DateTime DATETIME_UPDATE { get; set; }
        public int KODE_POS { get; set; }
        public int EXPECTED_SALARY { get; set; }
        public string JUDUL_POSISI { get; set; }
        public string CATATAN { get; set; }
        public string STATE_ID { get; set; }
        public string SOURCE_ID { get; set; }
        public string REFERER { get; set; }
        public string NPWP { get; set; }
        public string CV { get; set; }
        public Nullable<System.DateTime> AVAIABLE_JOIN { get; set; }
        public string SUITABLE_POSITION { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SELECTION_HISTORY> SELECTION_HISTORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SKILL> SKILLs { get; set; }
    }
}
