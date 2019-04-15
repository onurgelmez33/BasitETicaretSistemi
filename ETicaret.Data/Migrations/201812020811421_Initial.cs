namespace ETicaret.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KullaniciId = c.Int(nullable: false),
                        Adres = c.String(nullable: false, maxLength: 250),
                        Sehir = c.String(nullable: false, maxLength: 250),
                        Ulke = c.String(nullable: false, maxLength: 250),
                        PostaKodu = c.String(maxLength: 50),
                        Sirket = c.String(maxLength: 250),
                        VergiNo = c.String(maxLength: 50),
                        Telefon = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciId)
                .Index(t => t.KullaniciId);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.Guid(nullable: false),
                        Adi = c.String(nullable: false, maxLength: 250),
                        Soyadi = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        KullaniciAdi = c.String(maxLength: 250),
                        Sifre = c.String(nullable: false, maxLength: 250),
                        KargoAdresiId = c.Int(),
                        FaturaAdresiId = c.Int(),
                        EmailOnaylandi = c.Boolean(nullable: false),
                        Aktif = c.Boolean(nullable: false),
                        SonGirisTarihi = c.DateTime(nullable: false),
                        SonGirisIp = c.String(maxLength: 50),
                        KayitZamani = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adres", t => t.FaturaAdresiId)
                .ForeignKey("dbo.Adres", t => t.KargoAdresiId)
                .Index(t => t.KargoAdresiId)
                .Index(t => t.FaturaAdresiId);
            
            CreateTable(
                "dbo.Urun",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Visibility = c.Boolean(nullable: false),
                        UrunAdi = c.String(nullable: false, maxLength: 250),
                        KisaAciklama = c.String(nullable: false, maxLength: 250),
                        Aciklama = c.String(nullable: false),
                        StoklardanDussun = c.Boolean(nullable: false),
                        StokAdeti = c.Int(),
                        Slug = c.String(nullable: false, maxLength: 250),
                        Maliyet = c.Decimal(precision: 18, scale: 0),
                        Fiyat = c.Decimal(nullable: false, precision: 18, scale: 0),
                        EskiFiyat = c.Decimal(precision: 18, scale: 0),
                        OzelFiyat = c.Decimal(precision: 18, scale: 0),
                        OzelFiyatBaslangicTarihi = c.DateTime(storeType: "date"),
                        OzelFiyatBitisTarihi = c.DateTime(storeType: "date"),
                        KargolamaSuresiId = c.Int(),
                        StokKodu = c.String(maxLength: 250),
                        AdminYorumu = c.String(maxLength: 250),
                        KullaniciId = c.Int(nullable: false),
                        Deleted = c.Boolean(),
                        DeletedKullaniciId = c.Int(),
                        SonGuncelleyenKisi = c.Int(),
                        GuncellemeTarihi = c.DateTime(),
                        OlusturulmaTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KargolamaSure", t => t.KargolamaSuresiId)
                .ForeignKey("dbo.Kullanici", t => t.SonGuncelleyenKisi)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciId)
                .ForeignKey("dbo.Kullanici", t => t.DeletedKullaniciId)
                .Index(t => t.KargolamaSuresiId)
                .Index(t => t.KullaniciId)
                .Index(t => t.DeletedKullaniciId)
                .Index(t => t.SonGuncelleyenKisi);
            
            CreateTable(
                "dbo.KargolamaSure",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KargolamaSuresi = c.String(nullable: false, maxLength: 250),
                        RenkKodu = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kategori_Urun_Mapping",
                c => new
                    {
                        KategoriId = c.Int(nullable: false),
                        UrunId = c.Int(nullable: false),
                        Sira = c.Int(nullable: false),
                        OzelUrun = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.KategoriId, t.UrunId })
                .ForeignKey("dbo.Kategori", t => t.KategoriId)
                .ForeignKey("dbo.Urun", t => t.UrunId)
                .Index(t => t.KategoriId)
                .Index(t => t.UrunId);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 250),
                        KisaAciklama = c.String(nullable: false, maxLength: 250),
                        Aciklama = c.String(nullable: false, maxLength: 250),
                        UstKategoriId = c.Int(),
                        ResimId = c.Int(),
                        OlusturulmaTarihi = c.DateTime(nullable: false),
                        KullaniciId = c.Int(nullable: false),
                        Slug = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciId)
                .Index(t => t.KullaniciId);
            
            CreateTable(
                "dbo.Sepet",
                c => new
                    {
                        KullaniciId = c.Int(nullable: false),
                        UrunId = c.Int(nullable: false),
                        Adet = c.Int(nullable: false),
                        Tutar = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => new { t.KullaniciId, t.UrunId })
                .ForeignKey("dbo.Urun", t => t.UrunId)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciId)
                .Index(t => t.KullaniciId)
                .Index(t => t.UrunId);
            
            CreateTable(
                "dbo.UrunResim",
                c => new
                    {
                        UrunId = c.Int(nullable: false),
                        ResimId = c.Int(nullable: false),
                        Sira = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UrunId, t.ResimId })
                .ForeignKey("dbo.Urun", t => t.UrunId)
                .Index(t => t.UrunId);
            
            CreateTable(
                "dbo.Resim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KlasorId = c.Int(),
                        DosyaYol = c.String(nullable: false, maxLength: 250),
                        KullaniciId = c.Int(nullable: false),
                        OlusturulmaTarihi = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResimKlasor", t => t.KlasorId)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciId)
                .Index(t => t.KlasorId)
                .Index(t => t.KullaniciId);
            
            CreateTable(
                "dbo.ResimKlasor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KlasorAdi = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Siparis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guid = c.Guid(nullable: false),
                        KullaniciId = c.Int(nullable: false),
                        Tarih = c.DateTime(nullable: false),
                        Tutar = c.Decimal(nullable: false, precision: 18, scale: 0),
                        GercekTutari = c.Decimal(nullable: false, precision: 18, scale: 0),
                        FaturaAdresiId = c.Int(nullable: false),
                        KargoAdresiId = c.Int(nullable: false),
                        SiparisDurumu = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciId)
                .ForeignKey("dbo.Adres", t => t.FaturaAdresiId)
                .ForeignKey("dbo.Adres", t => t.KargoAdresiId)
                .Index(t => t.KullaniciId)
                .Index(t => t.FaturaAdresiId)
                .Index(t => t.KargoAdresiId);
            
            CreateTable(
                "dbo.SiparisUrun",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiparisId = c.Int(nullable: false),
                        UrunId = c.Int(nullable: false),
                        Adet = c.Int(nullable: false),
                        Fiyat = c.Decimal(nullable: false, precision: 18, scale: 0),
                        GercekFiyat = c.Decimal(nullable: false, precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Siparis", t => t.SiparisId)
                .Index(t => t.SiparisId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Siparis", "KargoAdresiId", "dbo.Adres");
            DropForeignKey("dbo.Siparis", "FaturaAdresiId", "dbo.Adres");
            DropForeignKey("dbo.Kullanici", "KargoAdresiId", "dbo.Adres");
            DropForeignKey("dbo.Kullanici", "FaturaAdresiId", "dbo.Adres");
            DropForeignKey("dbo.Siparis", "KullaniciId", "dbo.Kullanici");
            DropForeignKey("dbo.SiparisUrun", "SiparisId", "dbo.Siparis");
            DropForeignKey("dbo.Urun", "DeletedKullaniciId", "dbo.Kullanici");
            DropForeignKey("dbo.Sepet", "KullaniciId", "dbo.Kullanici");
            DropForeignKey("dbo.Resim", "KullaniciId", "dbo.Kullanici");
            DropForeignKey("dbo.Resim", "KlasorId", "dbo.ResimKlasor");
            DropForeignKey("dbo.Urun", "KullaniciId", "dbo.Kullanici");
            DropForeignKey("dbo.Kategori", "KullaniciId", "dbo.Kullanici");
            DropForeignKey("dbo.Urun", "SonGuncelleyenKisi", "dbo.Kullanici");
            DropForeignKey("dbo.UrunResim", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.Sepet", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.Kategori_Urun_Mapping", "UrunId", "dbo.Urun");
            DropForeignKey("dbo.Kategori_Urun_Mapping", "KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.Urun", "KargolamaSuresiId", "dbo.KargolamaSure");
            DropForeignKey("dbo.Adres", "KullaniciId", "dbo.Kullanici");
            DropIndex("dbo.SiparisUrun", new[] { "SiparisId" });
            DropIndex("dbo.Siparis", new[] { "KargoAdresiId" });
            DropIndex("dbo.Siparis", new[] { "FaturaAdresiId" });
            DropIndex("dbo.Siparis", new[] { "KullaniciId" });
            DropIndex("dbo.Resim", new[] { "KullaniciId" });
            DropIndex("dbo.Resim", new[] { "KlasorId" });
            DropIndex("dbo.UrunResim", new[] { "UrunId" });
            DropIndex("dbo.Sepet", new[] { "UrunId" });
            DropIndex("dbo.Sepet", new[] { "KullaniciId" });
            DropIndex("dbo.Kategori", new[] { "KullaniciId" });
            DropIndex("dbo.Kategori_Urun_Mapping", new[] { "UrunId" });
            DropIndex("dbo.Kategori_Urun_Mapping", new[] { "KategoriId" });
            DropIndex("dbo.Urun", new[] { "SonGuncelleyenKisi" });
            DropIndex("dbo.Urun", new[] { "DeletedKullaniciId" });
            DropIndex("dbo.Urun", new[] { "KullaniciId" });
            DropIndex("dbo.Urun", new[] { "KargolamaSuresiId" });
            DropIndex("dbo.Kullanici", new[] { "FaturaAdresiId" });
            DropIndex("dbo.Kullanici", new[] { "KargoAdresiId" });
            DropIndex("dbo.Adres", new[] { "KullaniciId" });
            DropTable("dbo.SiparisUrun");
            DropTable("dbo.Siparis");
            DropTable("dbo.ResimKlasor");
            DropTable("dbo.Resim");
            DropTable("dbo.UrunResim");
            DropTable("dbo.Sepet");
            DropTable("dbo.Kategori");
            DropTable("dbo.Kategori_Urun_Mapping");
            DropTable("dbo.KargolamaSure");
            DropTable("dbo.Urun");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Adres");
        }
    }
}
