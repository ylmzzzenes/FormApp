FormApp
Türkçe | English
Türkçe
ASP.NET Core MVC ile ürün listeleme, form işlemleri ve resim yükleme konularını çalışmak için geliştirilmiş küçük bir uygulama.

Teknolojiler: C#, .NET 8, ASP.NET Core MVC, Razor, LINQ ve Bootstrap.
Özellikler
- Ürün listeleme, ekleme, düzenleme ve silme.
- Ürün adına göre arama ve kategoriye göre filtreleme.
- Ürünlerin aktiflik durumunu toplu güncelleme.
- Form doğrulama ve hata mesajları.
- .jpg, .jpeg, .png dosyaları için uzantı ve boyut kontrolü (en fazla 2 MiB).
Yapı
Tek bir MVC projesidir. HomeController istekleri yönetir, Repository verileri bellekte tutar, Razor view'ları arayüzü oluşturur. Resim yükleme, kod tekrarını azaltmak ve sorumlulukları ayırmak için IImageStorage üzerinden DI ile alınan LocalImageStorage servisine taşınmıştır.

Veritabanı kullanılmaz; uygulama yeniden başladığında ürün değişiklikleri sıfırlanır. Yüklenen dosyalar diskte kalır.
English
A small ASP.NET Core MVC application built to practice product listing, form handling, and image uploads.

Technologies: C#, .NET 8, ASP.NET Core MVC, Razor, LINQ, and Bootstrap.
Features
- Product listing, creation, editing, and deletion.
- Search by product name and filter by category.
- Bulk updates to product active status.
- Form validation and error messages.
- File extension and size checks for .jpg, .jpeg, and .png uploads (up to 2 MiB).
Structure
A single MVC project. HomeController handles requests, Repository stores data in memory, and Razor views render the UI. Image uploads are handled by LocalImageStorage, injected through IImageStorage, to reduce duplication and separate responsibilities.

No database is required. Product changes are lost when the application restarts; uploaded files remain on disk.
Çalıştırma / Run
.NET 8 SDK gereklidir. / Requires the .NET 8 SDK.
git clone https://github.com/ylmzzzenes/FormApp.git
cd FormApp
dotnet restore
dotnet run --project FormsApp.csproj --launch-profile http
Adres / URL: http://localhost:5250

Geliştirici / Developer: Enes Yılmaz
