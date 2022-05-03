# Asp.Net Core ile Full Stack E-Ticaret Uygulaması

## Uygulamaya Genel Bakış

Uygulama çok geniş kapsamlı bir E-Ticaret uygulamasıdır. Kullanıcılar siteye girerek Anasayfadaki ürünleri görebilir, ürün detaylarına gidebilir veya kategori menüsünden ilgili kategorilerdeki ürünlere ulaşabilir. Bunlar dışında siteye kayıt olabilir, giriş yapabilir hatta şifresini unutursa **"Şifremi Unuttum"** bölümünden şifresini değiştirebilir.

Bu projeyi geliştirirken benim en çok zamanımı alan kısım **Admin Paneli** kısmıdır. Sadece Admin panelini kodlamam yaklaşık 1 ay sürdü. Admin Paneli kısmında admin rolüne sahip kullanıcı Ürün, Kategori, Kullanıcı ve Rol kısımlarına veri ekleme, silme, güncelleme gibi işlemleri rahatlıkla yapabilir. Uygulamanın nasıl bir yazılım mimarisi ile geliştirildiği ve nasıl kullanılacağı aşağıda verilmiştir.

## Front-End

Uygulamanın Front-End kısmı **HTML, CSS, JavaScript, jQuery** ve **Bootstrap 5** yazılım dilleri ile geliştirildi.

## Back-End

Uygulamanın Back-End kısmında tüm VTYS (Veri Tabanı Yönetim Sistemi) de standart haline gelmiş **SQL** dili kullanıldı. Ancak Veritabanı sorgu
yönetiminde .Net Core ile birlikte gelen **Entity Framework** aracındaki Linq sorguları ile işlemler gerçekleştirildi.

## Asp.Net Core Konu Başlıkları

* Generic Repository Pattern
* Unit Of Work Design Pattern
* Entity Framework Core
* Fluent Api
* Fluent Validation
* Dependency Injection
* Asp.Net Core Identity
* Web Api (Get-Post-Delete-Put)
* Web Api DTO (Data Transfer Object)
* Web Api Cors (Cross-Origin Resource Sharing)

## Web Api

API (Application Programming Interface) kısmında Web katmanındaki gibi, Business (İş) katmanındaki metotlar çağrılarak **GET** (Verileri Almak), **POST** (Verileri Kaydetmek), **DELETE** (Verileri Silmek) ve **PUT** (Verileri Güncellemek) gibi standart işlemler yapıldı.

Apideki bu bilgilere başka bir Front-End programlama dilinden ulaşmak için (JavaScript gibi) **CORS** (Cross-Origin Resource Sharing) politikasını, projenin **Startup** katmanında yeni bir Policy (politika) ekleyerek yasakları kaldırdım. Eğer bunu yapmamış olsaydık Api katmanına JavaScript üzerinden erişim sağlayamazdık.

---

## Uygulamayı Çalıştırmak

Projeyi sıfırdan çalıştırmak için ilgili dosyaları indirip açtıktan sonra yapılması gereken ilk işlem Veritabanı bağlantısıdır. Veritabanı bağlantısı **shopapp.webui > aspsettings.json > ConnectionStrings > MsSqlDesktopConnection** adresi üzerinde yer alıyor. Bu veritabanı adresinin veritabanına iletildiği kod satırı ise **shopapp.webui > Startup > ConfigureServices** içerisinde yer almaktadır. Bu adresi istediğiniz gibi düzenleyip bağlantı sağlayabilirsiniz.

Ben bu bağlantıyı **MsSql** veritabanı ile yaptım, eğer sizin bilgisayarınızda **MsSql**  veritabanı yoksa uygulama veritabanına bağlanamayacak ve hata verecektir. Eğer uygulamayı başka bir veritabanında çalıştırmak istiyorsanız (MySql gibi) **shopapp.webui** ve **shopapp.data** katmanlarındaki **Migrations** klasörünü silip yeni bir Migration oluşturmanız gerekir, tabii bunu yapabilmeniz için de **Asp.Net Core** bilgisine sahip olmanız gerekmektedir.

## Mail Gönderimi

Mail gönderme bağlantısı **shopapp.webui > appsettings.json > EmailSender** sekmesi altında yer alıyor. Ben bu bağlantıyı varsayılan olarak Microsofta ait ücretsiz mail gönderme servisi ile ilişkilendirdim. Sizde bağlantı ayarlarını düzenleyip değiştirebilirsiniz, hatta uygulamayı yayınladığınızda hosting üzerinde bulunan Mail bilgilerini girerek **Web Mail** üzerinden kullanıcılara mail gönderebilirsiniz. Tabii bu servisi kullanabilmek için **Host, Port** ve **EnableSsl** dışında Mail gönderilecek Email adresi ile Mail'in şifresini yamanız gerekir, bu bilgiler yazılmadıkça uygulamadaki Mail gönderme servisleri çalışmayacaktır.

![Resim](https://r.resimlink.com/fOp1I.png2)
